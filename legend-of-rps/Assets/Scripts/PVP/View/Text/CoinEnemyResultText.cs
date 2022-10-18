using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEnemyResultText : RxTextMeshPro<string>
{
    [SerializeField]
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.coinEnemyResult;
    }

    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
    {
        return observationTargetIssueValue;
    }
}
