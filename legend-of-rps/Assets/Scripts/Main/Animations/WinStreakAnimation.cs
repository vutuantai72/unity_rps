using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WinStreakAnimation : RxAnimationPlay
{
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        Debug.LogError("PLAY_ANIMATION");
        ObservingObjectValueIssuance("Play");
    }
}
