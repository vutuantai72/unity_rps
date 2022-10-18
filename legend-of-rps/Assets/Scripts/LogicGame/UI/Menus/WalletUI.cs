using Nethereum.Web3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Unity;

public class WalletUI : MonoBehaviour
{
    private GameService gameService = GameService.@object;

    public void OnEnable()
    {
        gameService.isLoading.Value = true;
        GameDataServices.Instance.GetUserLRPSBalance();
        gameService.isLoading.Value = false;
    }
}
