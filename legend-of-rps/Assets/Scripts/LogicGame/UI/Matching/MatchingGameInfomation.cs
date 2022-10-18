using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchingGameInfomation : MonoBehaviour
{
    [SerializeField] private MatchingInfo_ITEM item;
    [SerializeField] private ImageLoader imgLoader;
    [SerializeField] private RectTransform enemyInfo;
    [SerializeField] private EnemyMatchingContent enemyContent;

    GameService gameService = GameService.@object;

    public void DisplayInfo()
    {
        var jsonObject = new JSONObject();
        jsonObject.AddField("email", $"{gameService.userEmail.Value}");
        StartCoroutine(GameDataServices.Instance.socketConnect(jsonObject));

        if (gameService.userName.Value.Length > 9)
        {
            string userName = gameService.userName.Value.Substring(0, 8) + "...";
            item.SetupInfo(userName, gameService.betCoinPVP.Value);
        }
        else
            item.SetupInfo(gameService.userName.Value, gameService.betCoinPVP.Value);


        enemyContent.DisplayEnemyMatchingData();
        imgLoader.OnShowImage(gameService.userAvatar.Value);

        item.transform.DOShakePosition(10, new Vector3(0, 16, 0), 5, 1.25f, false, false).SetEase(Ease.Linear);
        enemyInfo.gameObject.transform.DOShakePosition(10, new Vector3(0, 16, 0), 5, 1.25f, false, false).SetEase(Ease.Linear);

        gameService.isMatchingComplete.Value = true;
    }
}
