using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketPlaceItem : MonoBehaviour
{
    [SerializeField] private Image itemType;
    [SerializeField] private Image itemLogo;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemID;

    public void SetupItemMarket(Sprite type, Sprite logo, string price, string id)
    {
        itemType.sprite = type;
        itemLogo.sprite = logo;
        itemPrice.text = price;
        itemID.text = id;
    }
}
