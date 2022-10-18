using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectWalletDialog : MonoBehaviour
{
    GameService gameService = GameService.@object;
    [SerializeField] private RectTransform connectWalletDialog;

    private void Start()
    {
        Debug.LogError("Dialog connect wallet: " + gameService.userWall.Value.Contains("0x"));
        Debug.LogError("Dialog connect wallet: " + !gameService.userWall.Value.Contains("0x"));
        Debug.LogError("Dialog connect wallet: " + gameService.userWall.Value);
        if (string.IsNullOrEmpty(gameService.userWall.Value) || !gameService.userWall.Value.Contains("0x"))
        {
            Debug.LogError("Dialog connect wallet [SHOW DIALOG]: " + gameService.userWall.Value.Contains("0x"));
            connectWalletDialog.gameObject.SetActive(true);
        }
    }

    public void CloseDialog()
    {
        connectWalletDialog.gameObject.SetActive(false);
    }

    //Open wallet UI
    public void ConnectWallet()
    {
        GameDataServices.Instance.OnConnectWallet();
    }
}
