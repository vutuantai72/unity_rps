using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Spine.Unity;

public class ChoseModeTournament1VSNActive : RxActive
{
    [SerializeField] private SkeletonGraphic waitingRoomAnimation;

    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isPlayerInTournament_1vsN.Select(@bool => @bool);

        WaitingRoomAnimation();
    }

    private void WaitingRoomAnimation()
    {
        waitingRoomAnimation.AnimationState.SetAnimation(0, "waiting_room_appear", false);
        waitingRoomAnimation.AnimationState.SetEmptyAnimation(1, 0);
        waitingRoomAnimation.AnimationState.AddAnimation(1, "waiting_room_loop", true, 0).MixDuration = 2.7f;
    }
}
