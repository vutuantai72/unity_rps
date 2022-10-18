using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugManager : UnityActiveSingleton<DebugManager>
{
    [SerializeField] private GameObject debugUIGo;
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private bool isDebug;

    private bool isShow = false;
    private string txtDebug = "";
    private Action action;

    public bool IsShow { get => isShow; set => isShow = value; }
    public string TxtDebug { get => txtDebug; set => txtDebug = value; }
    public Action Action { get => action; set => action = value; }

    private void Start()
    {
        this.gameObject.SetActive(isDebug);
        action += OnTest;
    }

    public void UpdateActiveDebug(bool isShow)
    {
        this.isShow = isShow;
        debugUIGo.SetActive(this.isShow);
    }
    public void UpdateDebugText(string txtDebug)
    {
        UpdateActiveDebug(isShow: true);
        this.TxtDebug += string.Format("{0}\n", txtDebug);
        debugText.text = this.TxtDebug;
    }

    public void OnClose()
    {
        UpdateActiveDebug(!this.isShow);
    }

    public void OnClear()
    {
        this.TxtDebug = string.Empty;
        UpdateDebugText(txtDebug);
    }

    private void Update()
    {
        action?.Invoke();
    }

    private void OnTest()
    {
        Debug.LogError("TEST_LOG_2");
    }
}
