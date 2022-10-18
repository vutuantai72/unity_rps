using Nethereum.Web3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Unity;

public class BuyToken : MonoBehaviour
{
    [SerializeField] private RectTransform rootsMenu;
    [SerializeField] private RectTransform connectWalletUI;
    [SerializeField] private RectTransform myWalletUI;
    [SerializeField] private TMP_Text msgErrorText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text coinToday;
    [SerializeField] private TMP_Text totalChargeAmountText;
    [SerializeField] private Button btnBuyToken;

    private GameService gameService = GameService.@object;

    private decimal amountToken;

    // Start is called before the first frame update
    private void Start()
    {
        GameDataServices.Instance.action += WalletConnectActionBuyTokenHandler;
        GameDataServices.Instance.OnGetBNBTokenRate();
        btnBuyToken.interactable = false;
        msgErrorText.text = "";

        inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return ValidateInputNumber(addedChar); };
    }

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(WalletConnect.ActiveSession.Accounts?[0]))
        {
            connectWalletUI.gameObject.SetActive(true);
        }
        else
        {
            connectWalletUI.gameObject.SetActive(false);
        }
    }

    private async void WalletConnectActionBuyTokenHandler()
    {
        Debug.Log($"WalletConnectActionHandler Buy Token");
        Debug.Log($"WalletConnectActionHandler Buy Token: {WalletConnect.ActiveSession.Accounts?[0]}");

        if (!string.IsNullOrEmpty(WalletConnect.ActiveSession.Accounts?[0]))
        {
            connectWalletUI.gameObject.SetActive(false);
        }
    }

    private char ValidateInputNumber(char addedChar)
    {
        if (char.IsNumber(addedChar))
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    public void OnChanger(string value)
    {
        Debug.LogError("[OnChanger] [Buy Token]: "+value);
        if (!string.IsNullOrEmpty(value))
        {
            amountToken = decimal.Parse(value);
            btnBuyToken.interactable = true;
            coinToday.text = $"+{value}";
            totalChargeAmountText.text = $"{gameService.bnbRate.Value * amountToken}";
        }
        else
        {
            coinToday.text = "0";
            totalChargeAmountText.text = "0.0";
            btnBuyToken.interactable = false;
        }
    }

    public async void BuyTokenAsync()
    {
        gameService.isLoading.Value = true;
        await GameDataServices.Instance.GetUserWalletBNB();
        var balanceBNB = gameService.userBNBBalance.Value;
        string transaction = string.Empty;
        decimal totalCharge = gameService.bnbRate.Value * amountToken;
        if (balanceBNB != 0 && balanceBNB > totalCharge)
        {
            gameService.isLoading.Value = false;
            try
            {
                transaction = await GameDataServices.Instance.OnBuyToken(amountToken);
                if (!string.IsNullOrEmpty(transaction))
                {
                    Debug.LogError($"[BuyTokenAsync]: {transaction}");
                    gameService.msgDialogTitleError.Value = "Congratulation";
                    gameService.msgDialogError.Value = "Successfully buy token!";
                    gameService.isShowDialogError.Value = true;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("[Withdraw] [Error]: " + ex);
                gameService.msgDialogTitleError.Value = "OOOPS!!";
                gameService.msgDialogError.Value = "Could not connect to server. Please try again later.";
                gameService.isShowDialogError.Value = true;
                gameService.isLoading.Value = false;
            }
        }
        else
        {
            gameService.isLoading.Value = false;
            string msgError = "BNB is not enough to make buy\nYou can buy it in website: http://winery.finance/";
            gameService.msgDialogTitleError.Value = "OOOPS!!";
            gameService.msgDialogError.Value = msgError;
            gameService.isShowDialogError.Value = true;
            return;
        }

        Debug.Log("Buy Token");
    }

    public void OnClose()
    {
        rootsMenu.gameObject.SetActive(false);
        connectWalletUI.gameObject.SetActive(false);
        myWalletUI.gameObject.SetActive(true);
    }

    public async void SwitchChain()
    {
        await GameDataServices.Instance.OnSwitchEthChain((result) =>
        {
            //if (result != null)
            //    gameService.isSwitchNetworkDialog.Value = false;
            WalletConnect.Instance.isActiveNetworkNotMatch = false;
        });
    }
}
