using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SelectRightActive : RxActive
{
    GameService gameService = GameService.@object;
    public SelectType selectType;

    protected override void ObservationTargetDesignation()
    {
        VibrationService.Instance.PlayHapticsHeavyImpact();

        objectObservation = gameService.playerType.Select(s => s == selectType);
    }
}
