public class TimeLeftText : RxTextMeshPro<float>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.timeBet;
    }

    protected override string DisplayFormatSpecified(float observationTargetIssueValue)
    {
        return $"{observationTargetIssueValue}";
    }
}
