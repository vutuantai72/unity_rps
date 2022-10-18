using UnityEngine;
using System.Collections.Generic;
using System;

public class GameConstants
{
    public readonly static string KEY_SAVE_NEW_USER = "KEY_SAVE_NEW_USER";
    public readonly static string KEY_SAVE_PRVE_TIME = "KEY_SAVE_PRVE_TIME";
    public readonly static string KEY_SAVE_TODAY_COIN = "KEY_SAVE_TODAY_COIN";
    public readonly static string KEY_SAVE_COMPLETE_TUTORIAL = "KEY_SAVE_COMPLETE_TUTORIAL";
    public readonly static string KEY_SAVE_DAILY = "";
    public readonly static string KEY_SAVE_CONNECT_WALLET = "true";
    public readonly static string KEY_SAVE_DAILY_LOGIN = "0";
}

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> SplashElements;

    private float timne = 0;
    private void Start()
    {
        GameServiceControler.Instance.Initialize();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.multiTouchEnabled = false;

        //SplashElements[0].SetActive(true);
        //SplashElements[1].SetActive(false);
        //var nowTime = DateTime.Now;
        //var prevTimeSaved = PlayerPrefs.GetString(GameConstants.KEY_SAVE_PRVE_TIME, "");

        //if (string.IsNullOrEmpty(prevTimeSaved))
        //{
        //    PlayerPrefs.SetString(GameConstants.KEY_SAVE_PRVE_TIME, DateTime.Now.ToString());

        //    if (DateTime.TryParse(prevTimeSaved, out var prevTime))
        //    {
        //        if (nowTime.Day != prevTime.Day)
        //        {
        //            PlayerPrefs.SetFloat(GameConstants.KEY_SAVE_TODAY_COIN, 0);
        //        }
        //    }
        //}

        //GameService.@object.todayCoin.Value = PlayerPrefs.GetFloat(GameConstants.KEY_SAVE_TODAY_COIN, 0);
        GameService.@object.isNewUser.Value = Convert.ToBoolean(PlayerPrefs.GetString(GameConstants.KEY_SAVE_NEW_USER, false.ToString()));
        GameService.@object.isCompleteTutorial.Value = Convert.ToBoolean(PlayerPrefs.GetString(GameConstants.KEY_SAVE_COMPLETE_TUTORIAL, false.ToString()));
    }

    private void FixedUpdate()
    {
        if (timne > 30)
            return;

        timne += 1;

        if(timne >= 15)
        {
            //SplashElements[0].SetActive(false);
            //SplashElements[1].SetActive(true);
        }

        if(timne == 30)
        {
            GoToMenu();
        }
    }

    private void GoToMenu()
    {
        GameSceneServices.Instance.LoadScene(Constants.MENU_SCENE);
    }
}
