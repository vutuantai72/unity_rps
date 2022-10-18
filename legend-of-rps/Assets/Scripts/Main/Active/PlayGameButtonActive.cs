using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayGameButtonActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.gameState, gameService.isGameStart, 
            (state, isStartGame) => state != GameState.Initializing);
    }
}
