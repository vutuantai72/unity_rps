using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchingInfo_ITEM : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerBetValue;

    public void SetupInfo(string name, int betValue)
    {
        playerName.text = name;
        playerBetValue.text = betValue.ToString();
    }
}
