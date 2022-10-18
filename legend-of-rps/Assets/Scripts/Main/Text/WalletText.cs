using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalletConnectSharp.Unity;

public class WalletText : RxTextMeshPro<string>
{
    
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.userWall;
    }

    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
    {
        int lenghtString = observationTargetIssueValue.Length;
        return observationTargetIssueValue.Contains("0x") 
            ? $"{observationTargetIssueValue.Substring(0, 6)}...{observationTargetIssueValue.Substring(lenghtString - 4, 4)}" 
            : string.Empty;
    }
}
