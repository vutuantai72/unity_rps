using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ButtonClosePanelEndActive : RxActive
{
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(
            gameService.gameState,
            gameService.resultStatus,
            gameService.isGameTourStart,
            gameService.isFinal,
            (state, result, isTour, isFinal) =>
                (state == GameState.EndGame  && ( result == Constants.LOSE || (result == Constants.WIN && isFinal)) && isTour)
        );
    }
}
