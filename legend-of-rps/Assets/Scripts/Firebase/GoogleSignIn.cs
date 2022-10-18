using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WalletConnectSharp.Unity;

public class GoogleSignIn : UnityActiveSingleton<GoogleSignIn>
{
    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    private GameService gameService = GameService.@object;
    private Action actionCallback;

    public override void Awake()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = Constants.WEB_CLIEN_ID, RequestIdToken = true };
        CheckFirebaseDependencies();
        base.Awake();
    }

    private void Start()
    {
        actionCallback += UpdateInffor;
        gameService.macAddress.Value = GetMacAddress();
        gameService.deviceID.Value = SystemInfo.deviceUniqueIdentifier;
    }

    private string GetMacAddress()
    {
        string macAddresses = "";
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            // Only consider Ethernet network interfaces, thereby ignoring any
            // loopback devices etc.
            if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }
    private void UpdateInffor()
    {
        GameDataServices.Instance.UpdateInfo(gameService.userWall.Value, gameService.userName.Value, gameService.accessToken.Value, callback: (result) =>
        {
            Debug.LogWarning("LOG_TEST " + result == null);
            if (result == null)
            {
                return;
            }

            gameService.insertedCoin.Value = (int)result.coinInserted;
            gameService.userName.Value = result.user.name;
            gameService.userEmail.Value = result.user.email;
            gameService.userCoin.Value = result.user.coin;
            gameService.userWall.Value = result.user.wallet;
            gameService.userAvatar.Value = result.user.avatar;
            gameService.accessToken.Value = result.accessToken;
            gameService.userID.Value = result.user.id;
            gameService.userBirthday.Value = result.user.birth;
            gameService.userGender.Value = result.user.gender;
            gameService.userPhone.Value = result.user.phone;

        });
    }

    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    auth = FirebaseAuth.DefaultInstance;
                }
                else
                {
                    Debug.LogWarning("Could not resolve all Firebase dependencies: " + task.Result.ToString());
                }
            }
            else
            {
                Debug.LogWarning("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    public void OnSignIn()
    {
        Google.GoogleSignIn.Configuration = configuration;
        Google.GoogleSignIn.Configuration.UseGameSignIn = false;
        Google.GoogleSignIn.Configuration.RequestIdToken = true;
        Google.GoogleSignIn.Configuration.RequestEmail = true;
        Debug.LogWarning("Calling SignIn");
        try
        {
            Google.GoogleSignIn.DefaultInstance.SignIn().ContinueWith(this.OnAuthenticationFinished);
            gameService.isLoading.Value = false;
        }
        catch (Exception ex)
        {
            gameService.isLoading.Value = false;
            //Catch something here if you need later
        }
    }

    public void OnSignOut()
    {
        Debug.LogWarning("Calling SignOut");
        Google.GoogleSignIn.DefaultInstance.SignOut();

        GameSceneServices.Instance.LoadScene(Constants.MENU_SCENE);
        //LoadingBar.Instance.isLoadingScene = false;
        //LoadingBar.Instance.barProgress.fillAmount = 0;
        gameService.isPlayerLogInGoogle.Value = false;
        gameService.isShowLoadingBar.Value = false;
        gameService.dailyKey.Value = 0;

        WalletConnect.Instance.ConnectedEventSession.RemoveAllListeners();
        WalletConnect.Instance.ConnectedEvent.RemoveAllListeners();
        WalletConnect.ActiveSession.Disconnect();
        PlayerPrefs.DeleteKey(WalletConnect.SessionKey);
        GameDataServices.Instance.Logout();
    }

    public void ErrorConnectionSignOut()
    {
        //Debug.LogWarning("Calling SignOut");
        //Google.GoogleSignIn.DefaultInstance.SignOut();

        GameSceneServices.Instance.LoadScene(Constants.MENU_SCENE);
        gameService.isPlayerLogInGoogle.Value = false;
        gameService.isShowLoadingBar.Value = false;
        gameService.dailyKey.Value = 0;

        WalletConnect.Instance.ConnectedEventSession.RemoveAllListeners();
        WalletConnect.Instance.ConnectedEvent.RemoveAllListeners();
        WalletConnect.ActiveSession.Disconnect();
        PlayerPrefs.DeleteKey(WalletConnect.SessionKey);
        GameDataServices.Instance.Logout();
    }

    public void OnDisconnect()
    {
        Debug.LogWarning("Calling Disconnect");
        Google.GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    Google.GoogleSignIn.SignInException error = (Google.GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogWarning("Got Error: " + error.Status + " " + error.Message);                    
                    //gameService.isShowDialogError.Value = true;
                    //gameService.msgDialogTitleError.Value = "Error!!";
                    //gameService.msgDialogError.Value = "Login time out";
                    return;
                }
                else
                {
                    Debug.LogWarning("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.LogWarning("Canceled");
        }
        else
        {
            Debug.LogWarning("Welcome: " + task.Result.DisplayName + "!");
            Debug.LogWarning("Email = " + task.Result.Email);
            Debug.LogWarning("Google ID Token = " + task.Result.IdToken);
            Debug.LogWarning("User ID Token = " + task.Result.UserId);
            gameService.userEmail.Value = task.Result.Email;
            //gameService.userName.Value = string.IsNullOrEmpty(gameService.userName.Value) ? task.Result.DisplayName.ToString() : gameService.userName.Value;
            gameService.userAvatar.Value = PlayerPrefs.GetString("Avatar");
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        Debug.LogWarning("Sign In Successful.");

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                {
                    Debug.LogWarning("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
                }
            }
            else
            {
                gameService.userUID.Value = task.Result.UserId;
                GameDataServices.Instance.Login(gameService.userEmail.Value, gameService.userUID.Value, gameService.macAddress.Value, gameService.deviceID.Value, (result)=> 
                {
                   
                    if (result.code == 1)
                    {
                        OnSignOut();
                        gameService.isShowDialogError.Value = true;
                        gameService.msgDialogTitleError.Value = "Error!!";
                        gameService.msgDialogError.Value = "This account is already logged in on another device";
                        return;
                    }

                    if (result != null)
                    {

                        gameService.userAvatar.Value = result.data.user.avatar;
                        Debug.LogError($"Avatar: {gameService.userAvatar.Value}");

                        //loader.OnShowAvatar();

                        gameService.insertedCoin.Value = (int)result.data.coinInserted;
                        gameService.isChoseModeGame.Value = true;
                        gameService.userCoin.Value = result.data.user.coin;
                        // gameService.userWall.Value = result.user.wallet.StartsWith("0x") ? result.user.wallet : string.Empty;
                        gameService.userWall.Value = result.data.user.wallet;
                        gameService.accessToken.Value = result.data.accessToken;
                        gameService.userID.Value = result.data.user.id;                        

                        gameService.isLoading.Value = false;
                        gameService.isPlayerLogInGoogle.Value = true;

                        if (!string.IsNullOrEmpty(WalletConnect.ActiveSession.Accounts?[0]) || 
                        (!string.IsNullOrEmpty(gameService.userWall.Value) && 
                        gameService.userWall.Value.Contains("0x")))
                        {
                            GameDataServices.Instance.GetUserLRPSBalance();
                        }

                        GameDataServices.Instance.GetTodayCoin(gameService.userEmail.Value);

                        actionCallback?.Invoke();
                    }
                    else
                    {
                        Debug.Log("LOGIN RESULT NULL");
                    }
                });               
                gameService.isLoading.Value = false;
            }
        });
    }

    public void OnSignInSilently()
    {
        Google.GoogleSignIn.Configuration = configuration;
        Google.GoogleSignIn.Configuration.UseGameSignIn = false;
        Google.GoogleSignIn.Configuration.RequestIdToken = true;
        Debug.LogWarning("Calling SignIn Silently");

        Google.GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(this.OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        Google.GoogleSignIn.Configuration = configuration;
        Google.GoogleSignIn.Configuration.UseGameSignIn = true;
        Google.GoogleSignIn.Configuration.RequestIdToken = false;
        Debug.LogWarning("Calling Games SignIn");

        Google.GoogleSignIn.DefaultInstance.SignIn().ContinueWith(this.OnAuthenticationFinished);
    }
}