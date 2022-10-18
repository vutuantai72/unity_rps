using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChoseMode1vs1Active : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isPlayerIn1vs1.Select(@bool => @bool);
    }
}
