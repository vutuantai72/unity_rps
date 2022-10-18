using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LoadingPagePVPActive : RxActive
{
    
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.gameState, gameService.isGameStart, 
            (state, isGameStart) => state == GameState.Initializing || isGameStart == true);
    }
}
