using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class LoseActive : RxActive
{
    GameService gameService = GameService.@object;
    public static bool isLose = false;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.MakeResultStream().Select(r => r == ResultType.Lose);
        isLose = true;
    }
}

