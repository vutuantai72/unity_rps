using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusMenu : MonoBehaviour
{
    [SerializeField] private RectTransform DailyBonusUI;

    public void ShowUI()
    {
        DailyBonusUI.gameObject.SetActive(true);
        AudioServices.Instance.PlayClickAudio();
    }

    public void CloseUI()
    {
        DailyBonusUI.gameObject.SetActive(false);
    }
}
