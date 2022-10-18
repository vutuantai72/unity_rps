using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Spine.Unity;

public class PanelResult : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    public Animator panelResultAnim;
    public SelectType playerChoose;

    GameService gameService = GameService.@object;
    private static float timebet;
    private static int interval = 1; 
    private static float nextTime = 0;

    [SerializeField] private SkeletonGraphic resultAnimation;

    private void OnEnable()
    {
        panelResultAnim = GetComponent<Animator>();
        switch (gameService.resultStatus.Value)
        {
            case Constants.WIN:
                WinAnim(gameService.playerType.Value);
                break;
            case Constants.LOSE:
                LoseAnim(gameService.playerType.Value);
                break;
            case Constants.DRAW:
                DrawAnim(gameService.playerType.Value);
                break;
        }
        panelResultAnim.Play("open"); 
    }

    private void WinAnim(SelectType selectType)
    {
        switch (selectType)
        {
            case global::SelectType.scissors:
                //StartCoroutine(PlayAnimation(winScissor));
                resultAnimation.AnimationState.SetAnimation(0, "Player1_Win_Scissor", false);
                break;
            case global::SelectType.rock:
                //StartCoroutine(PlayAnimation(winRock));
                resultAnimation.AnimationState.SetAnimation(0, "Player1_Win_Rock", false);
                break;
            case global::SelectType.paper:
                //StartCoroutine(PlayAnimation(winPaper));
                resultAnimation.AnimationState.SetAnimation(0, "Player1_Win_Paper", false);
                break;
            default:
                break;
        }
    }

    private void LoseAnim(SelectType selectType)
    {
        switch (selectType)
        {
            case global::SelectType.scissors:
                //StartCoroutine(PlayAnimation(loseScissor));
                resultAnimation.AnimationState.SetAnimation(0, "Player2_Win_Rock", false);
                break;
            case global::SelectType.rock:
                //StartCoroutine(PlayAnimation(loseRock));
                resultAnimation.AnimationState.SetAnimation(0, "Player2_Win_Paper", false);
                break;
            case global::SelectType.paper:
                //StartCoroutine(PlayAnimation(losePaper));
                resultAnimation.AnimationState.SetAnimation(0, "Player2_Win_Scissor", false);
                break;
            default:
                break;
        }
    }

    private void DrawAnim(SelectType selectType)
    {
        switch (selectType)
        {
            case global::SelectType.scissors:
                //StartCoroutine(PlayAnimation(drawScissor));
                resultAnimation.AnimationState.SetAnimation(0, "Draw_Scissor", false);
                break;
            case global::SelectType.rock:
                //StartCoroutine(PlayAnimation(drawRock));
                resultAnimation.AnimationState.SetAnimation(0, "Draw_Rock", false);
                break;
            case global::SelectType.paper:
                //StartCoroutine(PlayAnimation(drawPaper));
                resultAnimation.AnimationState.SetAnimation(0, "Draw_Paper", false);
                break;
            default:
                break;
        }
    }

    public void EndAnim(){
        if(gameService.isGameTourStart.Value){
             gameService.gameState.Value = GameState.EndGame;
        }
        else{
            gameService.gameState.Value = GameState.CoinResult;  
        }
    }
}
