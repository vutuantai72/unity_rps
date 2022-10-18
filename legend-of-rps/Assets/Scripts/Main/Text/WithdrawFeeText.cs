using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithdrawFeeText: RxTextMeshPro<int>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.withdrawFee;
    }

    protected override string DisplayFormatSpecified(int observationTargetIssueValue)
    {
        return observationTargetIssueValue.ToString() + "%";
    }
}
