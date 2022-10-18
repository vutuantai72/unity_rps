using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimeWaitEndActive : RxActive
{
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(
            gameService.gameState,
            gameService.timeWait,
            gameService.resultStatus,
            (state, timeWait, result) =>{
                return (state == GameState.EndGame && timeWait > 0 && result == Constants.DRAW);
            }                
        );
    }
}
