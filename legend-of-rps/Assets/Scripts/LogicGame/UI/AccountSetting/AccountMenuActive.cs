using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AccountMenuActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.openAccountMenu.Select(i => i);
    }
}
