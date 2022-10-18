using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SideMenu : MonoBehaviour
{
    [SerializeField] private GameObject SideMenu_UI;
    private float screenRatio = Screen.width * 1.0f / Screen.height;

    public void OpenSideMenu()
    {
        AudioServices.Instance.PlayClickAudio();

        if(Math.Round(screenRatio, 2) == Math.Round(UIPositionObjectByScreen.Instance.screen9_22, 2))
        {
            SideMenu_UI.transform.DOMove(new Vector3(3.5f, SideMenu_UI.transform.position.y, SideMenu_UI.transform.position.z), 1f);
        }           
        else if(screenRatio != UIPositionObjectByScreen.Instance.screen9_22)
        {
            SideMenu_UI.transform.DOMove(new Vector3(3.8f, SideMenu_UI.transform.position.y, SideMenu_UI.transform.position.z), 1f);
        }
    }
    
    public void CloseSideMenu()
    {
        AudioServices.Instance.PlayClickAudio();
        SideMenu_UI.transform.DOMove(new Vector3(9.9f, SideMenu_UI.transform.position.y, SideMenu_UI.transform.position.z), 1f);
    }

    public void SignOutGoogle()
    {
        GoogleSignIn.Instance.OnSignOut();
    }
}
