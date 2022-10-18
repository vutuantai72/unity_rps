using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterInviteCodeDialog : MonoBehaviour
{
    GameService gameService = GameService.@object;

    [SerializeField] private TMP_InputField enterInviteCode;
    [SerializeField] private Button continueButton;

    public void CloseInviteCode()
    {
        gameService.isShowInviteDialog.Value = false;
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(enterInviteCode.text))
        {
            continueButton.interactable = true;
        }
        else
            continueButton.interactable = false;
    }

    public void EnterInviteCode()
    {
        GameDataServices.Instance.InputInviteCode(enterInviteCode.text);
        gameService.isShowInviteDialog.Value = false;
    }
}
