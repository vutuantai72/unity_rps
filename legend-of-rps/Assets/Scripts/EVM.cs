using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class EVM : MonoBehaviour
{
    public class Response<T> { public T response; }
    private readonly static string host = "https://dev.nftmarble.games/api/system";

    /// <summary>
    /// Get Contract Data.
    /// </summary>
    /// <param name="contractAddress"></param>
    /// <param name="senderAddress"></param>
    /// <param name="targetAddress"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static async Task<string> GetContractData(string contractAddress, string senderAddress, string targetAddress, int amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("contractAddress", contractAddress);
        form.AddField("senderAddress", senderAddress);
        form.AddField("targetAddress", targetAddress);
        form.AddField("amount", amount);
        string url = host + "/fucntionEncode";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            await webRequest.SendWebRequest();
            Response<string> data = JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            return data.response;
        }
    }
}
