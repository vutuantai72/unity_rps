using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyPVPAnimationActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        // objectObservation = (gameService.CheckDuringSelection() && !(gameService.CheckResult()));
        objectObservation =  gameService.gameState.Select(s => s == GameState.Selecting || s != GameState.Result);
    }
}
