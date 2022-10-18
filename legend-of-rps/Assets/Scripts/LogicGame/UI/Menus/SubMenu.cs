using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WalletConnectSharp.Unity;

[System.Serializable]
public enum SubMenuElement
{
    Panel = 0,
    AppInfo,
    AccountSetting,
    Coupon,
    GetFreeCoin,
    RateApp,
    IAP,
    SwapMenu,
    Ranking,
    BuyToken,
    UserNotification,
    Withdraw,
    WalletUI,
    MarketPlace
}

public class SubMenu : MonoBehaviour
{
    private const string MESSAGE_NO_COMING_SOON = "This feature will be coming soon";
    [SerializeField] private RectTransform root;
    [SerializeField] private List<GameObject> subMenuElements;
    [SerializeField] private TMP_Text msgErrorText;
    [SerializeField] private TMP_Text msgBuyTokenErrorText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text swapToText;
    [SerializeField] private AccountSetting account;
    [SerializeField] private TMP_InputField couponCode;
    [SerializeField] private TMP_InputField inputFieldBuyToken;
    [SerializeField] private TMP_Text coinToday;
    [SerializeField] private TMP_Text totalChargeAmountText;
    [SerializeField] SideMenu sideMenu;
    GameService gameService = GameService.@object;
    

    private SubMenuElement menuElement = SubMenuElement.Panel;

    [System.Obsolete]
    private void Start()
    {
        OnCloseAll();
        // GetRankingData();
    }

    public void ShowPanel()
    {
        UpdateUI(SubMenuElement.Panel);
    }
    public void ShowAppInfo()
    {
        UpdateUI(SubMenuElement.AppInfo);
    }
    public void ShowMywallet()
    {
        UpdateUI(SubMenuElement.WalletUI);

        if (string.IsNullOrEmpty(WalletConnect.ActiveSession?.Accounts?[0]))
        {
            gameService.userWall.Value = string.Empty;
        }
    }
    public void ShowAccountSetting()
    {
        UpdateUI(SubMenuElement.AccountSetting);
    }
    public void ShowCoupon()
    {
        UpdateUI(SubMenuElement.Coupon);
    }
    public void ShowGetFreeCoin()
    {
        UpdateUI(SubMenuElement.GetFreeCoin);
    }
    //public void ShowGameMission()
    //{
    //    UpdateUI(SubMenuElement.GameMission);
    //}
    public void ShowRateApp()
    {
        UpdateUI(SubMenuElement.RateApp);
    }
    public void ShowIAP()
    {
        UpdateUI(SubMenuElement.IAP);
    }
    public void ShowSwapMenu()
    {
        UpdateUI(SubMenuElement.SwapMenu);
    }
    public void ShowRanking()
    {
        UpdateUI(SubMenuElement.Ranking);
    }

    public void ShowBuyToken()
    {
        UpdateUI(SubMenuElement.BuyToken);
    }

    public void ShowUserNotification()
    {
        UpdateUI(SubMenuElement.UserNotification);
    }

    public void ShowWithdraw()
    {
        UpdateUI(SubMenuElement.Withdraw);
    }
    
    public void ShowMarketPlace()
    {
        UpdateUI(SubMenuElement.MarketPlace);
    }

    public void OnShowCommingSoon()
    {
        AudioServices.Instance.PlayClickAudio();

        gameService.msgDialogTitleError.Value = "OOOPS!!";
        gameService.msgDialogError.Value = MESSAGE_NO_COMING_SOON;
        gameService.isShowDialogError.Value = true;
    }

    private void UpdateUI(SubMenuElement menuElement)
    {
        sideMenu.CloseSideMenu();
        root.gameObject.SetActive(true);
        AudioServices.Instance.PlayClickAudio();

        this.menuElement = menuElement;
        for (int i = 0; i < subMenuElements.Count; i++)
        {
            subMenuElements[i].SetActive(i == (int)this.menuElement);
        }
    }

    public void OnClickCloseAll()
    {
        AudioServices.Instance.PlayClickAudio();

        msgErrorText.text = string.Empty;
        inputField.text = string.Empty;
        swapToText.text = "0.0";
        for (int i = 0; i < subMenuElements.Count; i++)
        {
            if (i == 12)
            {
                subMenuElements[i].SetActive(true);
            }
            else
            {
                subMenuElements[i].SetActive(false);
            }
        }
    }

    public void OnCloseAll()
    {
        AudioServices.Instance.PlayClickAudio();

        msgErrorText.text = string.Empty;
        inputField.text = string.Empty;
        swapToText.text = "0.0";
        for (int i = 0; i < subMenuElements.Count; i++)
        {
            subMenuElements[i].SetActive(false);
        }
    }

    public void OnClaimCoupon()
    {
        if (!string.IsNullOrEmpty(couponCode.text))
        {
            GameDataServices.Instance.ClaimCoupon(couponCode.text);
        }
    }

    public void OnCloseClaimCouponDialog()
    {
        gameService.isClaimCouponDialog.Value = false;
    }

    [System.Obsolete]
    private async void GetRankingData()
    {
        var result = await GameDataServices.Instance.GetRankingList(RankType.Daily);

        //result.data.ForEach(data =>
        //{
        //    StartCoroutine(GameDataServices.Instance.DownloadImage(data));
        //});
    }
}
