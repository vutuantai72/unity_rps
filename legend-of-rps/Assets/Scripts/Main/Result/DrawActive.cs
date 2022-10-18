using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DrawActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.MakeResultStream().Select(r => r == ResultType.Draw);
    }
}
