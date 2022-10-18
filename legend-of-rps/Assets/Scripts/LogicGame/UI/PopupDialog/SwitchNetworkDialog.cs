using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchNetworkDialog : MonoBehaviour
{
    GameService gameService = GameService.@object;

    public void Close()
    {
        gameService.isSwitchNetworkDialog.Value = false;
    }
}
