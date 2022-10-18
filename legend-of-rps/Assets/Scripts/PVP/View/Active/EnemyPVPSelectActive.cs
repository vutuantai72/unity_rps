using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyPVPSelectActive : RxActive
{
    GameService gameService = GameService.@object;
    public SelectType selectType;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.enemyType.Select(s => s == selectType);
        objectObservation = Observable.CombineLatest(gameService.gameState, gameService.enemyType,
            (state, enemyType) => (state != GameState.Selecting && enemyType == selectType));
    }
}

