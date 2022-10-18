using Nethereum.Web3;
using Nethereum.Util;
using Newtonsoft.Json;
using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Core.Models.Ethereum;
using WalletConnectSharp.Unity;
using static SwapMenu;
using TMPro;
using UnityEngine.Networking;
using System.IO;
using System.Linq;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UniRx;

public class Result
{
    public string wallet;
    public string user_choose;
    public string system_choose;
    public string status;
    public string upper;
    public int value;
    public string code;
    public int coin_used;
    public int win_chain;
    public long created_at;
    public long updated_at;
    public string id;

}

public class MissionResult
{
    public string email;
    public string achievement_id;
    public int reward;
    public int type;
    public long created_at;
    public long updated_at;
    public string id;
}

public class SpinWheelResult
{
    public int reward;
    public int type;
    public long created_at;
    public long updated_at;
    public string id;
    public int wheel_index;
    public string email;
    public string name;
}

public class User
{
    public string id;
    public long created_at;
    public long updated_at;
    public string email;
    public string wallet;
    public float coin;
    public string name;
    public string birth;
    public string gender;
    public string phone;
    public string avatar;
    public float userCurrentCoin;
    public string invite_code;
}

public class StartGameModel
{
    public Result result;
    public User user;
    public JackpotData jackpot;
    public int coinInserted;
    public int winChain;
}

public class ClaimCouponModel
{
    public Result result;
    public User user;
}

public class ClaimCouponResponse
{
    public ClaimCouponModel data;
    public string msg;
    public int code;
}

public class InviteFriendModel
{
    public User data;
    public string msg;
    public int code;
}

public class JackpotModel
{
    public float jackpot;
}
public class JackpotData
{
    public string id;
    public long created_at;
    public long updated_at;
    public float coin;
}
public class LoginModel
{
    public User user;
    public float coinInserted;
    public string accessToken;
    public string msg;
}

public class LoginModelResponse
{
    public LoginModel data;
    public string msg;
    public int code;
}

public class GameDataModel
{
    public string msg;
    public int code;
    public StartGameModel data;
}
public class AddCoinModel
{
    public string msg;
    public float code;
    public User data;
}

public class UpdateCoinModel
{
    public float coin;
}

public class GetUserInfoModel
{
    public User data;
    public string msg;
    public int code;
}

#region Game Mission
public class GameMissionData
{
    public string name;
    public int type;
    public int sub_type;
    public int requirement;
    public int reward;
    public string id;
    public long created_at;
    public long updated_at;
    public int status;
    public int process;
}

public class GameMissionModel
{
    public List<GameMissionData> data;
    public int total;
}

public class ClaimMissionData
{
    public User user;
    public MissionResult result;
}

public class ClaimMissionModel
{
    public string msg;
    public int code;
    public ClaimMissionData data;
}
#endregion

public class SpinWheelModel
{
    public SpinWheelResult result;
    public User user;
    public int currentTurn;
}

public class SpinWheelResponse
{
    public SpinWheelModel data;
    public string msg;
    public int code;
}

public class UpdateGameModel
{
    public string version;
    public string link;
}

#region User Notification
public class UserNotificationData
{
    public string email;
    public string message;
    public decimal coin;
    public int type;
    public string status;
    public string relative_data;
    public string description;
    public string id;
    public long created_at;
    public long updated_at;
}

public class UserNotificationModel
{
    public List<UserNotificationData> data;
    public int total;
}

public class Notification
{
    public string email;
    public string message;
    public int coin;
    public int type;
    public string status;
    public string relative_data;
    public string id;
    public long created_at;
    public long updated_at;
}

public class NotificationData
{
    public User user;
    public Notification notification;
}

public class NotificationModel
{
    public string msg;
    public int code;
    public NotificationData data;
}

#endregion

public class JackpotNotify
{
    public string text;
    public string date;
}

public class WithdrawModel
{
    public TransactionData withdrawConfig;
    public string withdrawId;
}

public class WithdrawResponse
{
    public WithdrawModel data;
    public string msg;
    public int code;
}

public class UpdateWithdrawResponse
{
    public string msg;
    public int code;
}

public class UpdateTimeModel
{
    public int time;
}

#region Market Nft
public class MarketData
{
    public string id;
    public long created_at;
    public long updated_at;
    public int category;
    public int type;
    public int rank;
    public int launch_id;
    public string name;
    public int total_amount;
    public int sold_amount;
    public int price;
    public string token;
    public string image;
}

public class MarketModel
{
    public MarketData[] data;
}

#endregion

#region UserDailyLogin
public class UserLoginData
{
    public Dictionary<string, bool> status;
    public int currentDate;
}

#endregion

public class Info
{
    public string email { get; set; }
    public string wallet { get; set; }
    public string coin { get; set; }
    public string name { get; set; }
    public string birth { get; set; }
    public string gender { get; set; }
    public string phone { get; set; }
    public string avatar { get; set; }
    public string invite_code { get; set; }
    public string id { get; set; }
}

public class Player
{
    public string socket_id { get; set; }
    public Info info { get; set; }
    public string choose { get; set; }
    public string result { get; set; }
    public string coin_result { get; set; }
    public bool is_connect { get; set; }
}

public class DataPVP
{
    public List<Player> players { get; set; }
    public int time { get; set; }
}

public class DataCancelRoom
{
    public int reason { get; set; }
    public string msg { get; set; }
}

public class RoundPlayerList
{
    public List<Player> players { get; set; }
    public int choose_count { get; set; }
}


public class DataTour
{
    public List<RoundPlayerList> roundPlayerList { get; set; }
    public int time { get; set; }
}

public class DataTourInBattle
{
    public RoundPlayerList roundPlayerList { get; set; }
    public int time { get; set; }
}

public class Accept
{
    public string email { get; set; }
    public string id { get; set; }
}

public class DataAcceptList
{
    public int acceptCount { get; set; }
}

#region Get User LRPS Balance / Withdraw Rate policy / User BNB
public class UserLRPSBalance
{
    public string msg;
    public int code;
    public decimal data;
}

public class UserBNBBalance
{
    public string msg;
    public int code;
    public decimal data;
}

public class UserWalletAllowance
{
    public string msg;
    public int code;
    public string data;
}

public class WithdrawRatePolicyData
{
    public int fee;
    public int minimumWithdraw;
    public decimal coinToLRPSRate;
    public int minimumFee;
}

public class WithdrawRatePolicyModel
{
    public string msg;
    public int code;
    public WithdrawRatePolicyData data;
}
#endregion

#region NFT / Coins Airdrop reward
public class MyNFTData
{
    public string id;
    public long created_at;
    public long updated_at;
    public string nft_id;
    public string type;
    public string rank;
    public string name;
    public string media;
    public string media_type;
    public string status;
    public string nft_classify;

}

public class MyNFTList
{
    public MyNFTData[] data;
    public int total;
}

public class NFTRewardData
{
    public int coinReward;
}

public class ClaimNFTAirdrop
{
    public string msg;
    public int code;
    public NFTRewardData data;
}
#endregion

#region Game Mode 1 Vs N
public class LobbyInfoResponse
{
    public int timeLeft;
    public int userCount;
    public LobbyInfoStatus status;
}

public class BuyTicketResponse
{
    public string gameId;
    public int id;
}

public class PlayerInfo
{
    public string avatar;
    public string name;
    public string email;

    public string gameId;
    public string id;
}

public class PlayerInfoResponse
{
    public PlayerInfo info;
}

public class GameStartInfoResponse
{
    public int timeLeft;
    public List<PlayerInfoResponse> playerInfo;
    public string name;
    public string avatar;
    public float coin;
}

public class GameCountdown
{
    public int timeLeft;
}

public class Game1VsNResult
{
    public string result;
    public string yourChoose;
    public string opponentChoose;
}

public enum LobbyInfoStatus
{
    WAITING,
    REGISTER,
    PLAYING,
    END
}
#endregion

[RequireComponent(typeof(SocketIOComponent))]
public class GameDataServices : UnityActiveSingleton<GameDataServices>
{
    public readonly static string host = "https://dev.nftmarble.games/api";
    //public readonly static string host = "https://api-v2.rpsgame.world/api";
    public WalletConnect walletConnect;

    [SerializeField] private SocketIOComponent socket;
    private GameService gameService = GameService.@object;
    private ApiController apiController;

    public decimal amountApprove;

    public event Action action = null;
    public event Action action1vsN = null;
    Menu menu;
    private void Awake()
    {
        GetDailyStatus();
        //CheckUserNotification();
    }

    private void Start()
    {
        apiController = new ApiController(new JsonSerializationOption());

        CheckInternet();
        //gameService.isSwitchNetworkDialog
        //    .Where(_ => walletConnect.isActiveNetworkNotMatch == false)
        //    .Select(_ =>
        //    {
        //        return _;
        //    })
        //    .Subscribe(_ => gameService.isSwitchNetworkDialog.Value = walletConnect.isActiveNetworkNotMatch);

        //Debug.Log($"CHECKING: {walletConnect.isActiveNetworkNotMatch}");
    }

    private void Update()
    {
        CheckInternetConnection();

        // Delete this after testing
        //gameService.isSwitchNetworkDialog.Value = walletConnect.isActiveNetworkNotMatch;
    }

    private void CheckInternet()
    {
        gameService.networkConnect.Value = Application.internetReachability;
        gameService.networkConnect.ObserveEveryValueChanged(_ => Application.internetReachability).
            Subscribe(_ => {
                if(Application.internetReachability == NetworkReachability.NotReachable)
                {
                    gameService.isShowDialogError.Value = true;
                    gameService.msgDialogError.Value = "No internet connection,\n Please check your inernet";
                    gameService.msgDialogTitleError.Value = "OOOPS!!";
                }
                else
                {
                    gameService.isShowDialogError.Value = false;

                    //checK update here
                }
            });
    }
    public async void WalletConnectHandlerAsync(WCSessionData data)
    {
        Debug.Log("Start [WalletConnectHandlerAsync]: " + data.accounts[0]);
        gameService.isLoading.Value = false;
        string address = Web3.ToChecksumAddress(data.accounts[0]);

        if (!string.IsNullOrEmpty(gameService.userName.Value) || !string.IsNullOrEmpty(gameService.accessToken.Value))
        {
            await UpdateInfo(Web3.ToChecksumAddress(address), gameService.userName.Value, gameService.accessToken.Value, callback: (result) =>
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
        }

        await GetUserLRPSBalance();

        action?.Invoke();
    }

    [ContextMenu("Test AddCoin")]
    public async void AddCoin(string email, Action<AddCoinModel> callback = null, int coinUsed = 1)
    {
        var sub = "/game/addCoin";
        var url = $"{host}{sub}";

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("coinUsed", coinUsed);

        AddCoinModel result = await apiController.Post<AddCoinModel>(url, form);

        gameService.userCoin.Value = result.data.userCurrentCoin;

        callback?.Invoke(result);
    }

    [ContextMenu("Test GetTodayCoin")]
    public async void GetTodayCoin(string email)
    {
        var sub = $"/profile/getTodayCoinClaimed?email={email}";
        var url = $"{host}{sub}";

        float result = await apiController.Get<float>(url);

        gameService.todayCoin.Value = result;

        Debug.Log("[GetTodayCoin]: " + result);
    }

    [ContextMenu("Test Login")]
    public async void Login(string email, string access_token, string macAddress = null, string deviceID = null, Action<LoginModelResponse> callback = null) //
    {
        var sub = "/profile/login";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("access_token", access_token);
        var result = await apiController.Post<LoginModelResponse>(url, form, access_token, macAddress, deviceID);

        var jsonObject = new JSONObject();
        jsonObject.AddField("email", $"{email}");

        Debug.Log(jsonObject);
        if (result.code == 0 || gameService.isNetworkError.Value == false)
        {
            StartCoroutine(socketConnect(jsonObject));
            Debug.Log($"Check Socket {socket.IsConnected}");
        }

        callback?.Invoke(result);
    }

    public void OpenSocketConnection()
    {
        if (!socket.IsConnected)
        {
            socket.Connect();
        }
        Debug.Log($"Check Socket LOLO");
    }

    public IEnumerator socketConnect(JSONObject jsonObject)
    {
        socket.On("open", SocketOpen);
        socket.On("error", SocketError);
        socket.On("close", SocketClose);
        socket.On("jackpot", SocketGetJackpot);
        socket.On("updateCoin", SocketUpdateCoin);
        socket.On("notify", SocketJackpotNotify);
        //pvp
        socket.On("initSuccess", SocketInitSuccess);
        socket.On("pvpStart", SocketPvpStart);
        socket.On("pvpResult", SocketPvpResult);
        socket.On("pvpRequestContinue", SocketPvpRequestContinue);
        socket.On("pvpContinue", SocketPvpContinue);
        socket.On("pvpCancelRoom", SocketPvpCancelRoom);
        // tournament
        socket.On("tnmRequestAcceptMatching", SocketTourRequestAcceptMatching);
        socket.On("tnmAcceptList", SocketTourAcceptList);
        socket.On("tnmStartCooldown", SocketTourStartCooldown);
        socket.On("tnmStart", SocketTourStart);
        socket.On("tnmResult", SocketTourResult);
        socket.On("tnmDraw", SocketTourDraw);
        socket.On("tnmCancelRoom", SocketTourCancelRoom);

        //1vsN
        socket.On("lobbyInfo", SocketLobbyInfo1VsN);
        socket.On("playerInfo", SocketPlayerInfo1VsN);
        socket.On("buyTicketResponse", SocketBuyTicketResponse1VsN);
        socket.On("cancelTicketResponse", SocketCancelTicketResponse1VsN);
        socket.On("leaveGameResponse", SocketLeaveTicketResponse1VsN);
        socket.On("chooseResponse", SocketChooseResponse1VsN);
        socket.On("updateGameCountdown", SocketUpdateGameCountdown1VsN);
        socket.On("gameStart", SocketGameStartResponse1VsN);
        socket.On("gameEnd", SocketGameEndResponse1VsN);
        socket.On("gameResult", SocketGameResult1VsN);

        //InvokeRepeating("CheckUserNotification", 3f, 10f);
        //InvokeRepeating("GetDailyStatus", 20f, 30f);
        socket.Emit("init", jsonObject); //
        Debug.Log("SOCKET_OPEN");
        yield return null;
    }

    [ContextMenu("Test Login")]
    public async Task UpdateInfo(string wallet, string name, string accessToken, string birth = null, string gender = null, string phone = null,
        string avatar = null, Action<LoginModel> callback = null)
    {
        var sub = "/profile/updateInfo";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();

        if (!string.IsNullOrEmpty(wallet))
        {
            form.AddField("wallet", wallet);
        }

        if (!string.IsNullOrEmpty(name))
        {
            form.AddField("name", name);
        }

        if (!string.IsNullOrEmpty(birth))
        {
            form.AddField("birth", birth);
        }

        if (!string.IsNullOrEmpty(gender))
        {
            form.AddField("gender", gender);
        }

        if (!string.IsNullOrEmpty(phone))
        {
            form.AddField("phone", phone);
        }

        if (!string.IsNullOrEmpty(avatar))
        {
            form.AddField("avatar", avatar);
        }

        try
        {
            var result = await apiController.Post<LoginModel>(url, form, accessToken);

            if (string.Equals(result.msg, "Wallet address already exist"))
            {
                Debug.Log("[UpdateInfo]: Wallet address already exist");
                await WalletConnect.ActiveSession.Disconnect();
                gameService.msgDialogTitleError.Value = "OOOPS!!";
                gameService.msgDialogError.Value = result.msg;
                gameService.isShowDialogError.Value = true;
            }
            else
            {
                callback?.Invoke(result);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("[UpdateInfo]: " + ex);
        }
    }

    [ContextMenu("Test ClaimMissionReward")]
    public async void ClaimMissonReward(string missionID, string email, Action<ClaimMissionModel> callback = null)
    {
        var sub = "/mission/claimMisionReward";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();
        form.AddField("id", missionID);
        form.AddField("email", email);

        ClaimMissionModel result = await apiController.Post<ClaimMissionModel>(url, form);

        gameService.userCoin.Value = result.data.user.coin;
        callback?.Invoke(result);
    }

    [ContextMenu("Test ClaimNFTAirdropReward")]
    public async void ClaimNFTAirdropReward(string NFT_ID, Action<ClaimNFTAirdrop> callback = null)
    {
        var sub = "/nft/claimAirdrop";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();
        form.AddField("id", NFT_ID);
        ClaimNFTAirdrop result = await apiController.Post<ClaimNFTAirdrop>(url, form);
        callback?.Invoke(result);
    }

    [ContextMenu("Test ClaimNotificationCoin")]
    public async void ClaimNotificationCoin(string notificationID, Action<NotificationModel> callback = null)
    {
        var sub = "/notification/claimReward";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();
        form.AddField("id", notificationID);

        NotificationModel result = await apiController.Post<NotificationModel>(url, form);

        gameService.userCoin.Value = result.data.user.coin;

        callback?.Invoke(result);
    }

    [ContextMenu("Test GetJackpotCoin")]
    public async void GetJackpotCoin()
    {
        try
        {
            var sub = "/game/getJackpot";
            var url = string.Format("{0}{1}", host, sub);
            var result = await apiController.Get<JackpotData>(url);
            if (result != null)
                gameService.jackpotCoin.Value = result.coin;
            Debug.Log("[JackpotCoin]: " + result.coin);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    [ContextMenu("Test GetTodayCoin")]
    public async void GetTodayCoin()
    {
        try
        {
            var sub = $"/profile/getTodayCoinClaimed?wallet={gameService.userEmail}";
            var url = string.Format("{0}{1}", host, sub);
            var result = await apiController.Get<float?>(url);
            if (result == null)
            {
                return;
            }

            gameService.todayCoin.Value = result.Value;
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    [ContextMenu("Test SpinLuckyWheel")]
    public async void SpinLuckyWheel(Action<SpinWheelModel> callBack = null)
    {
        try
        {
            var url = $"{host}/lucky/spinWheel";

            WWWForm form = new WWWForm();
            form.AddField("email", gameService.userEmail.Value);

            var result = await apiController.Post<SpinWheelResponse>(url, form);
            if (result == null)
            {
                return;
            }

            callBack?.Invoke(result.data);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    [ContextMenu("Test SpinTurn")]
    public async void SpinTurn()
    {
        try
        {
            var url = $"{host}/lucky/getSpinTurn?email={gameService.userEmail.Value}";

            var result = await apiController.Get<int>(url);
            if (result == null)
            {
                return;
            }

            gameService.spinTurn.Value = result;
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    [ContextMenu("Test GetUserInfo")]
    public async Task GetUserInfo(Action<GetUserInfoModel> callback = null)
    {
        try
        {
            var sub = $"/profile/getUserInfo?email={gameService.userEmail.Value}";
            var url = string.Format("{0}{1}", host, sub);
            GetUserInfoModel result = await apiController.Get<GetUserInfoModel?>(url);
            if (result == null)
            {
                return;
            }

            gameService.userName.Value = result.data.name;
            gameService.userBirthday.Value = result.data.birth;
            gameService.userGender.Value = result.data.gender;
            gameService.userPhone.Value = result.data.phone;
            gameService.userAvatar.Value = result.data.avatar;
            callback?.Invoke(result);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    public async Task GetMissionList(Action<GameMissionModel> callback = null)
    {
        //var Format = "{0}/mission/getMissionList?email={1}&page={2}&limit={3}&type={4}";

        string url = $"{host}/mission/getMissionList?email={gameService.userEmail.Value}&page=1&limit=10&type=single-play";

        GameMissionModel result = await apiController.Get<GameMissionModel>(url);

        gameService.missionModel.Value = result;
        callback?.Invoke(result);
    }

    public async Task GetMyNFTList(Action<MyNFTList> callback = null)
    {
        // Use this after testing
        //string url = $"{host}/nft/getUserNftInventory?page=1&limit=10&email={gameService.userEmail.Value}";
        string url = $"{host}/nft/getUserNftInventory?page=1&limit=10&email=baoanhiuh.dev@gmail.com";
        MyNFTList result = await apiController.Get<MyNFTList>(url);
        gameService.myNFT.Value = result;
        callback?.Invoke(result);
    }

    public async Task GetNotifications()
    {
        //var Format = "{0}/mission/getMissionList?email={1}&page={2}&limit={3}";

        // Use this after testing
        //string url = $"{host}/notification/getIngameNotification?email={gameService.userEmail.Value}&page=1&limit=10";
        string url = $"{host}/notification/getIngameNotification?email=baoanhiuh.dev@gmail.com&page=1&limit=10";


        UserNotificationModel result = await apiController.Get<UserNotificationModel>(url);

        gameService.notificationModel.Value = result;
    }

    public async Task GetVersionGame(Action<UpdateGameModel> callback = null)
    {
        string url = $"{host}/config/getGameVersion";

        //var result = await apiController.Get<GameVersion>(url);
        //gameService.gameVersion.Value = result.ToString();

        //var url = string.Format("{0}{1}", host, API_GET_GAME_VERSION);
        UpdateGameModel result = await apiController.Get<UpdateGameModel>(url);
        callback?.Invoke(result);

    }

    [ContextMenu("Test Connect Wallet")]
    public void OnConnectWallet()
    {
        Debug.Log("Start connect wallet");

        // Open app metamask.
        walletConnect.OpenDeepLink();
    }

    [ContextMenu("Test Approve Transaction")]
    public async Task<string> OnApproveTransaction()
    {
        string amount = "1000000000000000";
        string senderAddress = Web3.ToChecksumAddress(WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value);

        string url = string.Format(
            Constants.API_REQUEST_ENCODE_SWAP_FORMAT,
            Constants.HOST,
            Constants.RPS_CONTRACT_ADDRESS,
            senderAddress,
            Constants.BC_CONTRACT_ADDRESS,
            amount,
            Constants.C2C_APPROVE_FUNCTION,
            "true");

        ApiController apiController = new ApiController(new JsonSerializationOption());
        TransactionData transactionData = await apiController.Get<TransactionData>(url);
        Debug.Log("[LOG_CAT] [SWAP_API_DATA]: " + JsonConvert.SerializeObject(transactionData));

        string response = await walletConnect.Session.EthSendTransaction(transactionData);

        Debug.Log("[LOG_CAT] [TRANSACTION]: " + response);
        return response;
    }

    [ContextMenu("Test Get data withdraw")]
    public async Task<WithdrawResponse> GetDataWithdraw(string coin)
    {
        string url = $"{host}/withdraw";

        WWWForm form = new WWWForm();
        form.AddField("coin", coin);

        ApiController apiController = new ApiController(new JsonSerializationOption());
        WithdrawResponse withdrawResponse = await apiController.Post<WithdrawResponse>(url, form, gameService.accessToken.Value);
        Debug.Log("[LOG_CAT] [WITHDRAW]: " + JsonConvert.SerializeObject(withdrawResponse));

        return withdrawResponse;
    }

    [ContextMenu("Test Withdraw")]
    public async Task<string> OnWithdraw(TransactionData transactionData)
    {
        string response = await walletConnect.Session.EthSendTransaction(transactionData);

        Debug.Log("[LOG_CAT] [TRANSACTION]: " + response);
        return response;
    }

    [ContextMenu("Test Swap Token To Coin")]
    public async Task<string> OnSwapTokenToCoin(decimal amount = 1)
    {
        try
        {
            string senderAddress = Web3.ToChecksumAddress(WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value);
            Debug.Log("SENDER_ADDRESSS===: " + senderAddress);
            string url = string.Format(
                Constants.API_REQUEST_ENCODE_SWAP_FORMAT,
                Constants.HOST,
                Constants.BC_CONTRACT_ADDRESS,
                senderAddress,
                Constants.RPS_CONTRACT_ADDRESS,
                amount,
                Constants.BC_SWAP_FUNCTION,
                "true");
            Debug.Log("[LOG_CAT] [URL]: " + url);

            ApiController apiController = new ApiController(new JsonSerializationOption());
            TransactionData transactionData = await apiController.Get<TransactionData>(url);
            Debug.Log("[LOG_CAT] [SWAP_API_DATA]: " + JsonConvert.SerializeObject(transactionData));

            //transactionData.chainId = 56;
            //transactionData.chainId = 97;

            string response = await walletConnect.Session.EthSendTransaction(transactionData);

            Debug.Log("[LOG_CAT] [SWAP_DATA_HASH]: " + response);
            return response;
        }
        catch (Exception ex)
        {
            Debug.Log("LOG_SWAP_EXCEPTION: " + ex.Message);
            if (ex.Message.Contains("active chainId is different"))
                gameService.isSwitchNetworkDialog.Value = true;
            throw new ArgumentException();
        }

    }

    [ContextMenu("Test Buy Token")]
    public async Task<string> OnBuyToken(decimal amount = 1)
    {
        try
        {
            string senderAddress = Web3.ToChecksumAddress(WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value);
            string url = string.Format(
                Constants.API_REQUEST_ENCODE_BUY_TOKEN_FORMAT,
                Constants.HOST,
                Constants.RPS_CONTRACT_ADDRESS,
                senderAddress,
                Constants.C2C_CONTRACT_ADDRESS,
                Constants.WETH_ADDRESS,
                amount,
                "true");
            Debug.Log("[LOG_CAT] [URL]: " + url);

            ApiController apiController = new ApiController(new JsonSerializationOption());
            TransactionData transactionData = await apiController.Get<TransactionData>(url);
            Debug.Log("[LOG_CAT] [BUY_TOKEN_API_DATA]: " + JsonConvert.SerializeObject(transactionData));

            //transactionData.chainId = 56;
            //transactionData.chainId = 97;

            string response = await walletConnect.Session.EthSendTransaction(transactionData);

            Debug.Log("[LOG_CAT] [BUY_TOKEN_HASH]: " + response);
            return response;
        }
        catch (Exception ex)
        {
            Debug.Log("LOG_BUY_TOKEN_EXCEPTION: " + ex.Message);
            if (ex.Message.Contains("active chainId is different"))
                gameService.isSwitchNetworkDialog.Value = true;
            throw new ArgumentException();
        }
    }

    [ContextMenu("Test Switch Chain")]
    public async Task OnSwitchEthChain(Action<string> callback = null)
    {
        //var chainId = new HexBigInteger(56);
        var chainId = new HexBigInteger(97);

        var chainIdHex = chainId.HexValue;

        SwitchChainData chainData = new SwitchChainData { chainId = chainIdHex };

        string response = await walletConnect.Session.EthSwitchNetwork(chainData);

        gameService.isSwitchNetworkDialog.Value = false;
        callback.Invoke(response);
        Debug.Log($"RESPONSE_SWITCH_CHAIN_DATA: {chainIdHex}");
    }

    [ContextMenu("Test Get Balance Token")]
    public async void OnGetBalanceToken()
    {
        try
        {
#if UNITY_EDITOR
            var address = "0x57755eb14e34BB7feB50B28425970df470d1EA26";
#else
            var address = Web3.ToChecksumAddress(WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value);
#endif
            var web3 = new Web3(Constants.BSC_JSON_RPC_TESTNET);

            string abi = Constants.C2C_ABI;
            string tokenAddress = Constants.C2C_CONTRACT_ADDRESS;
            var contract = web3.Eth.GetContract(abi, tokenAddress);
            var function = contract.GetFunction("balanceOf");

            // Call function of contract
            object[] param = new object[1] { address };
            var balance = await function.CallAsync<BigInteger>(param);
            var result = Web3.Convert.FromWei(balance);
            gameService.userBalance.Value = result;
            Debug.Log("[OnGetBalanceToken] [Balance]: " + result);
        }
        catch (Exception ex)
        {
            Debug.Log($"[Error] [OnGetBalance]: {ex}");
        }
    }

    [ContextMenu("Test Get Balance Coin")]
    public async void OnGetBalanceCoin()
    {
#if UNITY_EDITOR
        var address = "0x57755eb14e34BB7feB50B28425970df470d1EA26";
#else
            var address = Web3.ToChecksumAddress(WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value);
#endif
        var sub = $"/profile/getTotalCoinHold?wallet={address}";
        var url = string.Format("{0}{1}", host, sub);

        try
        {
            var result = await apiController.Get<float?>(url);
            gameService.userCoin.Value = result.Value;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    [ContextMenu("Test Get Balance Coin")]
    public async void OnGetBNBTokenRate()
    {
        var sub = $"/config/getBNBAndTokenRate";
        var url = string.Format("{0}{1}", host, sub);

        try
        {
            var result = await apiController.Get<decimal?>(url);

            Debug.Log($"[OnGetBNBTokenRate]: {result}");

            gameService.bnbRate.Value = result.Value;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    [ContextMenu("Test Get Balance BNB")]
    public async Task<decimal?> OnGetBalanceBNB()
    {
        try
        {
            Debug.Log($"[Start OnGetBalanceBNB]");
#if UNITY_EDITOR
            var address = "0x57755eb14e34BB7feB50B28425970df470d1EA26";
#else
            var address = Web3.ToChecksumAddress(WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value);
#endif
            var web3 = new Web3(Constants.BSC_JSON_RPC_TESTNET);
            var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
            var result = Web3.Convert.FromWei(balance);
            Debug.Log($"[OnGetBalanceBNB] >>>> : {address}");
            Debug.Log($"[Balance BNB]: {balance} \n {result}");
            return result;
        }
        catch (Exception ex)
        {
            Debug.Log($"[Error] [OnGetBalanceBNB]: {ex}");
            return null;
        }
    }

    [ContextMenu("Test Check User Approved")]
    public async Task<decimal?> OnCheckUserApproved()
    {
        try
        {
#if UNITY_EDITOR
            var address = "0x57755eb14e34BB7feB50B28425970df470d1EA26";
#else
            var address = Web3.ToChecksumAddress(WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value);
#endif
            var web3 = new Web3(Constants.BSC_JSON_RPC_TESTNET);

            string abi = Constants.C2C_ABI;
            string tokenAddress = Constants.BC_CONTRACT_ADDRESS;
            string contractAddress = Constants.RPS_CONTRACT_ADDRESS;
            var contract = web3.Eth.GetContract(abi, contractAddress);
            var function = contract.GetFunction("allowance");

            Debug.Log($"[OnCheckUserApproved] [address]: {address}");
            // Call function of contract
            object[] param = new object[2] { address, tokenAddress };
            var allowanceToken = await function.CallAsync<BigInteger>(param);

            var result = Web3.Convert.FromWei(allowanceToken);

            Debug.Log($"[Allowance]: {result}");

            return result;
        }
        catch (Exception ex)
        {
            Debug.Log($"[Error] [OnCheckUserApproved]: {ex}");

            return null;
        }
    }

    [ContextMenu("GetUserWalletAllowance")]
    public async Task<UserWalletAllowance> GetUserWalletAllowance()
    {
        string url = $"{host}/profile/getUserWalletAllowance?wallet={gameService.userWall.Value}";
        UserWalletAllowance result = await apiController.Get<UserWalletAllowance>(url);

        if (result != null)
            gameService.userWalletAllowance.Value = result.data;

        return result;
    }

    [ContextMenu("GetUserWalletBNB")]
    public async Task<UserBNBBalance> GetUserWalletBNB()
    {
        string url = $"{host}/profile/getUserWalletBNB?wallet={gameService.userWall.Value}";
        UserBNBBalance result = await apiController.Get<UserBNBBalance>(url);

        if (result != null)
            gameService.userBNBBalance.Value = result.data;

        return result;
    }

    [ContextMenu("GetRankingList")]
    public async Task<RankingModel> GetRankingList(RankType rankType)
    {
        string url = $"{host}/game/getRankingList?page=1&limit=10&date={rankType}";
        ApiController apiController = new ApiController(new JsonSerializationOption());
        var result = await apiController.Get<RankingModel>(url);

        gameService.rankingModel.Value = result;

        return result;
    }

    public async Task<UserLRPSBalance> GetUserLRPSBalance()
    {
        string url = $"{host}/profile/getUserWalletBalance?wallet={gameService.userWall.Value}";
        //ApiController apiController = new ApiController(new JsonSerializationOption());
        UserLRPSBalance result = await apiController.Get<UserLRPSBalance>(url);

        if (result != null)
            gameService.userBalance.Value = result.data;

        return result;
    }

    public async Task<WithdrawRatePolicyModel> GetWithdrawRatePolicy()
    {
        string url = $"{host}//withdraw/getWithdrawConfig?email={gameService.userEmail.Value}";
        //ApiController apiController = new ApiController(new JsonSerializationOption());
        WithdrawRatePolicyModel result = await apiController.Get<WithdrawRatePolicyModel>(url);

        if (result != null)
        {
            gameService.withdrawFee.Value = result.data.fee;
            gameService.coinToRPSRate.Value = result.data.coinToLRPSRate;
        }

        return result;
    }

    [Obsolete]
    public IEnumerator DownloadImage(RankingModel data, Action<RankingData> callBackShowUI)
    {
        for (int i = 0; i < data.data.Count; i++)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(data.data[i].avatar);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                data.data[i].avatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new UnityEngine.Vector2(texture.width / 2, texture.height / 2));
                //#if UNITY_EDITOR
                //            string path = $"{Application.dataPath}/Resources/DownloadAssets";
                //#else
                //            string path = $"{Application.persistentDataPath}/Resources/DownloadAssets";
                //#endif
                //            if (!Directory.Exists(path))
                //            {
                //                Directory.CreateDirectory(path);
                //            }

                //            if (!File.Exists($"{path}/{data.wallet}.png"))
                //            {
                //                byte[] itemBGBytes = texture.EncodeToPNG();
                //                File.WriteAllBytes($"{path}/{data.wallet}.png", itemBGBytes);
                //            }
                callBackShowUI.Invoke(data.data[i]);
            }
        }
    }

    [Obsolete]
    public async Task DownloadImageAsync(RankingModel data)
    {
        Dictionary<string, Sprite> dic = new Dictionary<string, Sprite>();
        foreach (var a in data.data)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(a.avatar);

            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                Sprite spriteAvatar = null;
                if (texture != null)
                {
                    spriteAvatar = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new UnityEngine.Vector2(texture.width / 2, texture.height / 2));
                }

                dic.Add(a.wallet, spriteAvatar);
            }
        }
        gameService.dicAvatar.Value = dic;
    }

    [ContextMenu("Test ClaimCoupon")]
    public async void ClaimCoupon(string couponCode)
    {
        var sub = "/coupon/claimCouponReward";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();
        form.AddField("email", gameService.userEmail.Value);
        form.AddField("code", couponCode);

        var result = await apiController.Post<ClaimCouponResponse>(url, form);

        if (result != null)
        {
            if (!string.IsNullOrEmpty(result.msg))
            {
                gameService.msgDialogTitleError.Value = "OOOPS!!";
                gameService.msgDialogError.Value = result.msg;
                gameService.isShowDialogError.Value = true;
            }
            else
            {
                Debug.Log("[ClaimCoupon]: " + result.data.user.coin);
                gameService.userCoin.Value = result.data.user.coin;

                gameService.isClaimCouponDialog.Value = true;
            }
        }
    }

    [ContextMenu("Test GetMarketNft")]
    public async Task<MarketModel> GetMarketNft()
    {
        var sub = "/market/getNftList?page={2}&limit={3}";
        var url = string.Format("{0}{1}{2}{3}", host, sub, 1, 10);

        MarketModel result = await apiController.Get<MarketModel>(url);

        if (result != null)
        {
            gameService.marketModel.Value = result;
        }

        return result;
    }

    [ContextMenu("Test InputInviteCode")]
    public async void InputInviteCode(string inviteCode)
    {
        var sub = "/friend/enterInviteCode";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();
        form.AddField("code", inviteCode);

        InviteFriendModel result = await apiController.Post<InviteFriendModel>(url, form, gameService.accessToken.Value);
        var serverCode = result.data.invite_code;

        if (result != null)
        {
            if (!string.Equals(inviteCode, result.data.invite_code))
            {
                gameService.msgDialogTitleError.Value = "Error!!";
                gameService.msgDialogError.Value = result.msg;
                gameService.isShowDialogError.Value = true;
            }
            else
            {
                gameService.msgDialogTitleError.Value = "Successful!!";
                gameService.msgDialogError.Value = "Your input invite code is valid";
                gameService.isShowDialogError.Value = true;

            }
        }
    }

    [ContextMenu("Test UpdateHashWithdraw")]
    public async Task<UpdateWithdrawResponse> UpdateHashWithdraw(string transactionHash, string idHash)
    {
        var sub = "/withdraw/updateWithdrawHash";
        var url = string.Format("{0}{1}", host, sub);

        WWWForm form = new WWWForm();
        form.AddField("id", idHash);
        form.AddField("hash", transactionHash);

        var result = await apiController.Post<UpdateWithdrawResponse>(url, form);

        return result;
    }

    public void Logout()
    {
        socket.Close();
    }

    public void SocketOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void SocketError(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void SocketClose(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }
    public void SocketGetJackpot(SocketIOEvent e)
    {
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<JackpotModel>(gameService.jsonJackpot.Value);
        if (result != null)
        {
            gameService.jackpotCoin.Value = result.jackpot;
            Debug.Log("[SocketGetJackpot]: " + result.jackpot);
        }
    }
    private void SocketUpdateCoin(SocketIOEvent e)
    {
        Debug.Log($"Socket Swap Token: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<UpdateCoinModel>(gameService.jsonCoin.Value);
        Debug.Log($"Socket Swap Token: {result.coin}");
        if (result != null)
        {
            gameService.userCoin.Value = result.coin;
        }
    }
    public void SocketJackpotNotify(SocketIOEvent e)
    {
        Debug.Log($"Socket Jackpot Notify: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<JackpotNotify>(e.data.ToString());
        if (result != null)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(double.Parse(result.date)).ToLocalTime();
            gameService.jackpotNotify.Value = $"{result.text} {dateTime}";
        }
    }

    public void SocketInitSuccess(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] Socket Init Success: {e.data}");
        // ISerializationOption serializationOption = new JsonSerializationOption();
        // var result = serializationOption.Deserialize<string>(e.data.ToString());
    }

    private void SocketPvpStart(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketPvpStart: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataPVP>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketPvpStart: {result}");
        if (result != null)
        {
            int idPlayer;
            int idEnemy;
            if (gameService.isMe(result.players[0].info.id, result.players[1].info.id))
            {
                idPlayer = 0;
                idEnemy = 1;
            }
            else
            {
                idPlayer = 1;
                idEnemy = 0;
            }
            gameService.userName.Value = result.players[idPlayer].info.name;
            gameService.userAvatar.Value = result.players[idPlayer].info.avatar;
            gameService.userCoin.Value = (float)(Math.Round(float.Parse(result.players[idPlayer].info.coin), 0));
            gameService.enemyName.Value = result.players[idEnemy].info.name;
            gameService.enemyEmail.Value = result.players[idEnemy].info.email;
            gameService.enemyAvatar.Value = result.players[idEnemy].info.avatar;
            gameService.enemyCoin.Value = (float)(Math.Round(float.Parse(result.players[idEnemy].info.coin), 0));
            gameService.timeBet.Value = result.time;
            if (result.players[idEnemy].info.id == gameService.enemyID.Value)
            {
                Debug.Log(545454);
                gameService.gameState.Value = GameState.Selecting;
            }
            else
            {
                Debug.Log(3535);
                gameService.enemyID.Value = result.players[idEnemy].info.id;
                gameService.gameState.Value = GameState.Initializing;
            }
            gameService.isGameStart.Value = true;
            GameSceneServices.Instance.LoadScene(Constants.PVP_SCENE);
        }
        else
        {
            gameService.showError(Constants.MESSAGE_WRONG_API);
        }
    }

    private void SocketPvpResult(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketPvpResult: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataPVP>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketPvpResult: {result}");
        SelectType playerChoose;
        SelectType enemyChoose;
        if (result != null)
        {
            int idPlayer;
            int idEnemy;
            if (gameService.isMe(result.players[0].info.id, result.players[1].info.id))
            {
                idPlayer = 0;
                idEnemy = 1;
            }
            else
            {
                idPlayer = 1;
                idEnemy = 0;
            }
            if (
                Enum.TryParse<SelectType>(
                    (result.players[idPlayer].choose).ToLower(),
                    out playerChoose
                )
            )
            {
                gameService.playerType.Value = playerChoose;
            }
            if (
                Enum.TryParse<SelectType>(
                    (result.players[idEnemy].choose).ToLower(),
                    out enemyChoose
                )
            )
            {
                gameService.enemyType.Value = enemyChoose;
            }
            gameService.enemyCoin.Value = (float)(Math.Round(float.Parse(result.players[idEnemy].info.coin), 0));
            gameService.userCoin.Value = (float)(Math.Round(float.Parse(result.players[idPlayer].info.coin), 0));
            gameService.resultStatus.Value = result.players[idPlayer].result;
            gameService.coinPlayerResult.Value = result.players[idPlayer].coin_result;
            gameService.coinEnemyResult.Value = result.players[idEnemy].coin_result;
            gameService.gameState.Value = GameState.Result;
            if (gameService.resultStatus.Value == Constants.DRAW)
            {
                gameService.notifyEndGame.Value = Constants.NOTIFYGAMESTART;
            }
            else
            {
                gameService.notifyEndGame.Value = Constants.NOTIFYWAITCONTINUE;
            }
        }
    }

    private void SocketPvpRequestContinue(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketPvpRequestContinue: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<UpdateTimeModel>(e.data.ToString());
        Debug.Log($"[SocketIO]SocketPvpRequestContinue: {result.time}");
        if (result != null)
        {
            gameService.gameState.Value = GameState.EndGame;
            gameService.timeWait.Value = result.time;
        }
    }

    private void SocketPvpContinue(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketPvpContinue: {e.data}");
        // ISerializationOption serializationOption = new JsonSerializationOption();
        // var result = serializationOption.Deserialize<DataPVP>(e.data.ToString());
        // Debug.Log($"[SocketIO] SocketPvpContinue: {result}");
        // if (result != null)
        // {
        GameSceneServices.Instance.LoadScene(Constants.PVP_SCENE);
        // }
    }

    private void SocketPvpCancelRoom(SocketIOEvent e)
    {
        ISerializationOption serializationOption = new JsonSerializationOption();
        gameService.dataCancelRoom.Value = serializationOption.Deserialize<DataCancelRoom>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketPvpCancelRoom: {gameService.dataCancelRoom.Value.reason}");
        if (gameService.dataCancelRoom.Value != null)
        {
            if (gameService.dataCancelRoom.Value.reason == 1)
                return;

            switch (gameService.dataCancelRoom.Value.reason)
            {
                case (int)ReasonMode.OUTOFCOIN:
                    Debug.Log($"[SocketIO] TEST: {123456}");
                    gameService.isShowLoadingWaiting.Value = false;
                    gameService.isLoading.Value = false;
                    gameService.isPlayerIn1vs1.Value = false;
                    gameService.ResetGamePVP();
                    gameService.showError(gameService.dataCancelRoom.Value.msg);
                    break;
                case (int)ReasonMode.MATCHINGTIMEOUT:
                    gameService.isShowLoadingWaiting.Value = false;
                    gameService.isLoading.Value = false;
                    gameService.isPlayerInPVP.Value = true;
                    gameService.showError(gameService.dataCancelRoom.Value.msg);
                    break;
                case (int)ReasonMode.CONTINUETIMEOUT:
                    gameService.showError(gameService.dataCancelRoom.Value.msg);
                    break;
                default:
                    gameService.ResetGamePVP();
                    gameService.showError(gameService.dataCancelRoom.Value.msg);
                    break;
            }
        }
    }

    /* Tournament Listen*/
    private void SocketTourRequestAcceptMatching(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketTourRequestAcceptMatching: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataTour>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketTourRequestAcceptMatching: {result}");
        if (result != null)
        {
            gameService.isShowLoadingWaiting.Value = false;
            gameService.isMathFound.Value = true;
        }
    }

    private void SocketTourAcceptList(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketTourAcceptList: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataAcceptList>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketTourAcceptList: {result}");
        if (result != null)
        {
            gameService.listUserAcceptTour.Value = result.acceptCount;
        }
    }

    private void SocketTourStartCooldown(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketTourStartCooldown: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataTour>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketTourStartCooldown: {result}");
        if (result != null)
        {

            gameService.numberOfPlayerBattle.Value = result.roundPlayerList.Count * 2;
            if (gameService.numberOfPlayerBattle.Value == 2)
            {
                gameService.isFinal.Value = true;
            }
            else
            {
                gameService.isFinal.Value = false;
            }
            List<string> AvatarList = new List<string>();
            List<string> NameList = new List<string>();
            foreach (var roundPlayerList in result.roundPlayerList)
            {
                foreach (var players in roundPlayerList.players)
                {
                    AvatarList.Add(players.info.avatar);
                    NameList.Add(players.info.name);
                }
            }
            gameService.playerAvatarList.Value = AvatarList.ToList();
            gameService.playerNameList.Value = NameList.ToList();
            gameService.gameState.Value = GameState.Initializing;
            gameService.isGameTourStart.Value = true;
            GameSceneServices.Instance.LoadScene(Constants.PVP_SCENE);
        }
    }

    private void SocketTourStart(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketTourStart: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataTourInBattle>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketTourStart: {result}");

        if (result != null)
        {
            var dataPlayer = result.roundPlayerList.players;
            int idPlayer;
            int idEnemy;
            if (gameService.isMe(dataPlayer[0].info.id, dataPlayer[1].info.id))
            {
                idPlayer = 0;
                idEnemy = 1;
            }
            else
            {
                idPlayer = 1;
                idEnemy = 0;
            }
            gameService.userName.Value = dataPlayer[idPlayer].info.name;
            gameService.userAvatar.Value = dataPlayer[idPlayer].info.avatar;
            gameService.userCoin.Value = (float)(
                Math.Round(float.Parse(dataPlayer[idPlayer].info.coin), 0)
            );
            gameService.enemyName.Value = dataPlayer[idEnemy].info.name;
            gameService.enemyAvatar.Value = dataPlayer[idEnemy].info.avatar;
            gameService.enemyCoin.Value = (float)(
                Math.Round(float.Parse(dataPlayer[idEnemy].info.coin), 0)
            );
            gameService.timeBet.Value = (float)result.time;
            gameService.isMathFound.Value = false;
            gameService.gameState.Value = GameState.Selecting;
            gameService.isGameStart.Value = true;
            gameService.numberOfPlayerBattle.Value = -1;
        }
    }

    private void SocketTourResult(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketTourResult: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataTourInBattle>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketTourResult: {result}");
        SelectType playerChoose;
        SelectType enemyChoose;
        if (result != null)
        {
            var dataPlayer = result.roundPlayerList.players;
            int idPlayer;
            int idEnemy;
            if (gameService.isMe(dataPlayer[0].info.id, dataPlayer[1].info.id))
            {

                idPlayer = 0;
                idEnemy = 1;
            }
            else
            {
                idPlayer = 1;
                idEnemy = 0;
            }
            if (
                Enum.TryParse<SelectType>(
                    (dataPlayer[idPlayer].choose).ToLower(),
                    out playerChoose
                )
            )
            {
                gameService.playerType.Value = playerChoose;
            }
            if (
                Enum.TryParse<SelectType>(
                    (dataPlayer[idEnemy].choose).ToLower(),
                    out enemyChoose
                )
            )
            {
                gameService.enemyType.Value = enemyChoose;
            }
            gameService.enemyCoin.Value = (float)(
                Math.Round(float.Parse(dataPlayer[idEnemy].info.coin), 0)
            );
            gameService.userCoin.Value = (float)(
                Math.Round(float.Parse(dataPlayer[idPlayer].info.coin), 0)
            );
            gameService.resultStatus.Value = dataPlayer[idPlayer].result;
            gameService.coinPlayerResult.Value = dataPlayer[idPlayer].coin_result;
            gameService.coinEnemyResult.Value = dataPlayer[idEnemy].coin_result;
            gameService.gameState.Value = GameState.Result;
            gameService.isGameStart.Value = false;
            gameService.timeBet.Value = 0;
            gameService.timeWait.Value = result.time - 3000;
            if (gameService.resultStatus.Value == Constants.DRAW)
            {
                gameService.notifyEndGame.Value = Constants.NOTIFYGAMESTART;
            }
            else if (gameService.resultStatus.Value == Constants.WIN)
            {
                gameService.notifyEndGame.Value = Constants.NOTIFYWAITENEMY;
            }
            else
            {
                gameService.notifyEndGame.Value = Constants.NOTIFYBACKTOMENU;
            }
        }
        else
        {
            gameService.showError(Constants.MESSAGE_WRONG_API);
        }
    }

    private void SocketTourDraw(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketTourDraw: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<UpdateTimeModel>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketTourDraw: {result}");
        if (result != null)
        {
            gameService.timeWait.Value = result.time;
            gameService.gameState.Value = GameState.EndGame;
        }
    }

    private void SocketTourCancelRoom(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketTourCancelRoom: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<DataCancelRoom>(e.data.ToString());
        Debug.Log($"[SocketIO] SocketTourCancelRoom: {result}");
        if (result != null)
        {
            switch (result.reason)
            {

                case (int)ReasonModeTour.OUTOFCOIN:
                    gameService.ResetGamePVP();
                    gameService.isShowLoadingWaiting.Value = false;
                    gameService.isLoading.Value = false;
                    gameService.isMathFound.Value = false;
                    gameService.isPlayerInPVP.Value = true;
                    gameService.showError(result.msg);
                    break;
                case (int)ReasonModeTour.MATCHINGTIMDENIED:
                case (int)ReasonModeTour.MATCHINGTIMEOUT:
                    gameService.isShowLoadingWaiting.Value = false;
                    gameService.isLoading.Value = false;
                    gameService.isMathFound.Value = false;
                    gameService.isPlayerInPVP.Value = true;
                    gameService.showError(result.msg);
                    break;
                default:
                    gameService.ResetGamePVP();
                    gameService.showError(result.msg);
                    break;
            }

        }

    }


    [ContextMenu("SocketIO Emit")]
    public void SocketEmitInit()
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("email", $"{gameService.userEmail.Value}");
        // jsonObject.AddField("wallet", $"{address}");
        Debug.Log("[SocketIO] init: " + jsonObject);
        socket.Emit("init", jsonObject);
    }

    public void SocketEmitGameMode(GameMode mode)
    {
        // var jsonObject = new JSONObject();
        // jsonObject.AddField("data", $"{(int)mode}");
        Debug.Log("[SocketIO] selectGameMode: " + (int)mode);
        socket.Emit("selectGameMode", (int)mode + "");
    }

    public void SocketEmitPvPMatching(int coinBet)
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("data", $"{coinBet}");
        jsonObject.AddField("email", $"{gameService.userEmail.Value}");
        Debug.Log("[SocketIO] pvpMatching: " + coinBet);
        socket.Emit("pvpMatching", coinBet + "");
        socket.Emit("init", jsonObject);
        //StartCoroutine(socketConnect(jsonObject));
    }

    public void SocketEmitPlayChoose(SelectType selectType)
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] pvpUserChoose: " + selectType);
        socket.Emit("pvpUserChoose", selectType.ToString());
    }

    public void SocketEmitPvpAcceptContinue()
    {
        var jsonObject = new JSONObject();
        // jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] pvpAcceptContinue: " + jsonObject);
        socket.Emit("pvpAcceptContinue", "");
    }

    public void SocketEmitPvPLeaveRoom()
    {
        var jsonObject = new JSONObject();
        // jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] pvpLeaveRoom: " + jsonObject);
        socket.Emit("pvpLeaveRoom", "");//
    }

    /* tournament emit*/
    public void SocketEmitTourMatching()
    {
        var jsonObject = new JSONObject();
        // jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] tnmMatching: " + jsonObject);
        socket.Emit("tnmMatching", "");
    }

    public void SocketEmitTourAcceptMatching()
    {
        var jsonObject = new JSONObject();
        // jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] tnmAcceptMatching: " + jsonObject);
        socket.Emit("tnmAcceptMatching", "");
    }

    public void SocketEmitTourDenyMatching()
    {
        var jsonObject = new JSONObject();
        // jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] tnmDenyMatching: " + jsonObject);
        socket.Emit("tnmDenyMatching", "");
    }

    public void SocketEmitTourPlayChoose(SelectType selectType)
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] tnmUserChoose: " + selectType);
        socket.Emit("tnmUserChoose", selectType.ToString().ToUpper());
    }

    public void SocketEmitTourCancelMatching()
    {
        var jsonObject = new JSONObject();
        // jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] tnmCancelMatching: " + jsonObject);
        socket.Emit("tnmCancelMatching", "");
    }


    #region Game Mode 1 vs N
    public void SocketEmitPlayChoose1VsN(SelectType selectType)
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("data", $"{selectType}");
        Debug.Log("[SocketIO] choose: " + selectType);
        socket.Emit("choose", selectType.ToString());
    }

    public void SocketEmitBuyTicket1VsN()
    {
        var jsonObject = new JSONObject();
        Debug.Log("[SocketIO] buyTicket: " + jsonObject);
        socket.Emit("buyTicket", "");
    }

    public void SocketEmitCancelTicket1VsN()
    {
        var jsonObject = new JSONObject();
        Debug.Log("[SocketIO] cancelTicket: " + jsonObject);
        socket.Emit("cancelTicket", "");
    }

    public void SocketEmitLeaveTicket1VsN()
    {
        var jsonObject = new JSONObject();
        Debug.Log("[SocketIO] leaveGame: " + jsonObject);
        socket.Emit("leaveGame", "");
    }

    private void SocketBuyTicketResponse1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketBuyTicketResponse1VsN: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<BuyTicketResponse>(e.data.ToString());
        Debug.Log($"[SocketIO]SocketBuyTicketResponse1VsN: {result.gameId} {result.id}");
    }

    private void SocketCancelTicketResponse1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketCancelTicketResponse1VsN: {e.data}");
    }

    private void SocketLeaveTicketResponse1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketLeaveTicketResponse1VsN: {e.data}");
    }

    private void SocketChooseResponse1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketChooseResponse1VsN: {e.data}");
    }

    private void SocketGameStartResponse1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketGameStartResponse1VsN: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<GameStartInfoResponse>(e.data.ToString());
        Debug.Log($"[SocketIO]SocketGameStartResponse1VsN: {result.timeLeft.ToString()}");
        gameService.isShow1vsNMatchingStart.Value = true;
        gameService.gameState.Value = GameState.Selecting;
        if (result.playerInfo != null)
        {
            gameService.userAvatar.Value = result.playerInfo.FirstOrDefault(x => x.info.email.Equals(gameService.userEmail.Value)).info.avatar;
            gameService.enemyAvatar.Value = result.playerInfo.FirstOrDefault(x => !x.info.email.Equals(gameService.userEmail.Value)).info.avatar;
        }
        StartCoroutine(Show1vsNLoading());
        gameService.isGameStart.Value = true;
    }

    private void SocketGameEndResponse1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketGameEndResponse1VsN: {e.data}");
    }

    public void SocketLobbyInfo1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketLobbyInfo1VsN: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<LobbyInfoResponse>(e.data.ToString());
        Debug.Log($"[SocketIO]SocketLobbyInfo1VsN: {result.status.ToString()}");
        gameService.isActiveBuyTicketButton.Value = result.timeLeft <= 60;

        action1vsN?.Invoke();
    }

    public void SocketPlayerInfo1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketPlayerInfo1VsN: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<PlayerInfoResponse>(e.data.ToString());
        Debug.Log($"[SocketIO]SocketPlayerInfo1VsN: {result.info}");
    }

    public void SocketUpdateGameCountdown1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketUpdateGameCountdown1VsN: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<GameCountdown>(e.data.ToString());
        Debug.Log($"[SocketIO]SocketUpdateGameCountdown1VsN: {result.timeLeft}");
        gameService.timeBet.Value = (float)result.timeLeft;
    }

    public void SocketGameResult1VsN(SocketIOEvent e)
    {
        Debug.Log($"[SocketIO] SocketGameResult1VsN: {e.data}");
        ISerializationOption serializationOption = new JsonSerializationOption();
        var result = serializationOption.Deserialize<Game1VsNResult>(e.data.ToString());
        Debug.Log($"[SocketIO]SocketGameResult1VsN: {result.result}");
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
    #endregion

    public async Task StartGame(string selected, Action<GameDataModel> callbackStartGame = null)
    {
        if (!gameService.isGameStart.Value)
        {
            gameService.isGameStart.Value = true;

            var sub = "/game/startGame";
            var url = string.Format("{0}{1}", host, sub);
            var apiController = new ApiController(new JsonSerializationOption());

            WWWForm form = new WWWForm();
            form.AddField("email", gameService.userEmail.Value);
            form.AddField("userChoose", selected.ToUpper());
            form.AddField("coinUsed", 1);
            GameDataModel result = await apiController.Post<GameDataModel>(url, form);
            callbackStartGame?.Invoke(result);
        }
    }

    public async void CheckUserNotification()
    {
        await GetNotifications();

        var notificationData = gameService.notificationModel.Value;

        if (notificationData.total == 0)
        {
            gameService.isNotifiEmpty.Value = true;
        }
        else if (notificationData.total > 0)
        {
            gameService.isNotifiEmpty.Value = false;
            for (int i = 0; i < notificationData.total; i++)
            {
                if (Convert.ToBoolean(notificationData.data[i].status) == false && notificationData.data[i].type != 2)
                {
                    gameService.isUserHasNotifications.Value = true;
                }
            }
        }
    }

    public async void GetDailyStatus()
    {
        //string host = "https://dev.nftmarble.games/api";
        //string Format = "{0}/gift/getDailyGiftStatus?email={1}";

        //string url = string.Format(Format, host, gameService.userEmail.Value);

        //var result = await apiController.Get<object>(url);
        //gameService.dicDailyLogin.Value = JsonConvert.DeserializeObject<Dictionary<string, bool>>(result.ToString());

        //gameService.listKey.Value = new List<string>(gameService.dicDailyLogin.Value.Keys);

        //var curDay = DateTime.Now;
        //var preDay = PlayerPrefs.GetString(GameConstants.KEY_SAVE_DAILY, "");

        //Debug.Log(curDay);
        //Debug.Log(preDay);

        //if (string.IsNullOrEmpty(preDay))
        //{
        //    PlayerPrefs.SetString(GameConstants.KEY_SAVE_DAILY, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        //}
        //else
        //{
        //    DateTime prevDate = DateTime.Parse(preDay);
        //    if (curDay.Day != prevDate.Day)
        //    {
        //        gameService.isCurrentDay.Value = false;
        //    }
        //    else
        //    {
        //        gameService.isCurrentDay.Value = true;
        //    }
        //}
    }

    public async void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (gameService.isNetworkError.Value == false)
            {
                gameService.isNetworkError.Value = true;
                HandleConnectionError(gameService.isNetworkError.Value);
            }
        }
        else
        {
            gameService.isNetworkError.Value = false;
            if (string.Equals(Application.version, GameService.@object.gameVersion.Value))
            {
                GameService.@object.isGameVersionCompatible.Value = true;
            }
            else
                GameService.@object.isGameVersionCompatible.Value = false;
            //OpenSocketConnection();
        }
    }

    public async void HandleConnectionError(bool isConnectError)
    {
        if (isConnectError)
        {
            gameService.isShowDialogError.Value = true;
            gameService.msgDialogTitleError.Value = " Connection Error!!";
            gameService.msgDialogError.Value = Constants.MESSAGE_NO_INTERNET;


            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            {
                gameService.isChoseModeGame.Value = false;
            }
            GoogleSignIn.Instance.ErrorConnectionSignOut();
        }
    }
}
