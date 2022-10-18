using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodayCoinText: RxTextMeshPro<float>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.todayCoin;
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {
        return Math.Round(observationTargetIssueValue, 1).ToString();
    }
}
