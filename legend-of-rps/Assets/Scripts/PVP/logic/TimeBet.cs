using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeBet : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    public Text lbTime;

    GameService gameService = GameService.@object;
    private static int interval = 1;
    private static float nextTime = 0;

    private void Start()
    {

    }
    private void Update()
    {
        if ((gameService.timeBet.Value / 1000) <= 0)
        {
            lbTime.text = "0";
            gameService.timeBet.Value = 0;
            return;
        }
        gameService.timeBet.Value -= Time.deltaTime * 1000;
        if (Time.time >= nextTime)
        {

            //do something here every interval seconds
            lbTime.text = Math.Round((gameService.timeBet.Value / 1000), 0).ToString();
            nextTime += interval;
        }

    }
}
