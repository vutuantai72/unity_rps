using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChoseModeGameActive : RxActive
{
    [SerializeField] private AccountSetting account;
    [SerializeField] private ImageLoader imgLoader;
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isChoseModeGame.Select(@bool => @bool);
    }
}
