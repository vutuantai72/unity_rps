using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LoginActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isPlayerLogInGoogle.Select(@bool => !@bool);
    }
}
