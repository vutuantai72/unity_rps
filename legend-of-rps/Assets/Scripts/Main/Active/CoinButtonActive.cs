using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CoinButtonActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.betCoin.Select(i => i >= 0);
    }
}
