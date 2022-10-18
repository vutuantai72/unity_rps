using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenuSceneButton : RxButton
{
    GameService gameService = GameService.@object;

    public override void OnClickAsync()
    {
        gameService.gameState.Value = GameState.Initializing;
        gameService.isGameStart.Value = false;
        gameService.isPlayerInPVP.Value = false;
        gameService.isMathFound.Value = false;
        gameService.isGameTourStart.Value = false;
        gameService.isShowLoadingWaiting.Value = false;
        gameService.isShowLoadingBar.Value = false;
        gameService.isPlayerLogInGoogle.Value = true;
        gameService.isChoseModeGame.Value = true;
        AccountSetting.instance.GetUserInfomation();
        GameSceneServices.Instance.LoadScene(Constants.MENU_SCENE);
    }
}

