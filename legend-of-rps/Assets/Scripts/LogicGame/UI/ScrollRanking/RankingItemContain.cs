using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingItemContain : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text coinTxt;
    [SerializeField] TMP_Text rankTxt;
    [SerializeField] GameObject ribbon;
    [SerializeField] Image avatar;
    [SerializeField] GameObject star;

    public void Setup(string name, float coin, int rank, Sprite sprite, bool isRank1 = false)
    {
        text.text = name;
        coinTxt.text = string.Format("{0} COIN", coin);
        rankTxt.text = rank.ToString();
        ribbon.SetActive(isRank1);
        if (rank > 3)
        {
            star.SetActive(false);
        }
        avatar.sprite = sprite;
    }
}
