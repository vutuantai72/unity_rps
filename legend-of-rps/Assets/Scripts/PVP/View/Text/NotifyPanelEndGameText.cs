using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyPanelEndGameText : RxTextMeshPro<string>
{
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.notifyEndGame;
    }

    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
    {
        return observationTargetIssueValue;
    }
}
