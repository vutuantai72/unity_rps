using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NoMoneyDialogActiver : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.winStreak, gameService.gameState, (winStreak, state) =>
            winStreak > 0 && state == GameState.CoinRoulette);
    }
}
