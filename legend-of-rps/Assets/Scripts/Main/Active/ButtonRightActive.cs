using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class ButtonRightActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.gameState.Select(g => g == GameState.CoinRoulette || g == GameState.Selected || g == GameState.SelectionComplete || g == GameState.GetCoins);
    }
}