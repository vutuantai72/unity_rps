using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalletConnectSharp.Unity;

public class LoginUI : MonoBehaviour
{
    [SerializeField] GameObject facebookLoginBtn;
    [SerializeField] private GameObject loginUI;
    [SerializeField] private AccountSetting account;
    [SerializeField] private ImageLoader loader;

    GameService gameService = GameService.@object;

    private void Start()
    {
#if !UNITY_EDITOR
        facebookLoginBtn.SetActive(false);
#endif
    }

    public void OnSignInWithGoogle()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            gameService.isNetworkError.Value = true;
            GameDataServices.Instance.HandleConnectionError(gameService.isNetworkError.Value);
        }
        else
        {
            GameDataServices.Instance.OpenSocketConnection();
            gameService.isLoading.Value = true;
            GoogleSignIn.Instance.OnSignIn();
            StartCoroutine(UpdateUser());
            gameService.isNetworkError.Value = false;
            //account.GetUserInfomation();
        }
    }

    IEnumerator UpdateUser()
    {
        yield return new WaitForSeconds(0.8f);
        account.GetUserInfomation();
        loader.OnShowAvatar();
    }

    public void LoadMainScene()
    {
        GameService.@object.isShowLoadingBar.Value = true;
        GameService.@object.isPlayerLogInGoogle.Value = false;
        GameDataServices.Instance.CheckUserNotification();

        if (GameService.@object.isShowLoadingBar.Value)
        {
            GameService.@object.isChoseModeGame.Value = false;
            loginUI.SetActive(false);
        }        
    }

    public void OnSignInWithFacebook()
    {
        AudioServices.Instance.PlayClickAudio();
#if UNITY_EDITOR
        gameService.isLoading.Value = true;
        gameService.isShowInviteDialog.Value = true;
        //gameService.userEmail.Value = "tuna110699@gmail.com";
        gameService.userEmail.Value = "baoanhiuh.dev@gmail.com";
        gameService.userUID.Value = "ra0reSf6KnNePjUsBykr1EG5Xko1";
        gameService.userAvatar.Value = "https://lh3.googleusercontent.com/a-/AOh14Gh14Sef3x_wjrGdBPgUv2xcjZQmWEPpqRy1mamPyA=s96-c";
        //gameService.userAvatarPath.Value = "https://firebasestorage.googleapis.com/v0/b/jackpot-cf8ad.appspot.com/o/images.png?alt=media&token=c487c5e6-c79e-4d1e-bd8c-64ce679d8551";
        GameDataServices.Instance.Login(gameService.userEmail.Value, gameService.userUID.Value, gameService.macAddress.Value, gameService.deviceID.Value,  (result) =>
        {
            if (result == null)
            {
                return;
            }
            
            gameService.isLoading.Value = false; 
            gameService.isPlayerLogInGoogle.Value = true;
            gameService.insertedCoin.Value = (int)result.data.coinInserted;
            gameService.userCoin.Value = result.data.user.coin;
            gameService.userWall.Value = result.data.user.wallet;
            gameService.accessToken.Value = result.data.accessToken;
            gameService.isChoseModeGame.Value = true;
            gameService.userAvatar.Value = result.data.user.avatar;
            gameService.userName.Value = result.data.user.name;
            gameService.userID.Value = result.data.user.id;

            //For my account
            gameService.userBirthday.Value = result.data.user.birth;
            gameService.userGender.Value = result.data.user.gender;
            gameService.userPhone.Value = result.data.user.phone;
            GameDataServices.Instance.GetTodayCoin(gameService.userEmail.Value);
            account.GetUserInfomation();
            loader.OnShowAvatar();

        });

        GameDataServices.Instance.UpdateInfo(gameService.userWall.Value, gameService.userName.Value, gameService.accessToken.Value, callback: (result) =>
        {
            if (result == null)
            {
                return;
            }

            gameService.insertedCoin.Value = (int)result.coinInserted;
            gameService.userCoin.Value = result.user.coin;
            gameService.userWall.Value = result.user.wallet;
            gameService.accessToken.Value = result.accessToken;
        });
#endif
    }
}
