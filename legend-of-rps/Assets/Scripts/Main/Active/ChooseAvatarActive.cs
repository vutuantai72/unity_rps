using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChooseAvatarActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isUserChooseAvatar.Select(@bool => @bool);
    }
}
