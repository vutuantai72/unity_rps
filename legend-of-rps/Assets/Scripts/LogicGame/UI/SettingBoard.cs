using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBoard : MonoBehaviour
{
    [SerializeField] private GameObject Setting;
    [SerializeField] private GameObject ButtonOffSetting;

    public void OpenSetting()
    {
        Setting.SetActive(true);
        ButtonOffSetting.SetActive(true);
    }

    public void CloseSettingBoard()
    {
        Setting.SetActive(false);
        ButtonOffSetting.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
