using UniRx;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public partial class GameService : ManagerService<GameManager, GameService>
{
    private const string MESSAGE_NO_COIN = "There is no more coin \n Please add more coins to play!!!";
    private const string MESSAGE_NO_INTERNET = "No internet connection,\n Please check your inernet";

    public ReactiveProperty<SelectType> enemyType = new ReactiveProperty<SelectType>((SelectType)0);
    public ReactiveProperty<SelectType> playerType = new ReactiveProperty<SelectType>((SelectType)3);
    public ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>(GameState.Initializing);

    #region GAMEPLAY_DATA
    public ReactiveProperty<string> userWall = new ReactiveProperty<string>();
    public ReactiveProperty<string> userName = new ReactiveProperty<string>();
    public ReactiveProperty<string> userEmail = new ReactiveProperty<string>();
    public ReactiveProperty<string> userAvatar = new ReactiveProperty<string>();
    public ReactiveProperty<string> accessToken = new ReactiveProperty<string>();
    public ReactiveProperty<string> userUID = new ReactiveProperty<string>();
    public ReactiveProperty<string> userAvatarPath = new ReactiveProperty<string>();
    public ReactiveProperty<string> enemyAvatar = new ReactiveProperty<string>();
    public ReactiveProperty<string> userID = new ReactiveProperty<string>();
    public ReactiveProperty<string> userBirthday = new ReactiveProperty<string>();
    public ReactiveProperty<string> userGender = new ReactiveProperty<string>();
    public ReactiveProperty<string> userPhone = new ReactiveProperty<string>();
    public ReactiveProperty<float> userCoin = new ReactiveProperty<float>();
    public ReactiveProperty<string> enemyName = new ReactiveProperty<string>();
    public ReactiveProperty<string> enemyID = new ReactiveProperty<string>();
    public ReactiveProperty<string> enemyEmail = new ReactiveProperty<string>();
    public ReactiveProperty<float> enemyCoin = new ReactiveProperty<float>();
    public ReactiveProperty<decimal> bnbRate = new ReactiveProperty<decimal>();
    public ReactiveProperty<float> todayCoin = new ReactiveProperty<float>();
    public ReactiveProperty<int> insertedCoin = new ReactiveProperty<int>();
    public ReactiveProperty<float> jackpotCoin = new ReactiveProperty<float>();
    public ReactiveProperty<float> jackpotCoinClaim = new ReactiveProperty<float>();
    public ReactiveProperty<string> jackpotNotify = new ReactiveProperty<string>();
    public ReactiveProperty<float> musicVolumeVal = new ReactiveProperty<float>();    
    public ReactiveProperty<int> rewardAdsCoin = new ReactiveProperty<int>();
    public ReactiveProperty<int> rouletteValue = new ReactiveProperty<int>();
    public ReactiveProperty<string> resultStatus = new ReactiveProperty<string>();
    public ReactiveProperty<string> jsonJackpot = new ReactiveProperty<string>();
    public ReactiveProperty<string> jsonCoin = new ReactiveProperty<string>();
    public ReactiveProperty<string> missionID = new ReactiveProperty<string>();
    public ReactiveProperty<string> prevTime = new ReactiveProperty<string>();
    public ReactiveProperty<string> gameVersion = new ReactiveProperty<string>();
    public ReactiveProperty<DateTime> nowTime = new ReactiveProperty<DateTime>();
    public ReactiveProperty<RankingModel> rankingModel = new ReactiveProperty<RankingModel>();
    public ReactiveProperty<MarketModel> marketModel = new ReactiveProperty<MarketModel>();
    public ReactiveProperty<UserNotificationModel> notificationModel = new ReactiveProperty<UserNotificationModel>();
    public ReactiveProperty<GameDataModel> gameDataBySever = new ReactiveProperty<GameDataModel>();
    public ReactiveProperty<GameDataModel> gameDataByClient = new ReactiveProperty<GameDataModel>();
    public ReactiveProperty<Texture> avatarPlayer = new ReactiveProperty<Texture>();
    public ReactiveProperty<Dictionary<string, Sprite>> dicAvatar = new ReactiveProperty<Dictionary<string, Sprite>>();
    public ReactiveProperty<Dictionary<string, bool>> dicDailyLogin = new ReactiveProperty<Dictionary<string, bool>>();

    public ReactiveProperty<DataCancelRoom> dataCancelRoom = new ReactiveProperty<DataCancelRoom>();
    public ReactiveProperty<GameMissionModel> missionData = new ReactiveProperty<GameMissionModel>();
    public ReactiveProperty<GameMissionModel> missionModel = new ReactiveProperty<GameMissionModel>();
    public ReactiveProperty<MyNFTList> myNFT = new ReactiveProperty<MyNFTList>();
    public IReadOnlyReactiveProperty<GameMissionModel> _missionModel => missionModel;
    #endregion

    #region GAMEPLAY_VARIABLE
    public ReactiveProperty<float> betCoin = new ReactiveProperty<float>(0);
    public ReactiveProperty<float> insCoin = new ReactiveProperty<float>(0);
    public ReactiveProperty<float> winCoin = new ReactiveProperty<float>(0);
    public ReactiveProperty<float> rewardCoin = new ReactiveProperty<float>(0);
    public ReactiveProperty<float> loginTimeout = new ReactiveProperty<float>(5);
    public ReactiveProperty<int> winStreak = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> tutorialNum = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> dailyKey = new ReactiveProperty<int>(0);
    public ReactiveProperty<bool> isInsertCoin = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isGameStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isGetJackpost = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isJackpostShow = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isUserClaimDailyReward = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isUserClaimAdsReward = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isUserGetJackpot = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isUserLoginError = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isChestJackpotShow = new ReactiveProperty<bool>(true);
    public ReactiveProperty<bool> isUserChooseAvatar = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isUserChangeAvatar = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isMale = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isFemale = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isAvatarSelected = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isAvatarSelectedInMenu = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> openAccountMenu = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isCurrentDay = new ReactiveProperty<bool>(true);
    public ReactiveProperty<bool> isNFTShowcaseOpen = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isGameNeedUpdate = new ReactiveProperty<bool>();
    public ReactiveProperty<List<SelectedAvatarModel>> listAvatar = new ReactiveProperty<List<SelectedAvatarModel>>(null);
    public ReactiveProperty<List<string>> listKey = new ReactiveProperty<List<string>>();
    public ReactiveProperty<NetworkReachability> networkConnect = new ReactiveProperty<NetworkReachability>();
    #endregion

    #region SETTING_VARIABLE
    public ReactiveProperty<bool> isAuto = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isNewUser = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isTurnOnMusic = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isTurnOnSound = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isPlayerLogInGoogle = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowDialogError = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowInviteDialog = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowPopupFilter = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowPopupBuyTicket = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowPopupCancelTicket = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isActiveBuyTicketButton = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowInviteDialogError = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isSpinButtonActive = new ReactiveProperty<bool>(false);
    public ReactiveProperty<string> msgDialogError = new ReactiveProperty<string>();
    public ReactiveProperty<string> msgDialogTitleError = new ReactiveProperty<string>();
    
    public ReactiveProperty<int> spinTurn = new ReactiveProperty<int>();
    public ReactiveProperty<bool> isLoading = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isCompleteTutorial = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isTurnOnVibration = new ReactiveProperty<bool>(true);
    public ReactiveProperty<bool> isShowLoadingBar = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isSwitchNetworkDialog = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isClaimCouponDialog = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isDemoGame = new ReactiveProperty<bool>(true);
    public ReactiveProperty<bool> isNotifiEmpty = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isGameVersionCompatible = new ReactiveProperty<bool>(true);
    public ReactiveProperty<bool> isUserConfirmLogout = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isUserHasNotifications = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isHaveTicket = new ReactiveProperty<bool>(false);
    #endregion

    #region PVP_VARIABLE
    public ReactiveProperty<bool> isChoseModeGame = new ReactiveProperty<bool>(false);
    public ReactiveProperty<GameMode> modeGame = new ReactiveProperty<GameMode>(GameMode.NONE);
    public ReactiveProperty<bool> isPlayerInPVP = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isPlayerIn1vs1 = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isPlayerInTournament = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isPlayerInTournament_1vsN = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShow1vsNMatchingStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShow1vsNLoading = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowLoadingWaiting = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isShowLoadingPagePVP = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isMathFound = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isEndGamePVP = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isResultPVP = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isMatchingComplete = new ReactiveProperty<bool>(true);
    public ReactiveProperty<string> ResultPVP = new ReactiveProperty<string>();
    public ReactiveProperty<int> betCoinPVP = new ReactiveProperty<int>(0);
    public ReactiveProperty<float> timeBet = new ReactiveProperty<float>(0);
    public ReactiveProperty<float> timeWait = new ReactiveProperty<float>(0);
    public ReactiveProperty<string> play = new ReactiveProperty<string>();
    public ReactiveProperty<string> notifyEndGame = new ReactiveProperty<string>();
    public ReactiveProperty<string> coinPlayerResult = new ReactiveProperty<string>();
    public ReactiveProperty<string> coinEnemyResult = new ReactiveProperty<string>();
    #endregion

    #region TOURNAMET_VARIABLE
    // public ReactiveProperty<bool> isShowLoadingCountDown = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isGameTourStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isFinal = new ReactiveProperty<bool>(false);
    public ReactiveProperty<int> listUserAcceptTour = new ReactiveProperty<int>();
    public ReactiveProperty<int> numberOfPlayerBattle = new ReactiveProperty<int>();
    public ReactiveProperty<List<string>> playerAvatarList = new ReactiveProperty<List<string>>();
    public ReactiveProperty<List<string>> playerNameList = new ReactiveProperty<List<string>>();

    #endregion

    #region TOURNAMET_1vsN_VARIABLE
    // public ReactiveProperty<bool> isShowLoadingCountDown = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isGameTourStart_1vN = new ReactiveProperty<bool>(false);
    //public ReactiveProperty<bool> isFinal = new ReactiveProperty<bool>(false);
    public ReactiveProperty<int> listUserAcceptTour_1vN = new ReactiveProperty<int>();
    public ReactiveProperty<int> numberOfPlayerBattle_1vN = new ReactiveProperty<int>();
    #endregion

    #region CHECK_INTERNET_CONNECTION_VARIABLE
    public ReactiveProperty<bool> isNetworkError = new ReactiveProperty<bool>(false);

    #endregion

    #region WALLET_CONNECT_VARIABLES
    public ReactiveProperty<decimal> coinToRPSRate = new ReactiveProperty<decimal>();
    public ReactiveProperty<decimal> userBalance = new ReactiveProperty<decimal>();
    public ReactiveProperty<decimal> userBNBBalance = new ReactiveProperty<decimal>();
    public ReactiveProperty<string> userWalletAllowance = new ReactiveProperty<string>();
    public ReactiveProperty<int> withdrawFee = new ReactiveProperty<int>();
    public ReactiveProperty<bool> isUserApprove = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> userSwapSuccess = new ReactiveProperty<bool>(false);
    #endregion

    public ReactiveProperty<string> macAddress = new ReactiveProperty<string>();
    public ReactiveProperty<string> deviceID = new ReactiveProperty<string>();
    public Subject<Unit> insertCoinSubject = new Subject<Unit>();
    public ReactiveProperty<int> roulettes = new ReactiveProperty<int>(0);
    public int[] drainage = new int[12];
    public int roulette;

    public string[] missionsName = new string[50]; 
    public int[] missionsValue = new int[50]; 
    public int[] missionsStatus = new int[50]; 

    public void Reset()
    {
        drainage[0] = 1;
        drainage[1] = 2;
        drainage[2] = 7;
        drainage[3] = 4;
        drainage[4] = 2;
        drainage[5] = 20;
        drainage[6] = 1;
        drainage[7] = 2;
        drainage[8] = 4;
        drainage[9] = 7;
        drainage[10] = 2;
        drainage[11] = 4;
    }
    public IObservable<int> MultipleReturn(int index)
    {
        return WhenCoinIsBeingInserted()
            .SelectMany(Observable.Return(index))
            .Select(i => drainage[i]);
    }
    public int RouletteValueDetermination()
    {
        while (true)
        {
            roulette = UnityEngine.Random.Range(0, 12);
            if (drainage[roulette] == rouletteValue.Value)
            {
                break;
            }
        }

        Debug.Log("RouletteValueDetermination " + roulette + "/ " + drainage[roulette]);
        roulette += (drainage.Length * 2);
        return roulette;
    }

    public void StopRoulette()
    {
        betCoin.Value *= WinMultiple();

        if (isGetJackpost.Value)
        {
            betCoin.Value += jackpotCoinClaim.Value;  
        }

        winCoin.Value = betCoin.Value;
        if (isGetJackpost.Value)
        {
            GameDataServices.Instance.GetJackpotCoin();
            isJackpostShow.Value = true;
            // isUserGetJackpot.Value = true;
        }
        GameDataServices.Instance.GetJackpotCoin();
    }

    public void DuringRoulette()
    {
        if (roulette > 0)
        {
            roulette -= 1;
            NextRouletteNumber();
        }
        else
        {
            StopRoulette();
            StartReceivingCoins();
        }
    }
    public void NextRouletteNumber()
    {
        if (roulettes.Value >= drainage.Length - 1)
        {
            roulettes.Value -= drainage.Length - 1;
        }
        else
        {
            roulettes.Value += 1;
        }
    }
    public void InsertCoin()
    {
        if (betCoin.Value <= 0)
        {
            insertCoinSubject.OnNext(Unit.Default);
        }

        Debug.Log($"LOG_TEST {gameState.Value}");
    }

    public void Start()
    {
        //if (gameState.Value != GameState.Selecting && gameState.Value != GameState.CoinRoulette)
        //{
        //    betCoin.Value = 1;
        //    roulettes.Value = 0;
        //    gameState.Value = GameState.Selecting;
        //}
        betCoin.Value = 1;
        roulettes.Value = 0;
    }

    public void Replay()
    {
        gameState.Value = GameState.Selecting;
    }

    /// <summary>
    /// Reset game for ready new game.
    /// </summary>
    public void InsertingCoin()
    {
        isGameStart.Value = false;
        gameState.Value = GameState.InsertingCoin;

        if (insertedCoin.Value > 0)
        {
            gameState.Value = GameState.Selecting;
        }
        else
        {
            gameState.Value = GameState.Initializing;
        }

        insertCoinSubject.OnNext(Unit.Default);
    }

    public void StartReceivingCoins()
    {
        gameState.Value = GameState.GetCoins;        
    }

    public void StartCoinRoulette()
    {
        gameState.Value = GameState.CoinRoulette;
    }

    public void GetCoins(int value = 1)
    {
        userCoin.Value += value < betCoin.Value ? value : betCoin.Value;
        todayCoin.Value += value < betCoin.Value ? value : betCoin.Value;

        //if(betCoin.Value > 100)
        //{
        //    betCoin.Value -= 50;
        //}
        //else
        betCoin.Value -= value < betCoin.Value ? value : betCoin.Value;
        PlayerPrefs.SetFloat(GameConstants.KEY_SAVE_TODAY_COIN, todayCoin.Value);
    }

    public async void SelectType(SelectType selectType)
    {
        Debug.Log("@@@ starting state: " + gameState.Value);
        playerType.Value = selectType;
        Debug.Log("SELECTED");      
        gameState.Value = GameState.Selected;
        gameState.Value = GameState.Selecting;
        //gameState.Value = GameState.WaitServerSelect;
        await GameDataServices.Instance.StartGame(selectType.ToString(),(result) =>
        {
            if (result == null)
            {
                isGameStart.Value = false;
                msgDialogTitleError.Value = "OOOPS!!";
                isShowDialogError.Value = true;
                msgDialogError.Value = MESSAGE_NO_INTERNET;
                return;
            }

            gameDataBySever.Value = result;

            rouletteValue.Value = result.data.result.value;
            resultStatus.Value = result.data.result.status;
            Debug.Log($"[StartGame] [InsertCoin]: {result.data.coinInserted}");
            insertedCoin.Value = result.data.coinInserted;
            winStreak.Value = result.data.winChain;

            if (result.data.jackpot.coin > 0)
            {
                isGetJackpost.Value = true;
                jackpotCoinClaim.Value = result.data.jackpot.coin;
            }
            else
            {
                isGetJackpost.Value = false;
            }

            Start();
        });

        gameState.Value = GameState.Selected;
        switch (resultStatus.Value)
        {
            case Constants.WIN:
                enemyType.Value = HandleWin(playerType.Value);
                break;
            case Constants.LOSE:
                enemyType.Value = HandleLose(playerType.Value);
                break;
            case Constants.DRAW:
                enemyType.Value = HandleDraw(playerType.Value);
                break;
            default:
                enemyType.Value = HandleDraw(playerType.Value);
                Debug.Log("Invalid value");
                break;
        }
        gameState.Value = GameState.SelectionComplete;
    }

    public void RandomEnemy()
    {
        if (gameState.Value != GameState.SelectionComplete)
            return;
        //enemyType.Value = (SelectType)UnityEngine.Random.Range(0, 3);
    }

    private SelectType HandleWin(SelectType selectType)
    {
        switch (selectType)
        {
            case global::SelectType.scissors:
                return global::SelectType.paper;
            case global::SelectType.rock:
                return global::SelectType.scissors;
            case global::SelectType.paper:
                return global::SelectType.rock;
            default:
                Debug.Log("AI is smoking weed");
                return (SelectType)UnityEngine.Random.Range(0, 3);
        }
    }
    private SelectType HandleLose(SelectType selectType)
    {
        switch (selectType)
        {
            case global::SelectType.scissors:
                return global::SelectType.rock;
            case global::SelectType.rock:
                return global::SelectType.paper;
            case global::SelectType.paper:
                return global::SelectType.scissors;
            default:
                Debug.Log("AI is smoking weed");
                return (SelectType)UnityEngine.Random.Range(0, 3);
        }
    }
    private SelectType HandleDraw(SelectType selectType)
    {
        switch (selectType)
        {
            case global::SelectType.scissors:
                return global::SelectType.scissors;
            case global::SelectType.rock:
                return global::SelectType.rock;
            case global::SelectType.paper:
                return global::SelectType.paper;
            default:
                Debug.Log("AI is smoking weed");
                return (SelectType)UnityEngine.Random.Range(0, 3);
        }
    }

    public int WinMultiple()
    {
        return rouletteValue.Value;
    }


    #region Game pvp

    public void StartPVP()
    {
        if (gameState.Value != GameState.Selecting)
        {
            enemyType.Value = (SelectType)3;
            gameState.Value = GameState.Selecting;
        }
        
    }

    public void SelectTypePvP(SelectType selectType)
    {
        // enemyType.Value = selectType;
        playerType.Value = selectType;
        if(isGameTourStart.Value){
            GameDataServices.Instance.SocketEmitTourPlayChoose(selectType);
        }
        else
        {
            GameDataServices.Instance.SocketEmitPlayChoose(selectType);
        }
        gameState.Value = GameState.SelectionComplete;
    }

    public void SelectType1VsN(SelectType selectType)
    {
        // enemyType.Value = selectType;
        playerType.Value = selectType;
        GameDataServices.Instance.SocketEmitPlayChoose1VsN(selectType);
        gameState.Value = GameState.SelectionComplete;
    }

    public bool isMe(string idUser, string idEnemy)
    {
        Debug.Log($"idUser: {idUser}; idEnemy:{idEnemy}; userId: {userID.Value}");
        if (idUser == userID.Value && idEnemy != userID.Value)
        {
            return true;
        }
        else if (idUser != userID.Value && idEnemy == userID.Value)
        {
            return false;
        }
        else
        {
            showError(Constants.MESSAGE_WRONG_API);
            return false;
        }

    }
    public void showError(string msg)
    {
        isShowDialogError.Value = true;

        if (msg != null)
        {
            msgDialogError.Value = msg;
        }
        else
        {
            msgDialogError.Value = Constants.MESSAGE_ERROR;
        }

    }

    public void ResetGamePVP()
    {
        gameState.Value = GameState.Initializing;
        isGameStart.Value = false;
        isPlayerInPVP.Value = false;
        isChoseModeGame.Value = true;
        enemyID.Value = null;
        modeGame.Value = GameMode.NONE;
        
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
            GameSceneServices.Instance.LoadScene(Constants.MENU_SCENE);
    } 
    public void ResetGameTour()
    {
        gameState.Value = GameState.Initializing;
        isGameStart.Value = false;
        isPlayerInPVP.Value = true;
        isMathFound.Value = false;
        isGameTourStart.Value = false;
        modeGame.Value = GameMode.NONE;
        GameSceneServices.Instance.LoadScene(Constants.MENU_SCENE);
    }
    #endregion
}
