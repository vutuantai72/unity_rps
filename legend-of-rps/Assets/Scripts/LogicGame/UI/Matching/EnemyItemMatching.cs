using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyItemMatching : MonoBehaviour
{
    [SerializeField] private Image enemyAvt;

    public void MatchingEnemyAvatar(Sprite avatar)
    {
        enemyAvt.sprite = avatar;
    }
}
