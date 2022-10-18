using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading.Tasks;

public class InsertCoin : MonoBehaviour
{
    private const string MESSAGE_NO_INTERNET = "No Internet connection,\n Please check your Internet";
    private readonly int insertCoin = Animator.StringToHash("InsertCoin");
    [SerializeField] private Animator animator;
    GameService gameService = GameService.@object;

    Vector3 offsetPosition = new Vector3(0.0f, 3.0f, 3.0f);
    Vector3 firstPosition = new Vector3();
    Vector3 startPosition = new Vector3();

    private void Start()
    {
        firstPosition = this.transform.position;
        startPosition = firstPosition - offsetPosition;

        SetStartPosition(startPosition);

        gameService.insertCoinSubject
            .Subscribe(u => this.transform.position = startPosition)
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(u => gameService.isInsertCoin.Value)
            .Subscribe(u => MoveCoin())
            .AddTo(this);
    }

    private void SetStartPosition(Vector3 vector)
    {
        this.transform.position = vector; 
    }

    private void MoveCoin()
    {
        Vector3 myPosition = this.gameObject.transform.position;
        this.transform.position = new Vector3(myPosition.x, myPosition.y + 0.3f, myPosition.z + 0.3f);

        if (this.transform.position.y > firstPosition.y && this.transform.position.z > firstPosition.z)
        {
            //if (gameService.betCoin.Value <= 0)
            //{
            //    gameService.betCoin.Value++;
            //}

            animator.Play(insertCoin);
            gameService.isInsertCoin.Value = false;

            this.transform.position = startPosition;

            GameDataServices.Instance.AddCoin(gameService.userEmail.Value);
            if (gameService.gameState.Value == GameState.InsertingCoin && gameService.isAuto.Value)
            {
                if (gameService.isGameStart.Value)
                {
                    return;
                }

                gameService.isGameStart.Value = true;
                gameService.Start();
            }

        }
    }
}
