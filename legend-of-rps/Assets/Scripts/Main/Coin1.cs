using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class Coin1 : MonoBehaviour
{
    GameService gameService = GameService.@object;

    [Obsolete]
    private void Start()
    {
        gameService.gameState
            .Where(g => g != GameState.GetCoins)
            .SelectMany(Observable.Timer(TimeSpan.FromSeconds(1f)))
            .Subscribe(l => { DestroyObject(this.gameObject, 2.0f); })
            .AddTo(this);

        // On Move Tween.
        //OnMoveTween();
    }

    [Obsolete]
    private void OnMoveTween()
    {
        if (gameService.targetTransform != null)
        {
            this.transform.DOMove(gameService.targetTransform.position, 0.75f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    this.gameObject.SetActive(false);
                    ShowTextAnim();
                });
        }

        DestroyObject(this.gameObject, 2.0f);
    }

    private void ShowTextAnim()
    {
        GameObject @object = GameObject.FindGameObjectWithTag("TodayCoin");
        Animator animator = @object.GetComponent<Animator>();
        animator.Play("Play");
    }
}
