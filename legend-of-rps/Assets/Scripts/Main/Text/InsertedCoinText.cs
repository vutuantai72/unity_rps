using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertedCoinText: RxTextMeshPro<int>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.insertedCoin;
    }

    protected override string DisplayFormatSpecified(int observationTargetIssueValue)
    {
        return observationTargetIssueValue.ToString();
    }
}
