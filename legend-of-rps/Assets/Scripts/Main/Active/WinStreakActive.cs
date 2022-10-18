using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WinStreakActive : RxActive
{
    [SerializeField] int winTarget;
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.winStreak, gameService.gameState, (winStreak, state) =>
            winStreak == winTarget && state == GameState.CoinRoulette);
    }
}
