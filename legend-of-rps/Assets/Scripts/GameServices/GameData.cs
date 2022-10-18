using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameContans
{
    public static string KEY_SAVE_USER_COIN = "KEY_SAVE_USER_COIN";
    public static string KEY_SAVE_TODAY_COIN = "KEY_SAVE_TODAY_COIN";
    public static string KEY_SAVE_INSERTED_COIN = "KEY_SAVE_INSERTED_COIN";
    public static string KEY_SAVE_JACKPOT_COIN = "KEY_SAVE_JACKPOT_COIN";
}

public static class GameData
{
    #region User coin.
    private static int uC = 200;

    public static int UserCoin
    {
        get => uC;
        set
        {
            if (uC == value)
                return;
            uC = value;
            PlayerPrefs.SetInt(GameContans.KEY_SAVE_USER_COIN, uC);
        }
    }
    #endregion

    #region Today coin.
    private static int tC = 0;

    public static int TodayCoin
    {
        get => tC;
        set
        {
            if (tC == value)
                return;
            tC = value;
            PlayerPrefs.SetInt(GameContans.KEY_SAVE_TODAY_COIN, tC);
        }
    }
    #endregion

    #region Inserted coin.
    private static int iC = 0;

    public static int InsertedCoin
    {
        get => iC;
        set
        {
            if (iC == value)
                return;
            iC = value;
            PlayerPrefs.SetInt(GameContans.KEY_SAVE_TODAY_COIN, iC);
        }
    }
    #endregion

    #region JackpotCoin
    private static float jC = 0;
    public static float JackpotCoin
    {
        get => jC;
        set
        {
            if (jC == value)
                return;
            jC = value;
            PlayerPrefs.SetFloat(GameContans.KEY_SAVE_JACKPOT_COIN, jC);
        }
    }
    #endregion
}
