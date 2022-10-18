using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChoseModeTournamentActive : RxActive
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.isPlayerInTournament.Select(@bool => @bool);
    }
}
