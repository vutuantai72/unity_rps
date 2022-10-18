using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ClaimCouponDialogActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isClaimCouponDialog.Select(@bool => @bool);
    }
}
