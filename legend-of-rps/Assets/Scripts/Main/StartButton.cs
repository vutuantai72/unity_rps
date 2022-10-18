using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartButton : RxButton
{
    private const string MESSAGE_NO_COIN = "There is no more coin \n Please add more coins to play!!!";
    private const string MESSAGE_NO_INTERNET = "No internet connection,\n Please check your inernet";
    GameService gameService = GameService.@object;

    /// <summary>
    /// Start game for button.
    /// </summary>
    public override void OnClickAsync()
    {
        //AudioServices.Instance.PlayAudioStartGame();
        if (gameService.insertedCoin.Value <= 0)
        {
            gameService.isGameStart.Value = false;
            gameService.msgDialogTitleError.Value = "OOOPS!!";
            gameService.isShowDialogError.Value = true;
            gameService.msgDialogError.Value = MESSAGE_NO_COIN;
            return;
        }

        gameService.gameState.Value = GameState.Selecting;

        //GameDataServices.Instance.StartGame((result) =>
        //{
        //    if (result == null)
        //    {
        //        gameService.isGameStart.Value = false;
        //        gameService.isShowDialogError.Value = true;
        //        gameService.msgDialogError.Value = MESSAGE_NO_INTERNET;
        //        return;
        //    }

        //    gameService.gameDataBySever.Value = result;

        //    gameService.rouletteValue.Value = result.data.result.value;
        //    gameService.resultStatus.Value = result.data.result.status;
        //    gameService.insertedCoin.Value = result.data.coinInserted;
        //    gameService.winStreak.Value = result.data.winChain;
        //    gameService.isGetJackpost.Value = result.data.jackpot.coin > 0;

        //    gameService.Start();
        //});
    }
}