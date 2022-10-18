public class BalanceCoinText : RxTextMeshPro<float>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.userCoin;
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {
        return $"Balance: {observationTargetIssueValue}";
    }
}
