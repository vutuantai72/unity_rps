using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RxEnemy : RxSprite
{
    GameService gameService = GameService.@object;
    public Sprite[] sprites;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.enemyType.Select(s => sprites[(int)s]);
    }

}
