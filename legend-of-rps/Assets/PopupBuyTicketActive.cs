using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PopupBuyTicketActive : RxActive
{
    // Start is called before the first frame update
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isShowPopupBuyTicket.Select(@bool => @bool);
    }


}
