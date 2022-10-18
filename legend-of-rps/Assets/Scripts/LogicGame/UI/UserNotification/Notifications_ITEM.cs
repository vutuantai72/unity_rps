using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notifications_ITEM : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationMessage;
    [SerializeField] private TextMeshProUGUI notificationValue;
    [SerializeField] private RectTransform notificationItem;
    [SerializeField] private RectTransform claimButton;
    [SerializeField] private RectTransform claimedReward;
    [SerializeField] private TextMeshProUGUI notificationID;
    [SerializeField] private Image iconCoin;
    [SerializeField] private Image iconSuccess;
    [SerializeField] private Image notifyReward;

    public void SetupItem(string message, decimal value, bool claimStatus, string id, int type)
    {
        notificationMessage.text = message;
        if (type == 2)
        {
            notificationValue.text = string.Empty;
            claimButton.gameObject.SetActive(false);
            claimedReward.gameObject.SetActive(false);
            iconCoin.gameObject.SetActive(false);
            iconSuccess.gameObject.SetActive(true);
            notifyReward.gameObject.SetActive(false);
        }
        else if(type == 3)
        {
            notificationValue.text = string.Empty;
            claimButton.gameObject.SetActive(!claimStatus);
            claimedReward.gameObject.SetActive(claimStatus);
            iconCoin.gameObject.SetActive(false);
            iconSuccess.gameObject.SetActive(true);
            notifyReward.gameObject.SetActive(false);
        }
        else
        {
            claimButton.gameObject.SetActive(!claimStatus);
            claimedReward.gameObject.SetActive(claimStatus);
            iconCoin.gameObject.SetActive(true);
            notificationValue.text = value.ToString();
            iconSuccess.gameObject.SetActive(false);
            notifyReward.gameObject.SetActive(true);
        }
        notificationID.text = id;
    }

    public void NotificationReward(Transform notificationID)
    {
        GameDataServices.Instance.ClaimNotificationCoin(notificationID.GetComponent<TextMeshProUGUI>().text, callback: (result) =>
        {
            if (result == null)
                return;

            UserNotification.Instance.GetInGameNotifications();
        });
    }
}
