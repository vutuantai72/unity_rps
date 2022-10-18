using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameText: RxTextMeshPro<string>
{
    GameService gameService = GameService.@object; 
    public int player;

    protected override void ObservationTargetDesignation()
    {
        switch (player)
        {
            case 1:
                objectObservation = gameService.userName;
                break;
            case 2:
                objectObservation = gameService.enemyName;
                break;
            default:
                break;
        }
    }

    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
    {        
        return observationTargetIssueValue;
       
    }
}
