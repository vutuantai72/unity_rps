using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ConnectDialogActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.userWall.Select(s => (string.IsNullOrEmpty(s) || !s.Contains("0x")));
    }
}
