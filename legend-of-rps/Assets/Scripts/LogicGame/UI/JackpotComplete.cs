using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JackpotComplete : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic jackpotSpine;
    GameService gameService = GameService.@object;

    private void Start()
    {
        jackpotSpine.AnimationState.Complete -= AnimationJackpot_Complete;
        jackpotSpine.AnimationState.Complete += AnimationJackpot_Complete;
    }

    private void AnimationJackpot_Complete(TrackEntry trackEntry)
    {
        gameService.isChestJackpotShow.Value = true;
        gameService.isUserGetJackpot.Value = false;
        gameService.isJackpostShow.Value = false;
    }
}
