using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMarketDisplay : MonoBehaviour
{
    [SerializeField] private List<Sprite> itemType;
    [SerializeField] private MarketPlaceItem marketItem;
    [SerializeField] private Transform containItem;

    GameService gameService = GameService.@object;

    public void DisplayMarketItem()
    {
        Destroy();

        for (int i = 0; i < 15; i++)
        {
            var item = Instantiate(marketItem, containItem);
            item.SetupItemMarket(itemType[0], null, "$999", "#0001");
        }
    }
    private void Destroy()
    {
        foreach (Transform child in containItem.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
