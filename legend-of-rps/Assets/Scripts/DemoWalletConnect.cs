using System;
using TMPro;
using UnityEngine;
using WalletConnectSharp.Unity;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Core.Models.Ethereum;
using Newtonsoft.Json;
using Nethereum.Web3;
using System.Numerics;

public class DemoWalletConnect : MonoBehaviour
{
    public WalletConnect walletConnect;
    public TMP_Text _textDebug;
    private string _txtDebug = "";
    public string NftTokenId;
    private WCSessionData sessionData;

    private async void Start()
    {
        await EvmManager.Initialize(walletConnect.AppData);
    }

    public async void WalletConnectHandler(WCSessionData data)
    {
        sessionData = data;
        Debug.Log("Wallet connection received");
        // Extract wallet address from the Wallet Connect Session data object.
        string address = data.accounts[0].ToLower();
        string appId = "DemoWalletConnect";
        long serverTime = DateTime.UtcNow.Ticks;

        string signMessage = $"{appId}:{serverTime}";

        Debug.Log($"Sending sign request for {address} ...");

        string response = await walletConnect.Session.EthPersonalSign(address, signMessage);
        _txtDebug += string.Format("\n[SIGN]:\n{0}\n{1}\n{2}", address, data.chainId, response);
        Debug.LogError(string.Format("[LOG_CAT] [SIGN]:\n {0}\n {1}\n {2}", address, data.chainId, response));
        _textDebug.text = _txtDebug;
    }

    public async void OnConnectWallet()
    {
        Debug.Log("Start connect wallet");
        string appId = "DemoWalletConnect";

        walletConnect.OpenDeepLink();

        var address = WalletConnect.ActiveSession.Accounts[0];
        var chainId = WalletConnect.ActiveSession.ChainId;
        _txtDebug += string.Format("\n[SIGN]:\n{0}\n{1}\n{2}", address, chainId);
        Debug.LogError(string.Format("[LOG_CAT] [SIGN]:\n {0}\n {1}\n {2}", address, chainId));
        _textDebug.text = _txtDebug;
    }

    public async void OnApproveTransaction()
    {
        string senderAddress = WalletConnect.ActiveSession.Accounts[0];
        int amount = 1;

        string url = string.Format(
            Constants.API_REQUEST_ENCODE_SWAP_FORMAT, 
            Constants.HOST, 
            Constants.C2C_CONTRACT_ADDRESS, 
            senderAddress, 
            Constants.C2C_CONTRACT_ADDRESS,
            amount,
            Constants.C2C_APPROVE_FUNCTION,
            "true");

        ApiController apiController = new ApiController(new JsonSerializationOption());
        TransactionData transactionData = await apiController.Get<TransactionData>(url);
        Debug.LogError("[LOG_CAT] [SWAP_API_DATA]: " + JsonConvert.SerializeObject(transactionData));

        string response = await walletConnect.Session.EthSendTransaction(transactionData);
        _txtDebug += string.Format("\n[TRANSACTION]:\n{0} ", response);
        _textDebug.text = _txtDebug;

        Debug.LogWarning("[LOG_CAT] [TRANSACTION]: " + response);
    }

    [ContextMenu("Test GetSwap")]
    public async void OnSwapTokenToCoin()
    {
        string senderAddress = WalletConnect.ActiveSession.Accounts[0];
        int amount = 1;

        string url = string.Format(
            Constants.API_REQUEST_ENCODE_SWAP_FORMAT,
            Constants.HOST,
            Constants.BC_CONTRACT_ADDRESS,
            senderAddress,
            Constants.C2C_CONTRACT_ADDRESS,
            amount,
            Constants.BC_SWAP_FUNCTION,
            "true");
        Debug.LogError("[LOG_CAT] [URL]: " + url);

        ApiController apiController = new ApiController(new JsonSerializationOption());
        TransactionData transactionData = await apiController.Get<TransactionData>(url);
        Debug.LogError("[LOG_CAT] [SWAP_API_DATA]: " + JsonConvert.SerializeObject(transactionData));

        _txtDebug += string.Format("\n[SWAP_DATA]:\n {0} ", JsonConvert.SerializeObject(transactionData));

        string response = await walletConnect.Session.EthSendTransaction(transactionData);
        _txtDebug += string.Format("\n[SWAP]:\n {0} ", response);
        _textDebug.text = _txtDebug;

        Debug.LogWarning("[LOG_CAT] [SWAP_DATA_HASH]: " + response);
    }

    public async void OnGetBalance()
    {
        var address = WalletConnect.ActiveSession.Accounts[0];
        var web3 = new Web3(Constants.BSC_JSON_RPC_TESTNET);

        string abi = Constants.C2C_ABI;
        string tokenAddress = "0xe0A939533A6E4580c62A7dEB58DcA958286cf638";
        var contract = web3.Eth.GetContract(abi, tokenAddress);

        // Call function of contract
        object[] param = new object[1] { address };
        // Here balance of a random address is requested for example
        var function = contract.GetFunction("balanceOf");
        var balance = await function.CallAsync<BigInteger>(param);
        var result = Web3.Convert.FromWei(balance);
        Debug.Log($"Balance: {result}");
    }
}
