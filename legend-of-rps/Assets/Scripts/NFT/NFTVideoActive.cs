using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NFTVideoActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isNFTShowcaseOpen.Select(@bool => @bool);
    }
}
