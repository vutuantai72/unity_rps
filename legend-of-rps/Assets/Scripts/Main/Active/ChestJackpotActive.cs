using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChestJackpotActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isJackpostShow.Select(@bool => @bool);        
    }
}
