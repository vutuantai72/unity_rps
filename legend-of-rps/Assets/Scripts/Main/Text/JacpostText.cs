using System;

public class JacpostText : RxTextMeshPro<float>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.jackpotCoin;
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {
        return Math.Round(observationTargetIssueValue, 1).ToString();
    }
}
