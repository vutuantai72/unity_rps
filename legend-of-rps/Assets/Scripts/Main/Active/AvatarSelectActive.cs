using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AvatarSelectActive : RxActive
{
    GameService gameService = GameService.@object;
    [SerializeField] private int indexAvatar;

    protected override void ObservationTargetDesignation()
    {
        //objectObservation = Observable.CombineLatest(gameService.avatarIndex, gameService.isAvatarSelected, (index, selected) =>
        //    index == indexAvatar && selected == true);
    }
}
