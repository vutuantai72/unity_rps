using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UniRx;
using System.IO;
using UnityEngine.Networking;
using UnityAndroidOpenUrl;

public class Menu : MonoBehaviour
{
    [SerializeField] private RectTransform loginUI;
    [SerializeField] private RectTransform infoUI;

    private static readonly string dataType = "application/vnd.android.package-archive";
    private static string savePath;
    private static long sizeAPK;

    [SerializeField] private TextMeshProUGUI versionGameTxt;
    [SerializeField] private Button okButton;
    [SerializeField] private Button buyTicketButton;
    [SerializeField] private TextMeshProUGUI downloadProgressTxt;
    [SerializeField] private Image downloadProgress;
    [SerializeField] private GameObject downloadProgressOBJ;

    public ReactiveProperty<NetworkReachability> networkConnect = new ReactiveProperty<NetworkReachability>();
    public ReactiveProperty<string> versionGame = new ReactiveProperty<string>();
    public ReactiveProperty<string> gameURL = new ReactiveProperty<string>();

    GameService gameService = GameService.@object;

    private void Start()
    {
        if(!GameServiceControler.Instance.IsInitialize)
            GameSceneServices.Instance.LoadScene(Constants.SPLASH_SCENE);

        AudioServices.Instance.OnPlayBackgroundMusic(true);

        GameDataServices.Instance.OpenSocketConnection();

        GameDataServices.Instance.action1vsN += ActiveBuyTicketButtonHandler;

        GetVersionGame();
        infoUI.gameObject.SetActive(true);
        downloadProgressOBJ.SetActive(false);
    }

    async void GetVersionGame()
    {
        await GameDataServices.Instance.GetVersionGame();
        CheckVersionGame();
    }

    void CheckVersionGame()
    {
        Debug.Log("@@@ app ver : " + Application.version + " client ver : " + GameService.@object.gameVersion.Value);
        if (string.Equals(Application.version, GameService.@object.gameVersion.Value))
        {

            GameService.@object.isGameVersionCompatible.Value = true;
        }
        else
            GameService.@object.isGameVersionCompatible.Value = false;
    }

    public void UpdateGame()
    {
        downloadProgressOBJ.SetActive(true);
        //Application.OpenURL($"https://rpsgame.world/RPS_1.0.1.apk");
        networkConnect.Value = Application.internetReachability;
        //versionGameTxt.text = $"V{Application.version}";
        savePath = Path.Combine(Application.persistentDataPath, $"{Application.productName}_{Application.version}");
        networkConnect.ObserveEveryValueChanged(_ => Application.internetReachability)
            .Subscribe(async _ =>
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    Debug.LogError($"Internet not connected");
                    if (Directory.Exists(savePath))
                    {
                        Directory.Delete(savePath, true);
                    }
                    okButton.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError($"Internet connected");
                    await GameDataServices.Instance.GetVersionGame((res) =>
                    {
                        versionGame.Value = res.version;
                        gameURL.Value = res.link;
                    });

                    Application.version.ObserveEveryValueChanged(_ => versionGame.Value)
                        .Subscribe(_ =>
                        {
                            if (!string.Equals(Application.version, versionGame.Value))
                            {
                                gameService.isGameNeedUpdate.Value = true;
                            }

                            StartCoroutine(GetAPKSize(gameURL.Value));
                        });

                    await StartCoroutine(UpdateGame(gameURL.Value));
                }
            });
    }

    public IEnumerator UpdateGame(string url)
    {
        //yield return new WaitUntil(() => okButton.gameObject.activeSelf == false);
        Debug.LogError($"savePath ==== : {savePath}");
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        //Download file APK and open it to update
        string path = Path.Combine(savePath, "rps__official.apk");
        var downloadFile = new DownloadHandlerFile(path);
        downloadFile.removeFileOnAbort = true;
        req.downloadHandler = downloadFile;
        UnityWebRequestAsyncOperation asyncOp = req.SendWebRequest();
        while (!asyncOp.isDone)
        {
            downloadProgressTxt.text = $"{req.downloadedBytes / 1000 / 1000} MB of {sizeAPK} MB";
            downloadProgress.fillAmount = req.downloadProgress;
            yield return null;
        }
        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError($"Download failed with {req.responseCode}, reason: {req.error}");
            req.Abort();
            req.Dispose();
            yield break;
        }
        if (asyncOp.isDone && req.result != UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError($"File successfully downloaded and saved to {path}");

            AndroidOpenUrl.OpenFile(path, dataType);
        }
        yield return null;
    }
    IEnumerator GetAPKSize(string url)
    {

        yield return new WaitUntil(() => !string.IsNullOrEmpty(gameURL.Value));

        //Get APK size before downloadFile
        UnityWebRequest reqSize = UnityWebRequest.Head(url);
        yield return reqSize.SendWebRequest();
        string strSizeAPK = reqSize.GetResponseHeader("Content-Length");
        sizeAPK = Convert.ToInt64(strSizeAPK) / 1000 / 1000;
        downloadProgressTxt.text = $"{0} MB of {sizeAPK} MB";
    }
    public void CancelUpdate()
    {
        gameService.isUserConfirmLogout.Value = false;

#if UNITY_EDITOR
        GameService.@object.isGameVersionCompatible.Value = true;
#else
        if(GameService.@object.isGameVersionCompatible.Value == false)
            Application.Quit();
#endif
    }

    public void Logout()
    {
        GameService.@object.isChoseModeGame.Value = false;
        gameService.isUserConfirmLogout.Value = false;
        GoogleSignIn.Instance.OnSignOut();
    }

    public void ConfirmLogout()
    {
        gameService.isUserConfirmLogout.Value = true;
    }

#region Button at menu game
    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackModeGame()
    {
        GameService.@object.isChoseModeGame.Value = true;
        GameService.@object.isPlayerInPVP.Value = false;

        GameService.@object.isPlayerInTournament.Value = false;

        if (GameService.@object.isChoseModeGame.Value && !GameService.@object.isPlayerInPVP.Value || GameService.@object.isChoseModeGame.Value && !GameService.@object.isPlayerInTournament.Value)
            loginUI.gameObject.SetActive(true);
    }
    public void BackModePvP()
    {
        GameService.@object.isPlayerInPVP.Value = false;
        GameService.@object.isPlayerIn1vs1.Value = false;
        GameService.@object.isChoseModeGame.Value = true;
    }

    public void BackModeTournament()
    {
        GameService.@object.isPlayerInTournament.Value = false;
    }
    public void BackModeTournament1vsNWatingRoom()
    {
        GameService.@object.isPlayerInTournament_1vsN.Value = false;
        GameService.@object.isPlayerInPVP.Value = true;
    }
    public void IntroCompany()
    {
        Application.OpenURL("https://www.linkedin.com/company/playgroundx-site/");
    }

    public void PvpButtonClick()
    {
        // GameService.@object.isPlayerLogInGoogle.Value = false;
        GameService.@object.isChoseModeGame.Value = false;
        GameService.@object.isPlayerInPVP.Value = true;

        //if (!GameService.@object.isChoseModeGame.Value && GameService.@object.isPlayerInPVP.Value)
        //    loginUI.gameObject.SetActive(false);
    }

    public void TournamentButtonClick()
    {
        // GameService.@object.isPlayerLogInGoogle.Value = false;
        GameService.@object.isChoseModeGame.Value = false;
        GameService.@object.isPlayerInTournament.Value = true;

        //if (!GameService.@object.isChoseModeGame.Value && GameService.@object.isPlayerInPVP.Value)
        //    loginUI.gameObject.SetActive(false);
    }

    public void ButtonSingleClick()
    {
        GameService.@object.isChoseModeGame.Value = false;
        GameService.@object.isPlayerInPVP.Value = false;
        GameService.@object.isPlayerIn1vs1.Value = true;
        GameService.@object.modeGame.Value = GameMode.PVP;
        GameDataServices.Instance.SocketEmitGameMode(GameMode.PVP);
    }

    public void ChoseModePvpButtonClick(int coinBet)
    {
        GameDataServices.Instance.OpenSocketConnection();
        GameDataServices.Instance.SocketEmitPvPMatching(coinBet);
        if(Math.Round(GameService.@object.userCoin.Value) < coinBet) 
        {            
            return;
        }

        Debug.Log("ChoseModePvpButtonClick: " + coinBet);
        GameService.@object.betCoinPVP.Value = coinBet;
        GameService.@object.isPlayerIn1vs1.Value = false;
        GameService.@object.isShowLoadingWaiting.Value = true;
        infoUI.gameObject.SetActive(true);
        //GameService.@object.isLoading.Value = true;
    }

    public void ButtonTourClick()
    {
        GameDataServices.Instance.SocketEmitGameMode(GameMode.TOURNAMENT);
        GameDataServices.Instance.SocketEmitTourMatching();
        if (Math.Round(GameService.@object.userCoin.Value) < 100)
        {
            return;
        }
        GameService.@object.isPlayerInPVP.Value = false;
        GameService.@object.modeGame.Value = GameMode.TOURNAMENT;
        GameService.@object.isShowLoadingWaiting.Value = true;
        infoUI.gameObject.SetActive(false);
    }

    public void ButtonTour1VSNClick()
    {
        GameDataServices.Instance.SocketEmitGameMode(GameMode.TOURNAMENT1VSN);

        //GameDataServices.Instance.SocketEmitTourMatching();
        //if (Math.Round(GameService.@object.userCoin.Value) < 100)
        //{
        //    return;
        //}
        //GameService.@object.isPlayerInTournament_1vsN.Value = false;
        GameService.@object.isHaveTicket.Value = false;
        GameService.@object.modeGame.Value = GameMode.TOURNAMENT1VSN;
        GameService.@object.isPlayerInTournament_1vsN.Value = true;
        infoUI.gameObject.SetActive(false);
    }

    public void ButtonBuyTickets1vsN()
    {
        GameDataServices.Instance.SocketEmitBuyTicket1VsN();

        GameService.@object.modeGame.Value = GameMode.TOURNAMENT1VSN;
        GameService.@object.isPlayerInTournament_1vsN.Value = true;
        //GameService.@object.isShow1vsNMatchingStart.Value = true;
        GameService.@object.isShowPopupBuyTicket.Value = false;
        GameService.@object.isHaveTicket.Value = true;
        //StartCoroutine(Show1vsNLoading());
        
        infoUI.gameObject.SetActive(false);
    }

    public void ActiveBuyTicketButtonHandler()
    {
        if (gameService.isActiveBuyTicketButton.Value)
        {
            buyTicketButton.interactable = true;
        }
        else
        {
            buyTicketButton.interactable = false;
        }
    }

    public void ShowPopupBuyTicket()
    {
        gameService.isShowPopupBuyTicket.Value = true;
    }

    public void ClosePopupBuyTicket()
    {
        gameService.isShowPopupBuyTicket.Value = false;
    }

    public void ShowPopupCancelTicket()
    {
        gameService.isShowPopupCancelTicket.Value = true;
    }

    public void ClosePopupCancelTicket()
    {
        gameService.isShowPopupCancelTicket.Value = false;
    }

    public void ButtonCancelTickets1vsN()
    {
        GameDataServices.Instance.SocketEmitCancelTicket1VsN();
        GameService.@object.modeGame.Value = GameMode.TOURNAMENT1VSN;
        GameService.@object.isHaveTicket.Value = false;
        gameService.isShowPopupCancelTicket.Value = false;

        infoUI.gameObject.SetActive(false);
    }

    IEnumerator Show1vsNLoading()
    {
        yield return new WaitForSeconds(10.2f);
        GameService.@object.isPlayerInTournament_1vsN.Value = false;
        GameService.@object.isShow1vsNMatchingStart.Value = false;
        GameService.@object.isShow1vsNLoading.Value = true;
        StartCoroutine(Change1vsNScene());
    }
    IEnumerator Change1vsNScene()
    {
        yield return new WaitForSeconds(5f);
        GameService.@object.isShow1vsNLoading.Value = false;
        GameSceneServices.Instance.LoadScene(Constants.TOURNAMENT1vsN_SCENE);
    }

    public void CloseAccountSettingAtMenu()
    {
        gameService.openAccountMenu.Value = false;
    }
    #endregion
}
