using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LoadingActiver : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        Debug.Log($"Error Checking: ===== {gameService.isLoading.Value}");
        objectObservation = gameService.isLoading.Select(i => i);
    }
}
