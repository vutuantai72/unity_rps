using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.Networking;
using System.Text;

[Serializable]
public class JsonClass<T>
{
    public T data;
}

public class GameMissions : UnityActiveSingleton<GameMissions>
{
    [SerializeField] private GameMission_Item missionItem;
    [SerializeField] private Transform containItem;
    [SerializeField] private Sprite[] missionTypeLogo;
    [SerializeField] private Sprite[] missionSubTypeLogo;
    [SerializeField] [Range(0, 1)] float process = 0f;

    private GameService gameService = GameService.@object;
    private Sprite logo;
    private bool status;
    private bool claimStatus;
    //private readonly static string host = "https://dev.nftmarble.games/api";
    private readonly static string host = "https://api.rpsgame.world/api";

    protected IEnumerator<UnityWebRequestAsyncOperation> SendRequestCoroutine(IObserver<string> webData, UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string dataString = Encoding.Default.GetString(request.downloadHandler.data);
            string jsonString = "{ \"data\":" + dataString + "}";
            webData.OnNext(jsonString);
            webData.OnCompleted();
        }
        else
        {
            webData.OnError(new Exception(request.result.ToString()));
        }
    }

    public T fromJson<T>(string jsonString)
    {
        return JsonUtility.FromJson<JsonClass<T>>(jsonString).data;
    }

    public async void GetMissionLists()
    {
        await GameDataServices.Instance.GetMissionList(callback: (result) =>
        {
            if (result == null)
                return;
            
            gameService.missionData.Value = result;
            DisplayMissionLists();           
        });
    }

    private void DisplayMissionLists()
    {
        Destroy();

        for (int i = 0; i < gameService.missionData.Value.total; i++)
        {
            var item = Instantiate(missionItem, containItem);
            switch (gameService.missionData.Value.data[i].status)
            {
                case 1:
                    status = false;
                    claimStatus = false;
                    break;
                case 2:
                    status = true;
                    claimStatus = true;
                    break;
                case 3:
                    status = false;
                    claimStatus = true;
                    break;
            }

            switch (gameService.missionData.Value.data[i].type)
            {
                case 1:
                    logo = missionTypeLogo[0];
                    break;
                case 2:
                    logo = missionTypeLogo[1];
                    break;
                case 3:
                    switch (gameService.missionData.Value.data[i].sub_type)
                    {
                        case 1:
                            logo = missionSubTypeLogo[0];
                            break;
                        case 2:
                            logo = missionSubTypeLogo[1];
                            break;
                        case 3:
                            logo = missionSubTypeLogo[2];
                            break;
                    }
                    break;
            }

            process = ((float)gameService.missionData.Value.data[i].process) / ((float)gameService.missionData.Value.data[i].requirement);
           
            item.SetupMission(logo, gameService.missionData.Value.data[i].name, gameService.missionData.Value.data[i].reward.ToString()
                , gameService.missionData.Value.data[i].process.ToString() + "/" + gameService.missionData.Value.data[i].requirement.ToString()
                , status, process, gameService.missionData.Value.data[i].id, claimStatus);
        }
    }

    public IObservable<T> IObGetMissionList<T>()
    {
        UnityWebRequest request = UnityWebRequest.Get($"{host}/mission/getMissionList?email={gameService.userEmail.Value}&page=1&limit=10&type=single-play");
        return Observable.FromCoroutine<string>(_ => SendRequestCoroutine(_, request))
            .Select(fromJson<T>)
            .Select(_ =>
            {
                Debug.LogError("URL: ====== " + request.downloadHandler.text);
                return _;
            }).ObserveOnMainThread();
    }

    public IObservable<GameMissionModel> UpdateGameMissions()
    {
        return IObGetMissionList<GameMissionModel>();
    }

    public IEnumerator UpdateMissionLists()
    {
        yield return new WaitForSeconds(1f);
        UpdateGameMissions().Select(_ =>
        {
            return _;
        })
        .Subscribe(_ =>
        {
            var missionData = _;
            for (int i = 0; i < missionData.total; i++)
            {
                var item = Instantiate(missionItem, containItem);
                switch (missionData.data[i].status)
                {
                    case 1:
                        status = false;
                        claimStatus = false;
                        break;
                    case 2:
                        status = true;
                        claimStatus = true;
                        break;
                    case 3:
                        status = false;
                        claimStatus = true;
                        break;
                }

                switch (missionData.data[i].type)
                {
                    case 1:
                        logo = missionTypeLogo[0];
                        break;
                    case 2:
                        logo = missionTypeLogo[1];
                        break;
                    case 3:
                        switch (missionData.data[i].sub_type)
                        {
                            case 1:
                                logo = missionSubTypeLogo[0];
                                break;
                            case 2:
                                logo = missionSubTypeLogo[1];
                                break;
                            case 3:
                                logo = missionSubTypeLogo[2];
                                break;
                        }
                        break;
                }

                process = missionData.data[i].process / missionData.data[i].requirement;

                item.SetupMission(logo, missionData.data[i].name, missionData.data[i].reward.ToString()
                    , missionData.data[i].process.ToString() + "/" + missionData.data[i].requirement.ToString()
                    , status, process, missionData.data[i].id, claimStatus);
            }
        });
    }

    private void Destroy()
    {
        foreach (Transform child in containItem.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
