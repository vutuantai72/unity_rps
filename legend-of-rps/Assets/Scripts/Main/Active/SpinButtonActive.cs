using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpinButtonActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isSpinButtonActive.Select(@bool => @bool);
    }
}
