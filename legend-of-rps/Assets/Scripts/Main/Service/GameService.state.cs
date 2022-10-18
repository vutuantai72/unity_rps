using UniRx;
using UnityEngine;
using System;

public partial class GameService : ManagerService<GameManager, GameService>
{
    public IObservable<GameState> WhenInitializing()
    {
        return gameState.Where(state => state == GameState.Initializing);
    }
    public IObservable<GameState> WhenCoinIsBeingInserted()
    {
        return gameState.Where(state => state == GameState.InsertingCoin);
    }

    public IObservable<GameState> WhenSelectionIsComplete()
    {
        return gameState.Where(state => state == GameState.SelectionComplete);
    }
    public IObservable<bool> CheckDuringSelection()
    {
        return gameState.Select(state => state == GameState.Selecting);
    }
    public IObservable<bool> ConfirmSelectionCompletion()
    {
        return gameState.Select(state => state == GameState.SelectionComplete);
    }

    public IObservable<bool> ConfirmWhileInsertingCoins()
    {
        return gameState.Select(state => state == GameState.InsertingCoin);
    }

    public IObservable<GameState> CoinRouletteCheck()
    {
        return gameState.Where(state => state == GameState.CoinRoulette);
    }

    public IObservable<GameState> ConfirmToReceiveCoins()
    {
        return gameState.Where(state => state == GameState.GetCoins);
    }
    public IObservable<bool> CheckResult()
    {
        return gameState.Select(state => state == GameState.Result);
    }
    public IObservable<bool> CheckEndGame()
    {
        return gameState.Select(state => state == GameState.EndGame);
    }
}
