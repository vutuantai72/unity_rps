using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RankingData
{
    public string wallet;
    public int coin;
    public string name;
    public Sprite avatarSprite;
    public string avatar;
}

public class RankingModel
{
    public List<RankingData> data;
    public int total;
}

public enum RankType
{
    Daily = 0,
    Weekly,
    Monthly
}

public class DialogRanking : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject root;
    [SerializeField] private List<RankingContent> gameObjects;
    [SerializeField] private RectTransform contentRankDay;
    [SerializeField] private RectTransform contentRankWeek;
    [SerializeField] private RectTransform contentRankMonth;

    private GameService gameService = GameService.@object;
    private RankType rankType = RankType.Daily;

    private int totalUser;
    public int TotalUser { get => totalUser; private set => totalUser = value; }
    public List<int> PlayerCoin = new List<int>();

    private Dictionary<string, Sprite> dicAvatar = new Dictionary<string, Sprite>();

    [System.Obsolete]
    private void OnEnable()
    {
        root.SetActive(true);
        OnShowDay();
    }

    [System.Obsolete]
    public void OnShow()
    {
        root.SetActive(true);
        OnShowDay();
    }

    public void OnClose()
    {
        root.SetActive(false);
    }

    [System.Obsolete]
    public async void OnShowDay() 
    {       
        await GetRankingList(RankType.Daily);
        UpdateUI(RankType.Daily);

        contentRankWeek.position = new Vector2(contentRankWeek.position.x, 0);
        contentRankMonth.position = new Vector2(contentRankMonth.position.x, 0);

    }

    [System.Obsolete]
    public async void OnShowWeek()
    {       
        await GetRankingList(RankType.Weekly);
        UpdateUI(RankType.Weekly);

        contentRankDay.position = new Vector2(contentRankDay.position.x, 0);
        contentRankMonth.position = new Vector2(contentRankMonth.position.x, 0);
    }

    [System.Obsolete]
    public async void OnShowMonth()
    {       
        await GetRankingList(RankType.Monthly);       
        UpdateUI(RankType.Monthly);

        contentRankWeek.position = new Vector2(contentRankWeek.position.x, 0);
        contentRankMonth.position = new Vector2(contentRankMonth.position.x, 0);
    }

    // Update is called once per frame
    private void UpdateUI(RankType rankType)
    {
        this.rankType = rankType;
        for (int i = 0; i < gameObjects.Count; i++)
        {
            var isActive = i == (int)this.rankType;
            gameObjects[i].gameObject.SetActive(isActive);
            if (isActive)
            {
                gameObjects[i].Show();
            }
        }
    }

    [System.Obsolete]
    private async Task GetRankingList(RankType type)
    {
        await GameDataServices.Instance.GetRankingList(type);
    }
}
