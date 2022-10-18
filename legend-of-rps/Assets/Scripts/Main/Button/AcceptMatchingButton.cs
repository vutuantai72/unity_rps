using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptMatchingButton : RxButton
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
        this.buttonAccept.interactable = false;
        this.buttonDeny.interactable = false;
        GameDataServices.Instance.SocketEmitTourAcceptMatching();
    }
}
