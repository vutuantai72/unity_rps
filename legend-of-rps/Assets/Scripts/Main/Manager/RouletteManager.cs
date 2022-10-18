using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RouletteManager : MonoBehaviour
{
    GameService gameService = GameService.@object;

    void Start()
    {
        gameService.CoinRouletteCheck()
           .Select(g => gameService.RouletteValueDetermination())
           .SelectMany(i => Observable.Interval(TimeSpan.FromSeconds(0.125f)).Take(i + 1))
           .Subscribe(l => { gameService.DuringRoulette(); })
           .AddTo(this);
    }
}
