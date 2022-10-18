using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarketType
{
    HandNFT = 0,
    ArcadeMachines,
    HandNFTBox,
    MachineBox
}

public class MarketPlaceMenu : MonoBehaviour
{ 
    [SerializeField] private List<ItemMarketDisplay> itemsMarket;
    
    private MarketType marketType = MarketType.HandNFT;

    private void Start()
    {
        UpdateUI(MarketType.HandNFT);
    }

    public void OpenHandNFT()
    {
        UpdateUI(MarketType.HandNFT);
    }

    public void OpenArcadeMachines()
    {
        UpdateUI(MarketType.ArcadeMachines);
    }

    public void OpenHandNFTBox()
    {
        UpdateUI(MarketType.HandNFTBox);
    }

    public void OpenMachineBox()
    {
        UpdateUI(MarketType.MachineBox);
    }

    private void UpdateUI(MarketType marketElement)
    {
        AudioServices.Instance.PlayClickAudio();

        this.marketType = marketElement;
        for (int i = 0; i < itemsMarket.Count; i++)
        {
            var isActive = i == (int)this.marketType;
            itemsMarket[i].gameObject.SetActive(isActive);
            if (isActive)
            {
                itemsMarket[i].DisplayMarketItem();
            }
        }
    }
}
