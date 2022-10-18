using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPlayerResultText : RxTextMeshPro<string>
{
    [SerializeField]
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
       
        objectObservation = gameService.coinPlayerResult;
        
    }

    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
    {
        return observationTargetIssueValue;
    }
}
