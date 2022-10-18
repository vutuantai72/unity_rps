using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RouletteNumberText : RxText<int>
{
    GameService gameService = GameService.@object;
    public int index;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.MultipleReturn(index);
    }

    protected override string DisplayFormatSpecified(int observationTargetIssueValue)
    {
        return observationTargetIssueValue.ToString();
    }
}

