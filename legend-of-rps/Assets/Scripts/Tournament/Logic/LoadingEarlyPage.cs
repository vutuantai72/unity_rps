using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingEarlyPage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerAvatar;
    [SerializeField]
    private Text[] playerName;

    GameService gameService = GameService.@object;

    private void OnEnable()
    {
        //gameService.gameState.Value = GameState.Selecting;
        for (int i = 0; i < playerAvatar.Length; i++)
        {
            ImageLoader avatar = playerAvatar[i].GetComponent<ImageLoader>();
            if (avatar != null)
            {
               avatar.OnShowImage(gameService.playerAvatarList.Value[i]);
            }
        }

        for (int i = 0; i < playerName.Length; i++)
        {
            playerName[i].text=gameService.playerNameList.Value[i];
        }
    }
}
