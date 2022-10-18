using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LoadingEarlyPageActive : RxActive
{
    public int Player = 2;
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
       objectObservation = Observable.CombineLatest(gameService.numberOfPlayerBattle, gameService.isGameTourStart, 
            (numberOfPlayerBattle, isGameTourStart) => numberOfPlayerBattle == Player && isGameTourStart == true);
    }
}
