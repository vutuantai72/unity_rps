public class SpinTurnText : RxTextMeshPro<int>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.spinTurn;
    }

    protected override string DisplayFormatSpecified(int observationTargetIssueValue)
    {
        return $"You have {observationTargetIssueValue} time to spinning!";
    }
}
