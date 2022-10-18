using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NFTCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image NFTImage;
    [SerializeField] private Image NFTFrame;

    [SerializeField] private Sprite rankARD;
    [SerializeField] private Sprite rankS;
    [SerializeField] private Sprite rankA;
    [SerializeField] private Sprite rankB;
    [SerializeField] private Sprite rankC;
    [SerializeField] private Sprite rankD;

    [SerializeField] private Sprite scissor;
    [SerializeField] private Sprite rock;
    [SerializeField] private Sprite paper;
    [SerializeField] private Sprite machine;

    public void LoadNFTRankSprite(string cardRank)
    {
        rankText.text = cardRank;
        switch (cardRank)
        {
            case "ARD":
                NFTFrame.sprite = rankARD;
                break;
            case "S":
                NFTFrame.sprite = rankS;
                break;
            case "A":
                NFTFrame.sprite = rankA;
                break;
            case "B":
                NFTFrame.sprite = rankB;
                break;
            case "C":
                NFTFrame.sprite = rankC;
                break;
            case "D":
                NFTFrame.sprite = rankD;
                break;
            default:
                NFTFrame.sprite = rankARD;
                break;
        }
    }

    public void LoadNFTImage(string cardImage)
    {
        //string imageFirstLetter = cardImage.Substring(0, 1);
        
        if (cardImage.Contains("scissor"))
        {
            NFTImage.sprite = scissor;
        }
        if (cardImage.Contains("rock"))
        {
            NFTImage.sprite = rock;
        }
        if (cardImage.Contains("paper"))
        {
            NFTImage.sprite = paper;
        }
        if (cardImage.Contains("machine"))
        {
            NFTImage.sprite = machine;
        }
        //switch (imageFirstLetter)
        //{
        //    case "1":
        //        NFTImage.sprite = scissor;
        //        break;
        //    case "2":
        //        NFTImage.sprite = rock;
        //        break;
        //    case "3":
        //        NFTImage.sprite = paper;
        //        break;
        //    case "4":
        //        NFTImage.sprite = machine;
        //        break;
        //    default:
        //        NFTImage.sprite = machine;
        //        break;
        //}
    }

    public void LoadNFTInfo(string NFTname, string NFTid)
    {
        nameText.text = NFTname;
        idText.text = "#"+NFTid;
    }
}
