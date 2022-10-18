using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyGiftData
{
    public string id;
    public int created_at;
    public int updated_at;
    public string wallet;
    public string package_name;
    public int day_to_claim;
    public int coin;
}

public class DailyGiftModel
{
    public DailyGiftData[] data;
}

public class DailyReward : MonoBehaviour
{
    GameService gameService = GameService.@object;

    [SerializeField] private RectTransform[] TurnOffUI;

    [SerializeField] private RectTransform DailyMenu;
    [SerializeField] private RectTransform SubMenuUI;
    [SerializeField] private CanvasGroup NotificationSpeaker;
    [SerializeField] private CanvasGroup RoulettePanel;

    [Header("RewardValueText_ButtonReward")]
    [SerializeField] private TextMeshProUGUI rewardValue;
    [SerializeField] private RewardManager reManager;

    #region Tab
    [SerializeField] private RectTransform[] TabReward;
    [SerializeField] private RectTransform claimDaily;
    [SerializeField] private RectTransform claimButton;
    #endregion

    #region DailyRewardUI
    public void OpenSelectUI() //For close daily reward
    {
        for (int i = 0; i < TurnOffUI.Length; i++)
        {
            TurnOffUI[i].gameObject.SetActive(true);
        }

        DailyMenu.gameObject.SetActive(false);
        SubMenuUI.gameObject.SetActive(true);
        NotificationSpeaker.alpha = 1;
        RoulettePanel.alpha = 1;
    }

    public void CloseSelectUI() //For open daily reward
    {
        for (int i = 0; i < TurnOffUI.Length; i++)
        {
            TurnOffUI[i].gameObject.SetActive(false);
        }

        NotificationSpeaker.alpha = 0;
        RoulettePanel.alpha = 0;
        DailyMenu.gameObject.SetActive(true);
        ShowTabGreen();
        AudioServices.Instance.PlayClickAudio();
    }
    #endregion

    #region Switch Tab
    public void ShowTabGreen()
    {
        TabReward[0].gameObject.SetActive(true);
        TabReward[1].gameObject.SetActive(false);
        TabReward[2].gameObject.SetActive(false);
    }

    public void ShowTabBlue()
    {
        TabReward[0].gameObject.SetActive(false);
        TabReward[1].gameObject.SetActive(true);
        TabReward[2].gameObject.SetActive(false);
    }

    public void ShowTabPurple()
    {
        TabReward[0].gameObject.SetActive(false);
        TabReward[1].gameObject.SetActive(false);
        TabReward[2].gameObject.SetActive(true);
    }
    #endregion

    int rewardText = 0;

    #region Claim Reward
    public void OpenPopupClaimReward()
    {
        TurnOffTabDaily();

        claimDaily.gameObject.SetActive(true);
        claimButton.gameObject.SetActive(true);

        Debug.LogError("KEYS: " + gameService.dailyKey.Value);

        if(gameService.dailyKey.Value == 1)
            rewardText = 7;
        else if(gameService.dailyKey.Value == 2)
            rewardText = 10;
        else if(gameService.dailyKey.Value == 3)
            rewardText = 12;
        else if(gameService.dailyKey.Value == 4)
            rewardText = 15;
        else if(gameService.dailyKey.Value == 5)
            rewardText = 20;
        else if(gameService.dailyKey.Value == 6)
            rewardText = 100;
        else
            rewardText = 5;

        rewardValue.text = "+ " + rewardText.ToString() + " Coin";
    }

    public void ClosePopupClaimReward()
    {
        DailyMenu.gameObject.SetActive(true);
        claimDaily.gameObject.SetActive(false);
        ShowTabGreen();
    }

    public void ClaimDaily()
    {
        claimButton.gameObject.SetActive(false);
    }
    #endregion

    #region Turn off UI
    void TurnOffTabDaily()
    {
        TabReward[0].gameObject.SetActive(false);
        TabReward[1].gameObject.SetActive(false);
        TabReward[2].gameObject.SetActive(false);
        DailyMenu.gameObject.SetActive(false);
    }
    #endregion

    public void CheckDailyRewardLogin()
    {
        reManager.GetDailyStatus();
    }
}
