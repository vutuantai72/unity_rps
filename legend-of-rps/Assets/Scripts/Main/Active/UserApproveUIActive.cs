using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UserApproveUIActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isUserApprove.Select(@bool => @bool);
    }
}
