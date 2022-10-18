using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InsertCoinNum : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI insertCoin;
    [SerializeField] private GameObject StartGame;

    public static int insertCoinNum;

    public void UpdateText()
    {
        StartGame.SetActive(false);

        if (insertCoinNum <= 99)
        {
            insertCoinNum += 1;
            insertCoin.text = insertCoinNum.ToString();
        }
    }     
        
}
