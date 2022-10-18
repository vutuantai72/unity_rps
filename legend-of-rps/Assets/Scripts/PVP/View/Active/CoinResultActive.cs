using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CoinResultActive : RxActive
{
    // [SerializeField] bool isWin;
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.betCoinPVP, gameService.gameState, (betCoinPVP, state) =>
            betCoinPVP > 0 && state == GameState.CoinResult);
    }
}
