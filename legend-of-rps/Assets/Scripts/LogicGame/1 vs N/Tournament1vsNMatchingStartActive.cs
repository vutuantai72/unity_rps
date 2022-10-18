using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Spine.Unity;

public class Tournament1vsNMatchingStartActive : RxActive
{
    [SerializeField] private SkeletonGraphic clockAnim;

    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isShow1vsNMatchingStart.Select(@bool => @bool);
      
        PlayMatchingAnim();
    }

    private void PlayMatchingAnim()
    {
        clockAnim.AnimationState.SetAnimation(0, "countdown", false);
    }
}
