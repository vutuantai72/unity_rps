using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TextInsertCoinActiver : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.insertedCoin.Select(i => i <= 0);
    }
}
