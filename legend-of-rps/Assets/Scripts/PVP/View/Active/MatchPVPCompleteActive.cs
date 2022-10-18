using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MatchPVPCompleteActive : RxActive
{
    
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isMatchingComplete.Select(i => i);
    }
}
