using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectButton : RxButton
{
    [SerializeField] private SelectType selectType;
    private GameService gameService = GameService.@object;

    public override void OnClickAsync()
    {
        Debug.Log("SELECT_BUTTON: GAME_STARE >>>>>> " + gameService.gameState.Value.ToString());
        Debug.Log("SELECT_BUTTON: GAME_START >>>>>> " + gameService.isGameStart.Value.ToString());

        VibrationService.Instance.PlayHapticsHeavyImpact();

        if (gameService.gameState.Value != GameState.Selecting)
        {
            return;
        }

        gameService.SelectType(selectType);
    }
}
