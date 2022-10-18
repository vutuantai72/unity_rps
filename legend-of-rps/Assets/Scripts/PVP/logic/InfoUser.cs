using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfoUser : MonoBehaviour
{
    GameService gameService = GameService.@object;
    [SerializeField] private ImageLoader playerAvatar;
    [SerializeField] private ImageLoader enemyAvatar;
    // Start is called before the first frame update
    void OnEnable()
    {
        playerAvatar.OnShowImage(gameService.userAvatar.Value);
        enemyAvatar.OnShowImage(gameService.enemyAvatar.Value);
    }
}
