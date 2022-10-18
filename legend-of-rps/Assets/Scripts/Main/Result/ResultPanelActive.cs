using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanelActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.ConfirmSelectionCompletion();
    }
}
