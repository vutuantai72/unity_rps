public class DialogErrorText : RxTextMeshPro<string>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.msgDialogError;
    }

    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
    {
        return observationTargetIssueValue;
    }
}
