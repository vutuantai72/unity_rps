using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PanelResultActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
           objectObservation = gameService.CheckResult();
    }
}
