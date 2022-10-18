using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAccountMenuButton : RxButton
{
    GameService gameService = GameService.@object;

    public override void OnClickAsync()
    {
        gameService.openAccountMenu.Value = true;
    }
}

