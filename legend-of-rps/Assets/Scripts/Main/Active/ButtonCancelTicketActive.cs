using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ButtonCancelTicketActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isHaveTicket.Select(@bool => @bool);
    }
}
