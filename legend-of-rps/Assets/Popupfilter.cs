using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popupfilter : MonoBehaviour
{
    [SerializeField] private Animator animator;

    GameService gameService = GameService.@object;

    private void OnEnable()
    {
        animator.Play("OpenPopUp");
    }

    public void Close()
    {
        animator.Play("ClosePopUp");
        gameService.isShowPopupFilter.Value = false;
    }

    public void Open()
    {
        animator.Play("OpenPopUp");
        gameService.isShowPopupFilter.Value = true;
    }
}
