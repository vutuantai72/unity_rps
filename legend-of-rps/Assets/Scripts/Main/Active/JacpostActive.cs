using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class JacpostActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isUserGetJackpot.Select(@bool => @bool);
    }
}
