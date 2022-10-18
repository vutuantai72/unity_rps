using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Spine.Unity;
using UnityEngine.UI;

public class WinPvPActive : RxActive
{
    [SerializeField] private SkeletonGraphic PVPwinAnim;
    [SerializeField] private GameObject avatar;

    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
       objectObservation = Observable.CombineLatest(gameService.resultStatus, gameService.gameState, (result, state) =>
            result == Constants.WIN && state == GameState.EndGame);

        WinSpineAnimation();
        
    }

    private void WinSpineAnimation()
    {
        if (gameService.numberOfPlayerBattle.Value == 2)
        {
            avatar.SetActive(false);
            PVPwinAnim.AnimationState.SetAnimation(0, "1st_winner_popup", false);
            PVPwinAnim.AnimationState.SetEmptyAnimation(1, 0);
            PVPwinAnim.AnimationState.AddAnimation(1, "1st_winner_loop", true, 0).MixDuration = 1;

        }
        else
        {

            PVPwinAnim.AnimationState.SetAnimation(0, "PopUp_Win", false);
            PVPwinAnim.AnimationState.SetEmptyAnimation(1, 0);
            PVPwinAnim.AnimationState.AddAnimation(1, "PopUp_Win_loop", true, 0).MixDuration = 1;
        }
    }
}

