
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
public delegate void APICallback(UnityEngine.Networking.UnityWebRequest req, string error, string result);

public enum APIMethod
{
    GET = 0,
    POST = 1,
    PUT = 2,
    DELETE = 3,
}

public class EndPoint : ICloneable
    <EndPoint>
{
    public string ResourceAt;
    public APIMethod Method;

    public EndPoint Clone()
    {
        return new EndPoint()
        {
            ResourceAt = this.ResourceAt,
            Method = this.Method,
        };
    }

    public string Format(params string[] args)
    {
        return string.Format(ResourceAt, args);
    }
}

public struct APIRequest
{
    public readonly uint id;
    public readonly APICallback callback;
    public readonly UnityEngine.Networking.UnityWebRequest wwwRequest;
    public UnityWebRequestAsyncOperation operation;

    public APIRequest(uint initId, APICallback initCb, UnityEngine.Networking.UnityWebRequest initReq)
    {
        id = initId;
        callback = initCb;
        wwwRequest = initReq;
        operation = null;
    }
}

public class APIService : UnitySingleton<APIService>
{
    private readonly static string host = "https://dev.nftmarble.games/api";
    public const int MAX_REQUEST_AT_THE_SAME_TIME = 10;
    private uint _requestId = uint.MinValue;
    private List<APIRequest> _requests = new List<APIRequest>();
    private Queue<APIRequest> _pendingRequests = new Queue<APIRequest>();

    public static EndPoint apiStartGame = new EndPoint()
    {
        ResourceAt = "/game/startGame",
        Method = APIMethod.POST
    };

    public APIRequest Request(UnityWebRequest req, APICallback callback, object args = null)
    {
        if (req == null)
        {
            return default;
        }

        SetupHeader(req);
        SerializeObject(req, args);
        APIRequest apiRequest = new APIRequest(++_requestId, callback, req);

        if (_requests.Count < MAX_REQUEST_AT_THE_SAME_TIME)
        {
            try
            {
                apiRequest.operation = req.SendWebRequest();
                _requests.Add(apiRequest);
            }
            catch (System.Exception e)
            {
                Debug.LogError(string.Format("[APIService] Internal Error: {0}", e.ToString()));
            }
        }
        else
        {
            _pendingRequests.Enqueue(apiRequest);
        }

        return apiRequest;
    }

    public APIRequest Request(EndPoint endPoint,APICallback callback , WWWForm form,object param)
    {
        string url = host + endPoint.ResourceAt;

        return Request(GetRequest(url, endPoint.Method, form), callback, param);
    }

    protected virtual void Update()
    {
        int reqCount = _requests.Count;
        int pendingCount = _pendingRequests.Count;

        if (reqCount == 0 && pendingCount == 0) { return; }

        UpdateAPIRequesting();
    }

    private void UpdateAPIRequesting()
    {
        for (int i = _requests.Count - 1; i >= 0; i--)
        {
            APIRequest apiReq = _requests[i];
            try
            {
                if (apiReq.operation.isDone)
                {
                    if (!string.IsNullOrEmpty(apiReq.wwwRequest.error))
                    {
                        Debug.LogError(string.Format("[APIService] Error {0}{1}\nURL:{2}", apiReq.wwwRequest.responseCode, apiReq.wwwRequest.error, apiReq.wwwRequest.url));
                        apiReq.callback.Invoke(apiReq.wwwRequest, apiReq.wwwRequest.error, default);
                    }
                    else
                    {
                        try
                        {
                            string json = apiReq.wwwRequest.downloadHandler.text;
                            if (!string.IsNullOrEmpty(json))
                            {
                                apiReq.callback.Invoke(apiReq.wwwRequest, null, json);
                            }
                            else
                            {
                                apiReq.callback.Invoke(apiReq.wwwRequest, null, null);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(string.Format("[APIService] Parse Error: {0}", e.ToString()));
                            apiReq.callback.Invoke(apiReq.wwwRequest, e.ToString(), default);
                        }
                    }
                    apiReq.wwwRequest.Dispose();
                    _requests.RemoveAt(i);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(string.Format("[APIService] Internal Error: {0}", e.ToString()));
                apiReq.wwwRequest.Dispose();
                _requests.RemoveAt(i);
            }
        }
    }

    private void UpdatePendingAPI()
    {
        if (_requests.Count < MAX_REQUEST_AT_THE_SAME_TIME && _pendingRequests.Count > 0)
        {
            while (_requests.Count < MAX_REQUEST_AT_THE_SAME_TIME && _pendingRequests.Count > 0)
            {
                APIRequest apiReq = _pendingRequests.Dequeue();
                try
                {
                    apiReq.operation = apiReq.wwwRequest.SendWebRequest();
                    _requests.Add(apiReq);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(string.Format("[APIService] Internal Error: {0}", e.ToString()));
                }
            }
        }
    }

    protected virtual void SetupHeader(UnityWebRequest request)
    {
        //request.SetRequestHeader("Content-Type", "application/json");
    }

    protected virtual void SerializeObject(UnityWebRequest request, object args)
    {
        request.downloadHandler = new DownloadHandlerBuffer();
        if (args != null)
        {
#if UNITY_EDITOR
            Debug.Log($"[APIService] request prams {args}");
#endif
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(args.ToString()));
        }
    }

    public static UnityWebRequest GetRequest(string url, APIMethod method, WWWForm form)
    {
        string kHttpVerb = null;
        UnityWebRequest request = null;
        switch (method)
        {
            case APIMethod.GET:
                kHttpVerb = UnityWebRequest.kHttpVerbGET;
                break;
            case APIMethod.POST:
                kHttpVerb = UnityWebRequest.kHttpVerbPOST;
                if (form != null)
                    return UnityWebRequest.Post(url, form);
                break;
            case APIMethod.PUT:
                kHttpVerb = UnityWebRequest.kHttpVerbPUT;
                break;
            case APIMethod.DELETE:
                kHttpVerb = UnityWebRequest.kHttpVerbDELETE;
                break;

        }
        if (!string.IsNullOrEmpty(kHttpVerb))
        {
            request = new UnityWebRequest()
            {
                url = url,
                method = kHttpVerb,
            };
        }
        return request;
    }
}
