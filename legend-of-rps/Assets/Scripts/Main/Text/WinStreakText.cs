public class WinStreakText : RxTextMeshPro<int>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.winStreak;
    }

    protected override string DisplayFormatSpecified(int observationTargetIssueValue)
    {
        return observationTargetIssueValue.ToString();
    }
}
