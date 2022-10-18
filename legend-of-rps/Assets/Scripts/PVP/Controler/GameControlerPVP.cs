using System;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameControlerPVP : MonoBehaviour
{
    
    GameService gameService = GameService.@object;


   private void Start()
    {
        if (!GameServiceControler.Instance.IsInitialize)
        {
            GameSceneServices.Instance.LoadScene(Constants.SPLASH_SCENE);
        }
        gameService.isLoading.Value = false;
        gameService.isShowLoadingWaiting.Value = false;

        gameService.WhenInitializing()
            .Subscribe(i => gameService.Reset())
            .AddTo(this);

    }

}
