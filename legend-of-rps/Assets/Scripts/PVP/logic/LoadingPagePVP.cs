using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingPagePVP : MonoBehaviour
{
    GameService gameService = GameService.@object;
    [SerializeField] private ImageLoader playerAvatar;
    [SerializeField] private ImageLoader enemyAvatar;
    private static int interval = 1; 
    private static float nextTime = 0;
    private float timeCountDown = 3;
    private bool start = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        //gameService.gameState.Value = GameState.Initializing;
        playerAvatar.OnShowImage(gameService.userAvatar.Value);
        enemyAvatar.OnShowImage(gameService.enemyAvatar.Value);
    }

    public void onStartGame(){
        gameService.StartPVP();
    }
}
