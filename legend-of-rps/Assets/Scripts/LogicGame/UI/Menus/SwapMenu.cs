using Nethereum.Web3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Unity;

public enum SwapDialogElement
{
    Connect = 0,
    Approve,
    Wallet
}

public class SwapMenu : MonoBehaviour
{
    [SerializeField] private RectTransform rootsMenu;
    [SerializeField] private RectTransform swapMenu;
    [SerializeField] private List<GameObject> subDialog;
    [SerializeField] private TMP_Text msgErrorText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text swapToText;
    [SerializeField] private TMP_Text errorCoinText;
    [SerializeField] private Text swapText;
    [SerializeField] private Button btnSwap;
    [SerializeField] private Sprite btnImageNormal;
    [SerializeField] private Sprite btnImageRed;
    [SerializeField] private Image btnSprite;


    private SwapDialogElement dialogElement = SwapDialogElement.Connect;

    private GameService gameService = GameService.@object;

    public delegate void WalletConnectEventHandler(string address);

    private bool isConnectWallet;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("[Start SwapMenu]");
        GameDataServices.Instance.action += WalletConnectActionHandler;
        btnSwap.interactable = false;
        msgErrorText.text = "";

        inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return ValidateInputNumber(addedChar); };
        gameService.isSwitchNetworkDialog.ObserveEveryValueChanged(x => WalletConnect.Instance.isActiveNetworkNotMatch)
          .Subscribe(x =>
          {
              CheckNetwork();
          });
    }

    
    private void CheckNetwork()
    {
        if (WalletConnect.Instance.isActiveNetworkNotMatch == true)
        {
            btnSwap.interactable = true;
            btnSprite.sprite = btnImageRed;
            SwapButtonEvent(SwitchChainSwap, "Switch");
        }
        else
        {
            btnSwap.interactable = false;
            btnSprite.sprite = btnImageNormal;
            SwapButtonEvent(SwapTokenToCoin, "Swap To Coin");
        }
    }

    private void SwapButtonEvent(UnityAction action, string text = null)
    {
        btnSwap.onClick.RemoveAllListeners();
        swapText.text = text;
        btnSwap.onClick.AddListener(action);
    }

    private void OnEnable()
    {
        Debug.Log("[OnEnable]");
        if (string.IsNullOrEmpty(WalletConnect.ActiveSession.Accounts?[0]))
        {
            OnShowConnect();
        }

        if (gameService.isUserApprove.Value == true)
            gameService.isUserApprove.Value = false;
    }

    private char ValidateInputNumber(char addedChar)
    {
        if (char.IsNumber(addedChar) || addedChar.Equals('.'))
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    public void OnShowConnect()
    {
        dialogElement = SwapDialogElement.Connect;
        UpdateUI(dialogElement);
    }
    public void OnShowApprove()
    {
        dialogElement = SwapDialogElement.Approve;
        UpdateUI(dialogElement);
    }
    public void OnShowSwap()
    {
        foreach (var item in subDialog)
        {
            item.SetActive(false);
        }
    }
    public void OnShowWallet()
    {
        dialogElement = SwapDialogElement.Wallet;
        UpdateUI(dialogElement);
    }

    public void OnShow(SwapDialogElement dialogElement)
    {
        rootsMenu.gameObject.SetActive(true);
        UpdateUI(this.dialogElement);
    }

    public void UpdateUI(SwapDialogElement dialogElement)
    {
        for (int i = 0; i < subDialog.Count; i++)
        {
            subDialog[i].SetActive(i == (int)dialogElement);
        }
    }

    public void OnClose()
    {
        rootsMenu.gameObject.SetActive(false);
        foreach (var item in subDialog)
        {
            item.SetActive(false);
        }
    }

    private async void WalletConnectActionHandler()
    {
        Debug.Log($"WalletConnectActionHandler");
        var address = WalletConnect.ActiveSession.Accounts?[0] ?? gameService.userWall.Value;
        Debug.Log($"WalletConnectActionHandler: {address}");

        if (string.IsNullOrEmpty(WalletConnect.ActiveSession.Accounts?[0]))
        {
            OnShowConnect();
            return;
        }

        if (!string.IsNullOrEmpty(address))
        {
            isConnectWallet = true;
            OnShowSwap();
        }
    }

    private async Task CheckApprove()
    {
        await GameDataServices.Instance.GetUserWalletAllowance();
        var allowance = decimal.Parse(gameService.userWalletAllowance.Value);
        if (allowance == 0 || allowance < GameDataServices.Instance.amountApprove)
        {
            await StartCoroutine(ShowApprove());
            gameService.isLoading.Value = false;
        }
    }

    // This code is a little garbage, please fix it later if you have any other solution
    IEnumerator ShowApprove()
    {
        while (CheckApprovePopup() == false)
        {
            OnShowApprove();
            yield return null;
        }
    }

    private bool CheckApprovePopup()
    {
        if (subDialog[1].activeSelf == true)
            return true;
        else return false;
    }

    public void OnChanger(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            GameDataServices.Instance.amountApprove = decimal.Parse(value);
            // catch exception here
            if (CheckSwapAmount())
            {
                btnSwap.interactable = true;
                ShowErrorCoinSwap(false);
            }
            else
            {
                btnSwap.interactable = false;
                ShowErrorCoinSwap(true);
            }
            swapToText.text = $"{GameDataServices.Instance.amountApprove * 10}";
        }
        else
        {
            swapToText.text = "0.0";
            btnSwap.interactable = false;
        }
    }
    private bool CheckSwapAmount()
        => gameService.userBalance.Value == 0 ||
        GameDataServices.Instance.amountApprove == 0 ||
        GameDataServices.Instance.amountApprove > gameService.userBalance.Value ? false : true;

    private void ShowErrorCoinSwap(bool isOn) => errorCoinText.gameObject.SetActive(isOn);

    //Open wallet UI
    public void ConnectWallet()
    {
        GameDataServices.Instance.OnConnectWallet();
    }

    public async void SwapTokenToCoin()
    {
        gameService.isLoading.Value = true;
        btnSwap.interactable = false;
        await CheckApprove();
        if (decimal.Parse(gameService.userWalletAllowance.Value) > 0)
        {
            await GameDataServices.Instance.GetUserWalletBNB();
            var balanceBNB = gameService.userBNBBalance.Value;
            string transaction = string.Empty;
            if (balanceBNB != 0 && balanceBNB >= (decimal)0.001)
            {
                try
                {
                    transaction = await GameDataServices.Instance.OnSwapTokenToCoin(GameDataServices.Instance.amountApprove);
                    gameService.isLoading.Value = false;
                    if (!string.IsNullOrEmpty(transaction))
                    {
                        Debug.LogError($"[SwapTokenToCoin]: {transaction}");
                        gameService.msgDialogTitleError.Value = "Congratulation";
                        gameService.msgDialogError.Value = "Successfully swap token!";
                        gameService.isShowDialogError.Value = true;
                        await StartCoroutine(SwapSuccessPopup());
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("[SwapTokenToCoin] [Error]: " + ex);
                    gameService.msgDialogTitleError.Value = "OOOPS!!";
                    gameService.msgDialogError.Value = "Could not connect to server. Please try again later.";
                    gameService.isShowDialogError.Value = true;
                    gameService.isLoading.Value = false;
                }
                btnSwap.interactable = true;
            }
            else
            {
                gameService.isLoading.Value = false;
                string msgError = "BNB is not enough to make Swap\nYou can buy it in website: http://winery.finance/";
                gameService.msgDialogTitleError.Value = "OOOPS!!";
                gameService.msgDialogError.Value = msgError;
                gameService.isShowDialogError.Value = true;
                btnSwap.interactable = true;
                return;
            }
        }


        Debug.Log("SwapTokenToCoin");
    }

    public async void Approve()
    {
        gameService.isLoading.Value = true;
        await GameDataServices.Instance.GetUserWalletBNB();
        var balanceBNB = gameService.userBNBBalance.Value;
        if (balanceBNB != 0 && balanceBNB >= (decimal)0.001)
        {
            try
            {
                var transaction = await GameDataServices.Instance.OnApproveTransaction();
                gameService.isLoading.Value = false;
                if (!string.IsNullOrEmpty(transaction))
                {
                    OnShowSwap();
                    gameService.isUserApprove.Value = false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("[Approve] [Error]: " + ex);
                gameService.msgDialogTitleError.Value = "OOOPS!!";
                gameService.msgDialogError.Value = "Could not connect to server. Please try again later.";
                gameService.isShowDialogError.Value = true;
                gameService.isLoading.Value = false;
            }
        }
        else
        {
            gameService.isLoading.Value = false;
            string msgError = "BNB is not enough to make Approve\nYou can buy it in website: http://winery.finance/";
            gameService.msgDialogTitleError.Value = "OOOPS!!";
            gameService.msgDialogError.Value = msgError;
            gameService.isShowDialogError.Value = true;
            return;
        }
    }

    public void OnApplicationFocus(bool focus)
    {
        Debug.LogError("[OnApplicationFocus] :" + focus);
        if (focus)
        {
            gameService.isLoading.Value = false;
        }
    }

    public async void SwitchChainSwap()
    {
        await GameDataServices.Instance.OnSwitchEthChain((result) =>
        {
            //if(result != null)
            //    gameService.isSwitchNetworkDialog.Value = false;
            WalletConnect.Instance.isActiveNetworkNotMatch = false;
        });
    }

    public void ConfirmSwappSuccess()
    {
        gameService.userSwapSuccess.Value = false;
    }

    IEnumerator SwapSuccessPopup()
    {
        yield return new WaitForSeconds(3f);
        gameService.userSwapSuccess.Value = true;
    }
}

