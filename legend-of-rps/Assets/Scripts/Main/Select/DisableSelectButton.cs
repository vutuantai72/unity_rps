using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableSelectButton : MonoBehaviour
{
    private Button selectTypeButton;
    private GameService gameService = GameService.@object;
    // Start is called before the first frame update
    void Start()
    {
        selectTypeButton = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameService.gameState.Value != GameState.Selecting)
        {
            selectTypeButton.interactable = false;
        }

        else if (gameService.gameState.Value != GameState.InsertingCoin && gameService.isGameStart.Value == true)
        {
            selectTypeButton.interactable = false;
        }
        else
        {
            selectTypeButton.interactable = true;
        }
    }
}
