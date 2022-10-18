using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class RouletteActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.gameState.Select(g => g == GameState.CoinRoulette && g != GameState.Initializing);
    }
}
