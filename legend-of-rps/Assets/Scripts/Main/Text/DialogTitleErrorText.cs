public class DialogTitleErrorText : RxTextMeshPro<string>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.msgDialogTitleError;
    }

    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
    {
        return observationTargetIssueValue;
    }
}
