using System;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using WalletConnectSharp.Unity;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int accountIndex = 0;
    GameService gameService = GameService.@object;

    private void Awake()
    {
        gameService.setManager(this);
    }

    private async void Start()
    {
        if (!GameServiceControler.Instance.IsInitialize)
        {
            GameSceneServices.Instance.LoadScene(Constants.SPLASH_SCENE);
        }

        GameDataServices.Instance.GetJackpotCoin();
        gameService.isLoading.Value = false;

        gameService.WhenInitializing()
            .Subscribe(i => gameService.Reset())
            .AddTo(this);


        gameService
            .MakeResultStream()
            .Where(resultType => resultType == ResultType.Lose)
            .SelectMany(Observable.Timer(TimeSpan.FromSeconds(0.5f)))
            .Subscribe(i => DealWithTheLoss())
            .AddTo(this);

        gameService
            .MakeResultStream()
            .Where(resultType => resultType == ResultType.Draw)
            .SelectMany(Observable.Timer(TimeSpan.FromSeconds(0.5f)))
            .Subscribe(i => HandlingWhenItRainsAsync())
            .AddTo(this);


        gameService
            .MakeResultStream()
            .Where(resultType => resultType == ResultType.Win)
            .SelectMany(Observable.Timer(TimeSpan.FromSeconds(0.5f)))
            .Subscribe(l => DealWhenYouWin())
            .AddTo(this);

        GameDataServices.Instance.GetMarketNft();
        GameDataServices.Instance.GetWithdrawRatePolicy();
        GameDataServices.Instance.GetUserLRPSBalance();
        //Debug.LogError("Address ======: " + WalletConnect.ActiveSession.Accounts[accountIndex]);
        
    }

    private void DealWithTheLoss()
    {
        gameService.isAuto.Value = false;
        gameService.gameState.Value = GameState.InsertingCoin;
        gameService.InitializeInsertedCoin();
        gameService.InsertingCoin();
    }

    private void HandlingWhenItRainsAsync()
    {
        gameService.isGameStart.Value = false;
        gameService.gameState.Value = GameState.Selecting;
        //GameDataServices.Instance.StartGame((result) =>
        //{
        //    if (result == null || result.data == null || result.data.result == null)
        //    {
        //        gameService.isGameStart.Value = false;
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

    private void DealWhenYouWin()
    {
        gameService.StartCoinRoulette();
    }
}
