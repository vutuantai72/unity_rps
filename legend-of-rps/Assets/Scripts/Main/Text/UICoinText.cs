using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoinText : RxTextMeshPro<float>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.winCoin;       
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {
        return string.Format("+{0}", Math.Round(observationTargetIssueValue, 1));
    }
}
