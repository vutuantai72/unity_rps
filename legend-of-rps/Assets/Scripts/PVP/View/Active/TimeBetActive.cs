using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimeBetActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.gameState, gameService.timeBet,
             (state, timeBet) => (state == GameState.Initializing || state == GameState.Selecting || state == GameState.SelectionComplete) && timeBet > 0);
    }
}
