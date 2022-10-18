using UniRx;
using UnityEngine;
using System;

public partial class GameService : ManagerService<GameManager, GameService>
{
    public Transform targetTransform;
    public void InitializeInsertedCoin()
    {
        winCoin.Value = 0;
        betCoin.Value = 0;
    }
}
