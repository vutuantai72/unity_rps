using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    [SerializeField] private GameObject coin;

    GameService gameService = GameService.@object;
    void Start()
    {
        gameService.gameState
            .Where(g => g != GameState.GetCoins)
            .SelectMany(Observable.Timer(TimeSpan.FromSeconds(1.0f)))
            .Subscribe(l => coin.SetActive(false))
            .AddTo(this);
    }
}
