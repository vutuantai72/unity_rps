using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    GameService gameService = GameService.@object;
    public void OnChanged(bool isActiver)
    {
        gameService.isAuto.Value = toggle.isOn;
    }
}
