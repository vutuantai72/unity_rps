using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UniRx;
public class WebService<U> : Service<U> where U : class, new()
{
    protected bool 서버를쓰나 = false;
    private string 서버_URL = "http://localhost:3000";

    public UnityWebRequest getRequest(string url)
    {
        return UnityWebRequest.Get(서버_URL + url);
    }

    public UnityWebRequest postRequest(string url, WWWForm postData)
    {
        return UnityWebRequest.Post(서버_URL + url, postData);
    }

    IEnumerator<UnityWebRequestAsyncOperation> SendRequestCoroutine(IObserver<string> webData, UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            webData.OnNext(Encoding.Default.GetString(request.downloadHandler.data));
            webData.OnCompleted();
        }
        else
        {
            webData.OnError(new Exception(request.result.ToString()));
        }
    }

    public IObservable<T> get<T>(string url)
    {
        var request = getRequest(url);
        return Observable
            .FromCoroutine<string>(_ => SendRequestCoroutine(_, request))
            .Select(JsonUtility.FromJson<T>);
    }

    public IObservable<T> post<T>(string url, WWWForm postData)
    {
        var request = postRequest(url, postData);
        return Observable
            .FromCoroutine<string>(_ => SendRequestCoroutine(_, request))
            .Select(JsonUtility.FromJson<T>);
    }
}
