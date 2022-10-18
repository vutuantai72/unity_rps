using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UserSwapSuccessActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.userSwapSuccess.Select(@bool => @bool);
    }
}
