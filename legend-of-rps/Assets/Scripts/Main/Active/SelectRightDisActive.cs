using UniRx;

public class SelectRightDisActive : RxActive
{
    GameService gameService = GameService.@object;
    public SelectType selectType;

    protected override void ObservationTargetDesignation()
    {
        objectObservation = Observable.CombineLatest(gameService.gameState, gameService.playerType,
            (state, playerType) => (state != GameState.Selecting && playerType == selectType));
    }
}
