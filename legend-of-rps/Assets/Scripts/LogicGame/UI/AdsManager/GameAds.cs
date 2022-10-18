using Newtonsoft.Json;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UserClaimRewardAds
{
    public string id;
    public long created_at;
    public long updated_at;
    public string email;
    public string wallet;
    public float coin;
    public string name;
    public string avatar;
}

public class ResultClaimRewardAds
{
    public float coin;
    public string wallet;
    public long created_at;
    public long updated_at;
    public string id;
}

public class ClaimRewardAdsData
{
    public UserClaimRewardAds user;
    public ResultClaimRewardAds result;
}

public class ClaimRewardAdsModel
{
    public string msg;
    public float code;
    public ClaimRewardAdsData data;
}

public class GetClaimRewardAdsModel
{
    public ResultClaimRewardAds[] data;
    public int total;
}

public class GameAds : MonoBehaviour, IUnityAdsListener
{
    GameService gameService = GameService.@object;

    [SerializeField] private Button startAds;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private GameObject AdsCountDownTime;
    [SerializeField] private RectTransform AdsRewardPopup;
    [SerializeField] private Animator adsPopup;
    [SerializeField] private Animator anim;

    private static float countDownTime = 15;
    private bool isAdsFinish;
    private bool isPlayerFinishWatchAds;
    private ApiController apiController;

    private void Awake()
    {
        DontDestroyOnLoad(AdsRewardPopup);
    }

    private void Start()
    {
        apiController = new ApiController(new JsonSerializationOption());

        Advertisement.AddListener(this);
        AdsCountDownTime.SetActive(false);
    }

    private void Update()
    {
        if (isAdsFinish)
        {
            StartCoroutine(ShowPopup());
            adsPopup.Play("RewardAdsAnim");
        }

        if (isPlayerFinishWatchAds)
        {
            startAds.interactable = false;
            AdsCountDownTime.SetActive(true);
            if (countDownTime > 0)
            {
                countDownTime -= Time.deltaTime;
            }
            else
            {
                countDownTime = 0;
                AdsCountDownTime.SetActive(false);
                startAds.interactable = true;
                //isAdsFinish = false;
            }
            DisplayTime(countDownTime);
        }
    }

    public void PlayRewardAd()
    {
        if (AdsGameService.isAdsInit)
        {
            if (Advertisement.IsReady("RPS_Android_Rewarded"))
            {
                Advertisement.Show("RPS_Android_Rewarded");
            }
        }
    }

    public void PlayAd()
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (countDownTime == 0)
        {
            Debug.Log("Ad Ready");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("ERROR: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("CLIP STARTED");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "RPS_Android_Rewarded" && showResult == ShowResult.Finished)
        {
            isAdsFinish = true;
        }
    }

    void DisplayTime(float timeDisplay)
    {
        if (timeDisplay < 0)
        {
            timeDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator ShowPopup()
    {
        yield return new WaitForSeconds(0.01f);
        AdsRewardPopup.gameObject.SetActive(true);        
        isAdsFinish = false;
    }

    IEnumerator DisplayRewardAdsCoin()
    {
        yield return new WaitForSeconds(0.5f);
        anim.Play("Play");
        AdsRewardPopup.gameObject.SetActive(false);
    }

    public void ClaimAdsReward()
    {        
        isPlayerFinishWatchAds = true;
        countDownTime = 15;

        PostClaimAdsReward(gameService.userWall.Value);
        StartCoroutine(DisplayRewardAdsCoin());
    }

    #region Interact with data from server

    //private readonly static string host = "https://dev.nftmarble.games/api";
    private readonly static string host = "https://api.rpsgame.world/api";
    private const string Format = "{0}/ads/getAdsLogs?page={1}&limit={2}&email={3}";

    public async void PostClaimAdsReward(string email)
    {
        string url = host + "/ads/claimAdsReward";
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        var result = await apiController.Post<ClaimRewardAdsModel>(url, form);

        if (result != null)
        {
            //gameService.todayCoin.Value = result.data.result.coin;
            gameService.rewardCoin.Value = result.data.result.coin;
            gameService.userCoin.Value = result.data.user.coin;
        }

        

        Debug.LogError("USER_COIN: " + result.data.result.coin);
            
    }

    public async void GetClaimAdsReward(string email)
    {
        string url = string.Format(Format, host, 1, 10, email);

        var result = await apiController.Get<GetClaimRewardAdsModel>(url);
        //var jsonObj = JSON.Parse(result.ToString());

        //var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.ToString());

        Debug.LogError("TEST: " + result.total);
    }
    #endregion
}
