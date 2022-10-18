using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEndButton : RxButton
{
    [SerializeField]
    public Button[] button;
    GameService gameService = GameService.@object;
    public bool isYes;

    public override void OnClickAsync()
    {
       if (isYes)
        {
            gameService.notifyEndGame.Value = Constants.NOTIFYWAITENEMY;
            GameDataServices.Instance.SocketEmitPvpAcceptContinue();
        }
        else
        {
            // gameService.isShowLoadingWaiting.Value = false;
            gameService.ResetGamePVP();
            GameDataServices.Instance.SocketEmitPvPLeaveRoom();
        }
        foreach (var item in button)
        {
            item.interactable = false;
        }
    }
}
