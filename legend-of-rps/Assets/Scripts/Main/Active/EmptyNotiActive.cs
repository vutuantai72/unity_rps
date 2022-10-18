using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EmptyNotiActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isNotifiEmpty.Select(i => i);
    }
}
