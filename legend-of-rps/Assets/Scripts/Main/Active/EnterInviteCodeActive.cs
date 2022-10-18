using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnterInviteCodeActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isShowInviteDialog.Select(@bool => @bool);
    }
}
