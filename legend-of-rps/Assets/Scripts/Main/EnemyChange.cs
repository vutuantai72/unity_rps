using UniRx;
using UniRx.Triggers;

public class EnemyChange : RxView<Unit>
{
    GameService gameService = GameService.@object;
    protected override void ObservationTargetDesignation()
    {
        objectObservation = this.UpdateAsObservable().Where(u => gameService.gameState.Value == GameState.Selecting || gameService.gameState.Value == GameState.Selected);
    }

    protected override void ObservingObjectValueIssuance(Unit issueValue)
    {
        GameService.@object.RandomEnemy();
    }
}
