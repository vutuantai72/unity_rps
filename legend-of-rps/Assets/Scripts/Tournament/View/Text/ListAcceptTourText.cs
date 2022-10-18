public class ListAcceptTourText : RxTextMeshPro<int>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = gameService.listUserAcceptTour;
    }

    protected override string DisplayFormatSpecified(int observationTargetIssueValue)
    {
        return string.Format($"{0}/8", objectObservation);
    }
}
