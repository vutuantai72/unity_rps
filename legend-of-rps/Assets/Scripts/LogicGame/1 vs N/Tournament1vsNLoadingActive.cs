using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Tournament1vsNLoadingActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isShow1vsNLoading.Select(@bool => @bool);

    }
}
