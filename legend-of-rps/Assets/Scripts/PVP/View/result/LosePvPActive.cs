using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Spine.Unity;
using UnityEngine.UI;

public class LosePvPActive : RxActive
{
    [SerializeField] private SkeletonGraphic PVPloseAnim;
    [SerializeField] private GameObject avatar;

    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.resultStatus, gameService.gameState, (result, state) =>
            result == Constants.LOSE && state == GameState.EndGame);

        LoseSpineAnimation();   
    }

    private void LoseSpineAnimation()
    {
        if (gameService.numberOfPlayerBattle.Value == 2)
        {
            avatar.SetActive(false);
            PVPloseAnim.AnimationState.SetAnimation(0, "2nd_winner_popup", false);
            PVPloseAnim.AnimationState.SetEmptyAnimation(1, 0);
            PVPloseAnim.AnimationState.AddAnimation(1, "2nd_winner_loop", true, 0).MixDuration = 1;
        }
        else
        {


            PVPloseAnim.AnimationState.SetAnimation(0, "PopUp_Lose", false);
            PVPloseAnim.AnimationState.SetEmptyAnimation(1, 0);
            PVPloseAnim.AnimationState.AddAnimation(1, "PopUp_Lose_loop", true, 0).MixDuration = 1;
        }
    }
}

