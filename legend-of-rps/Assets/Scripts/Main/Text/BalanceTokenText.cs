using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BalanceTokenText: RxTextMeshPro<decimal>
{
    GameService gameService = GameService.@object;

    private ReactiveProperty<decimal> resetValueAfterLink;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.userBalance;
    }

    protected override string DisplayFormatSpecified(decimal observationTargetIssueValue)
    {
        return Math.Round(observationTargetIssueValue, 1).ToString();
    }

    private void Awake()
    {
        resetValueAfterLink.Value = 0;
        objectObservation = resetValueAfterLink;
    }
}
