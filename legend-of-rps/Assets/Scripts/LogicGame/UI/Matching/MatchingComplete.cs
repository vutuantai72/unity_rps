using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingComplete : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic matchingSpineComplete;
    GameService gameService = GameService.@object;

    private void Start()
    {
        matchingSpineComplete.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(TrackEntry trackEntry)
    {
        gameService.isMatchingComplete.Value = false;
        gameService.gameState.Value = GameState.Selecting;
    }
}
