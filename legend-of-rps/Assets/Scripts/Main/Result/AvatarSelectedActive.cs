using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class AvatarSelectedActive : RxActive
{
    GameService gameService = GameService.@object;
    [SerializeField] private TextMeshProUGUI nameAvatar;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.listAvatar.Select(r => r.Find(x => x.nameAvatar == nameAvatar.text).isSelected);
    }
}
