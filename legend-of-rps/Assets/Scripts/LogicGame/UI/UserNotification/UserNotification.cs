using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNotification : UnityActiveSingleton<UserNotification>
{
    [SerializeField] private Notifications_ITEM item;
    [SerializeField] private Transform containItem;
    [SerializeField] private RectTransform emptyItem;

    [SerializeField] RectTransform contentNotification;
    [SerializeField] GridLayoutGroup contentLayout;

    private GameService gameService = GameService.@object;
    private bool claimStatus;

    public async void GetInGameNotifications()
    {
        await GameDataServices.Instance.GetNotifications();

        DisplayNotificationLists();
    }

    private void DisplayNotificationLists()
    {
        Destroy();
        var notificationData = gameService.notificationModel.Value;
        //if(notificationData.total < 5)
        //{
        //    contentNotification.sizeDelta = new Vector2(contentNotification.sizeDelta.x, 1900f);
        //}
        //else
        //    contentNotification.sizeDelta = new Vector2(contentNotification.sizeDelta.x, contentLayout.cellSize.y * (notificationData.total + 10));

        if (notificationData.total == 0)
        {
            gameService.isNotifiEmpty.Value = true;
        }
        else if(notificationData.total > 0)
        {
            gameService.isNotifiEmpty.Value = false;
            for (int i = 0; i < notificationData.total; i++)
            {
                var itemNoti = Instantiate(item, containItem);
                //itemNoti.transform.position = contentNotification.transform.position;

                itemNoti.SetupItem(notificationData.data[i].message, 
                    notificationData.data[i].coin, 
                    Convert.ToBoolean(notificationData.data[i].status), 
                    notificationData.data[i].id,
                    notificationData.data[i].type
                    );
            }
        }
    }

    private void Destroy()
    {
        foreach (Transform child in containItem.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}
