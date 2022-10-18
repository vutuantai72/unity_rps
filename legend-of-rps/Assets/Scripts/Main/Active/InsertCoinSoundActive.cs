using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class InsertCoinSoundActive : RxSound
{
    GameService gameService = GameService.@object;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isInsertCoin;
    }
}
