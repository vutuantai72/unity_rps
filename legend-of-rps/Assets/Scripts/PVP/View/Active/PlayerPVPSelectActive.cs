using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerPVPSelectActive : RxActive
{
    GameService gameService = GameService.@object;
    public SelectType selectType;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.playerType.Select(s => s == selectType);
        objectObservation = Observable.CombineLatest(gameService.gameState, gameService.playerType,
            (state, playerType) => (state != GameState.Selecting && playerType == selectType));
    }
}

