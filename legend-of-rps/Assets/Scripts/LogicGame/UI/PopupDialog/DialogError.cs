using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogError : MonoBehaviour
{
    private const string MESSAGE_NO_COIN = "There is no more coin \n Please add more coins to play!!!";
    private const string MESSAGE_NO_INTERNET = "No internet connection,\n Please check your inernet";
    private const string MESSAGE_NO_COMING_SOON = "This feature will be coming soon";
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite[] newImg;
    [SerializeField] private Image img;

    GameService gameService = GameService.@object;

    private void OnEnable()
    {
        animator.Play("OpenPopUp");
        img.sprite = newImg[Random.Range(0, newImg.Length)];
    }

    public void Close()
    {
        animator.Play("ClosePopUp");
        gameService.isShowDialogError.Value = false;
        if (gameService.dataCancelRoom.Value.reason == (int)ReasonMode.CONTINUETIMEOUT)
        {
            gameService.ResetGamePVP();
        }

    }

}
