using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DenyMatchingButton : RxButton
{
    GameService gameService = GameService.@object;
    public Button buttonAccept;
    public Button buttonDeny;
    public override void OnClickAsync()
    {
        if (gameService.isMathFound.Value == false)
        {
            return;
        }
        gameService.isMathFound.Value = false;
        this.buttonAccept.interactable = false;
        this.buttonDeny.interactable = false;
        GameService.@object.isPlayerInPVP.Value = true;
        GameService.@object.modeGame.Value = GameMode.NONE;
        GameDataServices.Instance.SocketEmitTourDenyMatching();
    }
}
