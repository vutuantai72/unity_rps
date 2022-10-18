using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChestJackpotDeActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isChestJackpotShow.Select(@bool => @bool);        
    }
}
