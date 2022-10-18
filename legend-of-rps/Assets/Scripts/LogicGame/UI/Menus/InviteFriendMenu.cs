using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InviteFriendMenu : MonoBehaviour
{
    [SerializeField] private RectTransform GameMissionUI;
    [SerializeField] private RectTransform InviteFriendUI;
    [SerializeField] private RectTransform SuccessMessage;
    //[SerializeField] private RectTransform SideMenu_UI;

    [SerializeField] private TextMeshProUGUI inviteCode;
    GameService gameService = GameService.@object;

    private void LateUpdate()
    {
        inviteCode.text = gameService.userID.Value;        
    }

    public void OpenUI()
    {
        AudioServices.Instance.PlayClickAudio();
        InviteFriendUI.gameObject.SetActive(true);
        //GameMissionUI.gameObject.SetActive(false);

        //SideMenu_UI.gameObject.transform.DOMove(new Vector3(9.9f, SideMenu_UI.transform.position.y, SideMenu_UI.transform.position.z), 1f);
    }

    public void CloseUI()
    {
        InviteFriendUI.gameObject.SetActive(false);
        SuccessMessage.gameObject.SetActive(false);
    }

    public void CopyInviteCode()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = inviteCode.text;
        textEditor.SelectAll();
        textEditor.Copy();
        SuccessMessage.gameObject.SetActive(true);
    }

    public void PasteInviteCode()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.multiline = true;
        textEditor.Paste();
    }

}
