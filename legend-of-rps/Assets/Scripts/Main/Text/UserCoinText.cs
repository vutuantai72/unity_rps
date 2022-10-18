using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCoinText: RxTextMeshPro<float>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.userCoin;
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {
        return Math.Round(observationTargetIssueValue, 2).ToString();
    }
}
