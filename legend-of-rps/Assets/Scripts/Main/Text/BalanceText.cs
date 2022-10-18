using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceText: RxTextMeshPro<decimal>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.userBalance;
    }

    protected override string DisplayFormatSpecified(decimal observationTargetIssueValue)
    {
        return $"Balance: {Math.Round(observationTargetIssueValue, 4)}";
    }
}
