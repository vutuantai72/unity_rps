using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ApiController
{
    private readonly ISerializationOption serializationOption;

    public ApiController(ISerializationOption serializationOption)
    {
        this.serializationOption = serializationOption;
    }

    public async Task<TResultType> Get<TResultType>(string url)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);

            www.SetRequestHeader("Content-Type", serializationOption.ContentType);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Failed: {www.error}");
            }
            var result = serializationOption.Deserialize<TResultType>(www.downloadHandler.text);
            return result;
        }
        catch (Exception ex)
        {
            Debug.Log($"{nameof(Get)} failed: {ex.Message}");
            return default;
        }
    }

    public async Task<TResultType> Post<TResultType>(string url, WWWForm form, string accessToken = null, string macAddress = null, string deviceID = null)
    {
        try
        {
            using UnityWebRequest www = UnityWebRequest.Post(url, form);
            if (!string.IsNullOrEmpty(accessToken))
            {
                www.SetRequestHeader("x-access-token", accessToken);
            }

            if (!string.IsNullOrEmpty(macAddress))
            {
                www.SetRequestHeader("mac-address", macAddress);
            }

            if (!string.IsNullOrEmpty(deviceID))
            {
                www.SetRequestHeader("device-id", deviceID);
            }

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Failed: {www.error}");
            }

            var result = serializationOption.Deserialize<TResultType>(www.downloadHandler.text);
            return result;
        }
        catch (Exception ex)
        {
            Debug.Log($"{nameof(Post)} failed: {ex.Message}");
            return default;
        }
    }
}