using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButton : RxButton
{
    private const string MESSAGE_UNABLE_ADD_COIN = "Unable to add more coins";
    GameService gameService = GameService.@object;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public override void OnClickAsync()
    {
        if(timer <= 0.5f)
        {
            return;
        }

        if (gameService.userCoin.Value < 1 || gameService.insertedCoin.Value >= 99 || gameService.isInsertCoin.Value)
        {
            OnShowInvalidCoin();
            return;
        }

        gameService.isInsertCoin.Value = true;
        gameService.insertedCoin.Value += 1;
        gameService.todayCoin.Value -= 1;

        PlayerPrefs.SetFloat(GameConstants.KEY_SAVE_TODAY_COIN, gameService.todayCoin.Value);
        gameService.InsertCoin();
        AudioServices.Instance.PlayInsertCoin();
        timer = 0;
    }

    public void OnShowInvalidCoin()
    {
        gameService.msgDialogTitleError.Value = "OOOPS!!";
        gameService.msgDialogError.Value = MESSAGE_UNABLE_ADD_COIN;
        gameService.isShowDialogError.Value = true;
    }
}
