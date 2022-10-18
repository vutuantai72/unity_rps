using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEndGame : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;

    GameService gameService = GameService.@object;

    public ImageLoader playerAvatar;

    private void OnEnable()
    {
        playerAvatar.OnShowImage(gameService.userAvatar.Value);
    }
    
}
