using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAccountMenuButton : RxButton
{
    GameService gameService = GameService.@object;

    public override void OnClickAsync()
    {
        gameService.openAccountMenu.Value = false;
    }
}

