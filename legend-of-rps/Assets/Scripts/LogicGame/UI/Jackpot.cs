using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jackpot : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic chestSpine;
    GameService gameService = GameService.@object;

    private void Start()
    {
        chestSpine.AnimationState.Complete -= AnimationState_Complete;
        chestSpine.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(TrackEntry trackEntry)
    {
        gameService.isChestJackpotShow.Value = false;
        gameService.isUserGetJackpot.Value = true;
    }
}
