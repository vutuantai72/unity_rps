using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DG.Tweening;
using System.Threading.Tasks;

public class UserClaimDaily
{
    public string id;
    public long created_at;
    public long updated_at;
    public string wallet;
    public float coin;
    public string name;
    public string avatar;
}

public class ResultClaimDaily
{
    public int coin;
    public string package_name;
    public string day_to_claim;
    public string wallet;
    public long created_at;
    public long updated_at;
    public string id;
}

public class ClaimDailyData
{
    public UserClaimDaily user;
    public ResultClaimDaily result;
}

public class ClaimDailyModel
{
    public string msg;
    public float code;
    public ClaimDailyData data;
}

public class RewardManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] checkDaily;
    [SerializeField] private Button[] buttonDaily;
    [SerializeField] private DailyReward daily;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform gameCoin;

    public GameObject coinPrefab;
    public Transform makeTransform;
    public Transform targetTransform;

    public static int coinNumber = 0;
    public static int loginNum = 1;
    public static string nextDayToClaim = "";

    GameService gameService = GameService.@object;
    private static ApiController apiController;
    private ObjectPooler OP;
    private Sequence sequence;
    private int tKey; //From 0 to 6 = Day 1 to Day 7

    [Obsolete]
    private void Start()
    {
        gameService.targetTransform = targetTransform;
        apiController = new ApiController(new JsonSerializationOption());
        OP = ObjectPooler.SharedInstance;
        sequence = DOTween.Sequence();
        StartCoroutine(ReGetDailyStatus());
    }

    public async void GetCoins()
    {
        gameService.isLoading.Value = true;
        await PostClaimDailyGift(gameService.userEmail.Value);
        await StartCoroutine(ReGetDailyStatus());
        gameService.isLoading.Value = false;
        daily.ClaimDaily();
        StartCoroutine(MakeCoinPrefab(20));

    }

    public void ResetReward()
    {
        if (loginNum == 7)
        {
            for (int i = 1; i <= checkDaily.Length; i++)
            {
                for (int j = 0; j < buttonDaily.Length; j++)
                {
                    checkDaily[i].gameObject.SetActive(false);
                    buttonDaily[j].interactable = false;

                    buttonDaily[0].interactable = true;
                    loginNum = 0;
                    coinNumber = 0;
                }
            }
        }
    }

    private IEnumerator MakeCoinPrefab(int length)
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < length; i++)
        {
            gameService.GetCoins();
            GameObject @object = OP.GetPooledObject(coinNumber);
            sequence.Append(@object.transform.DOMove(gameCoin.position, 0.5f)
                .SetEase(Ease.InOutSine)
                .SetDelay(1.5f)
                .OnComplete(() =>
                {
                    animator.Play("Play");
                    @object.SetActive(false);
                }));

            @object.transform.localScale = Vector3.one * 30000;
            @object.transform.localPosition = new Vector3(UnityEngine.Random.Range(-85.0f, 85.0f), 0.0f, -100.0f);
            @object.transform.localRotation = new Quaternion(UnityEngine.Random.Range(0.0f, 180.0f), UnityEngine.Random.Range(0.0f, 180.0f),
                UnityEngine.Random.Range(0.0f, 180.0f), UnityEngine.Random.Range(0.0f, 180.0f));
            @object.SetActive(true);
        }
        yield return null;
    }

    #region Interact with data from server
    //private readonly static string host = "https://dev.nftmarble.games/api";
    //private readonly static string host = "https://api.rpsgame.world/api";
    private const string Format = "{0}/gift/getDailyGiftStatusAndDate?email={1}";

    public async void GetDailyStatus()
    {
        //gameService.dailyKey.Value = tKey
        string url = string.Format(Format, GameDataServices.host, gameService.userEmail.Value);
        UserLoginData result = await apiController.Get<UserLoginData>(url);
        gameService.dicDailyLogin.Value = result.status;
        gameService.listKey.Value = new List<string>(gameService.dicDailyLogin.Value.Keys);
        //if (!gameService.isCurrentDay.Value)
        //{
        //    for (int i = 0; i < gameService.listKey.Value.Count; i++)
        //    {
        //        if (gameService.dicDailyLogin.Value[gameService.listKey.Value[i]])
        //        {
        //        }
        //        else
        //        {
        //            gameService.dailyKey.Value = i;
        //            buttonDaily[gameService.dailyKey.Value].interactable = true;
        //            PlayerPrefs.SetString(GameConstants.KEY_SAVE_DAILY, "");
        //            break;
        //        }
        //    }
        //}

        for (int i = 0; i < gameService.listKey.Value.Count; i++)
        {
            if (gameService.dicDailyLogin.Value[gameService.listKey.Value[i]])
            {

            }
            else
            {
                gameService.dailyKey.Value = i;

                if (gameService.listKey.Value[i] == result.currentDate.ToString())
                {

                    buttonDaily[gameService.dailyKey.Value].interactable = true;
                }
                break;
            }
        }

        for (int i = 0; i < gameService.listKey.Value.Count; i++)
        {   
            if (gameService.dicDailyLogin.Value[gameService.listKey.Value[i]])
            {
                gameService.dailyKey.Value = i;
                gameService.isUserClaimDailyReward.Value = gameService.dicDailyLogin.Value[gameService.listKey.Value[gameService.dailyKey.Value]];
                if (gameService.isUserClaimDailyReward.Value)
                {
                    checkDaily[gameService.dailyKey.Value].gameObject.SetActive(true);
                    buttonDaily[gameService.dailyKey.Value].interactable = false;
                }
                gameService.dailyKey.Value++;
            }
        }
    }

    public async Task PostClaimDailyGift(string email)
    {
        string url = GameDataServices.host + "/gift/claimDailyGift";

        WWWForm form = new WWWForm();
        form.AddField("email", email);

        ClaimDailyModel result = await apiController.Post<ClaimDailyModel>(url, form);

        if(result != null)
        {
            gameService.userCoin.Value = result.data.user.coin;
            gameService.rewardCoin.Value = result.data.result.coin;
        }
    }

    IEnumerator ReGetDailyStatus()
    {
        GetDailyStatus();
        yield return new WaitForSeconds(1f);
    }

    #endregion
}
