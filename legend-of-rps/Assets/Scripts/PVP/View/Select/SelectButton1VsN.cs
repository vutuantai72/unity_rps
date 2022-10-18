using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton1VsN : RxButton
{
    [SerializeField] private SelectType selectType;
    private GameService gameService = GameService.@object;

    public override void OnClickAsync()
    {
        Debug.LogError("SELECT_BUTTON: GAME_STARE >>>>>> " + gameService.gameState.Value.ToString());
        Debug.LogError("SELECT_BUTTON: GAME_START >>>>>> " + gameService.isGameStart.Value.ToString());

        VibrationService.Instance.PlayHapticsHeavyImpact();

        if (gameService.gameState.Value != GameState.Selecting)
        {
            return;
        }
        gameService.SelectType1VsN(selectType);
        
    }
}
