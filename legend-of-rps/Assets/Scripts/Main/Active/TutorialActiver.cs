using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TutorialActiver : RxActive
{
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isCompleteTutorial.Select(i => i);
    }
}
