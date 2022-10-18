using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaitingLoading : MonoBehaviour
{
    GameService gameService = GameService.@object;
    [SerializeField] private GameObject gameObject;
    public Text lbTime;
    public Button btnCancel;
    private static float seconds;
    private static int minute;
    private static int interval = 1; 
    private static float nextTime = 0;
    private void OnEnable()
    {
        this.btnCancel.gameObject.SetActive(false);
        minute=0;
        seconds=0;
        lbTime.text ="00:00";
    }
    private void Update() {
        seconds += Time.deltaTime;
        if (Time.time >= nextTime) {
              //do something here every interval seconds
            lbTime.text = convertTime(Math.Round(seconds,0));
            if(minute>=1){
                this.btnCancel.gameObject.SetActive(true);
            }
            nextTime += interval; 
        }
       
    }

    private string convertTime(double s){
        if(s<10){
            return "0"+minute+":0"+Math.Round(seconds,0);
        }
        else if  (s<60){
            return "0"+minute+":"+Math.Round(seconds,0);
        }
        else
        {
            minute+=1;
            seconds-=60;
            return "0"+minute+":0"+Math.Round(seconds,0);
        }
    }

    public void clickButtonCancel(){

        if (GameService.@object.modeGame.Value == GameMode.PVP){
            GameDataServices.Instance.SocketEmitPvPLeaveRoom();
        }
        if(GameService.@object.modeGame.Value == GameMode.TOURNAMENT){
            GameDataServices.Instance.SocketEmitTourCancelMatching();
        }
        gameService.ResetGamePVP();
        gameService.isShowLoadingWaiting.Value = false;
        //gameService.isPlayerIn1vs1.Value = true;
        //gameService.isPlayerInPVP.Value = false;
    }
}
