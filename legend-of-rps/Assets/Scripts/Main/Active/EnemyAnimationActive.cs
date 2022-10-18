using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyAnimationActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.CheckDuringSelection();
    }
}
