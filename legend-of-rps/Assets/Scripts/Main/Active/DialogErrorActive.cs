using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DialogErrorActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isShowDialogError.Select(@bool => @bool);
    }
}
