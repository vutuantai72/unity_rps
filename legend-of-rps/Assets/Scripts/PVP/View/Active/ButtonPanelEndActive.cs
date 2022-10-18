using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ButtonPanelEndActive : RxActive
{
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(
            gameService.gameState,
            gameService.resultStatus,
            gameService.isGameTourStart,
            (state, result, isTour) =>
                (state == GameState.EndGame)  && result != Constants.DRAW && !isTour
        );
    }
}
