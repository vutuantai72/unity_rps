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


public class Withdraw : MonoBehaviour
{
    [SerializeField] private RectTransform rootsMenu;
    [SerializeField] private RectTransform connectWalletUI;
    [SerializeField] private RectTransform myWalletUI;
    [SerializeField] private TMP_Text msgErrorText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button btnWithdraw;
    [SerializeField] private Text withdrawText;

    [SerializeField] private Sprite btnImageNormal;
    [SerializeField] private Sprite btnImageRed;
    [SerializeField] private Image btnSprite;

    [SerializeField] Text hints;
    private int charLimit = 12;

    private GameService gameService = GameService.@object;

    private decimal amountToken;
    // Start is called before the first frame update
    private void Start()
    {
        GameDataServices.Instance.OnGetBNBTokenRate();
        GameDataServices.Instance.action += WalletConnectActionWithdrawHandler;
        btnWithdraw.interactable = false;
        msgErrorText.text = "";

        inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return ValidateInputNumber(addedChar); };
        inputField.characterLimit = charLimit;
        hints.text = "";
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
            btnWithdraw.interactable = true;
            btnSprite.sprite = btnImageRed;
            WithdrawButtonEvent(SwitchChain, "Switch");
        }
        else
        {
            btnWithdraw.interactable = false;
            btnSprite.sprite = btnImageNormal;
            WithdrawButtonEvent(WithdrawAsync, "Withdraw");
        }
    }

    private void WithdrawButtonEvent(UnityAction action, string text = null)
    {
        btnWithdraw.onClick.RemoveAllListeners();
        withdrawText.text = text;
        btnWithdraw.onClick.AddListener(action);
    }

    public void OnEnable()
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

    private async void WalletConnectActionWithdrawHandler()
    {
        Debug.Log($"[WalletConnectActionHandler Withdraw]");
        Debug.Log($"WalletConnectActionHandler Withdraw: {WalletConnect.ActiveSession.Accounts?[0]}");

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
        if (!string.IsNullOrEmpty(value) && CheckWithdrawInputValue(value))
        {
            amountToken = decimal.Parse(value);
            btnWithdraw.interactable = true;
        }
        else
            btnWithdraw.interactable = false;
    }

    private bool CheckWithdrawInputValue(string value)
    {
        if (long.Parse(value) > 1000 || long.Parse(value) < 500 || long.Parse(value) > gameService.userCoin.Value)
        {
            hints.text = "Invalid withdraw input value";
            return false;
        }
        else {
            hints.text = "";
            return true;
        }
    }
    

    public async void WithdrawAsync()
    {
        gameService.isLoading.Value = true;
        var withdrawData = new WithdrawResponse();

        try
        {
            if (amountToken > 1000 && amountToken < 500 && gameService.userCoin.Value > 500)
            {
                gameService.msgDialogTitleError.Value = "Error!!";
                gameService.msgDialogError.Value = "Invalid coin withdraw, min value is 500.";
                gameService.isShowDialogError.Value = true;
                gameService.isLoading.Value = false;
                return;
            }
            else if(gameService.userCoin.Value < 500)
            {
                gameService.msgDialogTitleError.Value = "Error!!";
                gameService.msgDialogError.Value = "Your coin is not enough to withdraw";
                gameService.isShowDialogError.Value = true;
                gameService.isLoading.Value = false;
                return;
            }    

            withdrawData = await GameDataServices.Instance.GetDataWithdraw(
                amountToken.ToString());
        }
        catch (Exception ex)
        {
            Debug.LogError("[Withdraw] [Error]: " + ex);
            gameService.msgDialogTitleError.Value = "OOOPS!!";
            // Use this ? 
            //gameService.msgDialogError.Value = "Could not connect to server. Please try again later.";
            gameService.msgDialogError.Value = withdrawData.msg;
            gameService.isShowDialogError.Value = true;
        }

        await GameDataServices.Instance.GetUserWalletBNB();
        var balanceBNB = gameService.userBNBBalance.Value;
        string transaction = string.Empty;
        if (balanceBNB != 0 && balanceBNB >= (decimal)0.001)
        {
            gameService.isLoading.Value = false;
            try
            {
                transaction = await GameDataServices.Instance.OnWithdraw(withdrawData.data.withdrawConfig);
                if (!string.IsNullOrEmpty(transaction))
                {
                    var result = await GameDataServices.Instance.UpdateHashWithdraw(transaction, withdrawData.data.withdrawId);
                    if (result.code == 0)
                    {
                        Debug.LogError($"{transaction}");
                        gameService.msgDialogTitleError.Value = "Congratulation";
                        gameService.msgDialogError.Value = "Successfully withdraw!";
                        gameService.isShowDialogError.Value = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("[Withdraw] [Error]: " + ex);
                gameService.msgDialogTitleError.Value = "OOOPS!!";
                // Use this ?
                //gameService.msgDialogError.Value = "Could not connect to server. Please try again later.";
                gameService.msgDialogError.Value = withdrawData.msg;
                gameService.isShowDialogError.Value = true;
                gameService.isLoading.Value = false;
            }
        }
        else
        {
            gameService.isLoading.Value = false;
            string msgError = "BNB is not enough to make Withdraw\nYou can buy it in website: http://winery.finance/";
            gameService.msgDialogTitleError.Value = "OOOPS!!";
            gameService.msgDialogError.Value = msgError;
            gameService.isShowDialogError.Value = true;
            return;
        }

        Debug.Log("Withdraw");
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
