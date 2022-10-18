using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    GameService gameService = GameService.@object;

    [SerializeField] private List<RectTransform> tutorialDialog;
    [SerializeField] private RectTransform rootsTutorial;
    [SerializeField] private RectTransform rootsDemo; 

    private int current = 0;

    private void Start()
    {
        tutorialDialog[0].gameObject.SetActive(true);

        if (gameService.isCompleteTutorial.Value)
            rootsTutorial.gameObject.SetActive(false);


        //if (string.IsNullOrEmpty(gameService.userWall.Value))
        //{
        //    rootsDemo.gameObject.SetActive(gameService.isDemoGame.Value);
        //}
    }

    #region Tutorial
    public void OnNext()
    {
        current++;
        UpdateTutorial();

        if(current == tutorialDialog.Count)
        {
            gameService.isCompleteTutorial.Value = true;
            rootsTutorial.gameObject.SetActive(!gameService.isCompleteTutorial.Value);
            PlayerPrefs.SetString(GameConstants.KEY_SAVE_COMPLETE_TUTORIAL, gameService.isCompleteTutorial.Value.ToString());
        }
    }

    public void OnSkip()
    {
        current = tutorialDialog.Count;
        UpdateTutorial();

        gameService.isCompleteTutorial.Value = true;
        rootsTutorial.gameObject.SetActive(!gameService.isCompleteTutorial.Value);
        PlayerPrefs.SetString(GameConstants.KEY_SAVE_COMPLETE_TUTORIAL, gameService.isCompleteTutorial.Value.ToString());
    }

    private void UpdateTutorial()
    {
        AudioServices.Instance.PlayClickAudio();

        for (int i = 0; i < tutorialDialog.Count; i++)
        {
            tutorialDialog[i].gameObject.SetActive(i == current);
        }
    }
    #endregion

    public void ClosePopupConnectWallet()
    {
        gameService.isDemoGame.Value = false;
        rootsDemo.gameObject.SetActive(gameService.isDemoGame.Value);
    }
}
