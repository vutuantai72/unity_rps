using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PopupCancelTicketActive : RxActive
{
    // Start is called before the first frame update
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isShowPopupCancelTicket.Select(@bool => @bool);
    }


}
