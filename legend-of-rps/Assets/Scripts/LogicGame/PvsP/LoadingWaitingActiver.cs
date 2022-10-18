using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LoadingWaitingActiver : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isShowLoadingWaiting.Select(i => i);
    }
}
