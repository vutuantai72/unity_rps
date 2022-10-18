using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : RxButton
{
    [SerializeField]
    public Button button;
    GameService gameService = GameService.@object;

    public override void OnClickAsync()
    {        
        gameService.ResetGameTour();
    }
}
