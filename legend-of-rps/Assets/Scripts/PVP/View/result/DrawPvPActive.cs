using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Spine.Unity;

public class DrawPvPActive : RxActive
{
    [SerializeField] private SkeletonGraphic PVPdrawAnim;

    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.resultStatus, gameService.gameState, (result, state) =>
            result == Constants.DRAW && state == GameState.EndGame);

        DrawSpineAnimation();
    }

    private void DrawSpineAnimation() {
        PVPdrawAnim.AnimationState.SetAnimation(0, "PopUp_Draw", false);
        PVPdrawAnim.AnimationState.SetEmptyAnimation(1, 0);
        PVPdrawAnim.AnimationState.AddAnimation(1, "PopUp_Draw_loop", true, 0).MixDuration = 1;
    }
}
