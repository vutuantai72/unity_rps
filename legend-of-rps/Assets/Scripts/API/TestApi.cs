using UnityEngine;
using WalletConnectSharp.Core.Models.Ethereum;

public class TestApi : MonoBehaviour
{
    private readonly static string host = "https://dev.nftmarble.games/api/system";
    private const string Format = "{0}/fucntionEncode?contractAddress={1}&senderAddress={2}&targetAddress={3}&method={4}&amount={5}";

    [ContextMenu("Test GetSwap")]
    public async void GetSwapTokenToCoinTransactionData()
    {
        string contractAddress = "0xdcDf31A3f71fC56F0465E5D157eC1A5fc8E1e408";
        string senderAddress = "0x57755eb14e34BB7feB50B28425970df470d1EA26";
        string targetAddress = "0xe0A939533A6E4580c62A7dEB58DcA958286cf638";
        string method = "swapTokenToCoin";
        int amount = 10;

        string url = string.Format(Format, host, contractAddress, senderAddress, targetAddress, method, amount);
        Debug.LogError("[LOG_TEST_URL]: " + url);

        ApiController apiController = new ApiController(new JsonSerializationOption());
        var result = await apiController.Get<TransactionData>(url);
        Debug.LogError("[LOG_TEST_SWAP]: " + result);
    }
}
