using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText: RxTextMeshPro<float>
{
    GameService gameService = GameService.@object; 
    public int player=1;

    protected override void ObservationTargetDesignation()
    {
        switch (player)
        {
            case 1:
                objectObservation = gameService.userCoin;
                break;
            case 2:
                objectObservation = gameService.enemyCoin;
                break;
            default:
                break;
        }
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {        
        // return observationTargetIssueValue.ToString("0.00");
        return FormattedAmount(observationTargetIssueValue);
    }

    public string FormattedAmount(float _amount)
    {
         return _amount == null ? "null" : _amount.ToString();
    }
}
