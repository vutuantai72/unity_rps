using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RxCoin : RxText<float>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.betCoin;
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {
        return "put coin:" + observationTargetIssueValue;
    }
}
