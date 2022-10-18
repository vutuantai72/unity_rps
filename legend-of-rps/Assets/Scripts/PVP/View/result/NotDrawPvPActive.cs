using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NotDrawPvPActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.resultStatus, gameService.gameState, (result, state) =>
            result != Constants.DRAW && state == GameState.EndGame);
    }
}
