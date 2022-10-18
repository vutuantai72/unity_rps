using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EnemyMatchingContent : MonoBehaviour
{
    [SerializeField] private EnemyItemMatching enemyItem;
    [SerializeField] private Transform containItem;
    [SerializeField] private Sprite[] enemyAvtSprite;
    [SerializeField] private TextMeshProUGUI enemyBetValue;

    [SerializeField] private RectTransform contentSize;
    [SerializeField] private GridLayoutGroup gridSize;

    GameService gameService = GameService.@object;
    private float cycleLength = 15f;

    public void DisplayEnemyMatchingData()
    {
        Destroy();

        contentSize.sizeDelta = new Vector2(contentSize.sizeDelta.x, gridSize.cellSize.y * ((enemyAvtSprite.Length * 20) + 5));
        for(int i = 0; i < enemyAvtSprite.Length * 20; i++)
        {
            var avt = Random.Range(0, enemyAvtSprite.Length);
            var item = Instantiate(enemyItem, containItem);
            item.MatchingEnemyAvatar(enemyAvtSprite[avt]);
        }

        enemyBetValue.text = gameService.betCoinPVP.Value.ToString();
        containItem.transform.DOMove(new Vector3(0, 73, 0), cycleLength).SetLoops(-1, LoopType.Restart);

    }

    private void Destroy()
    {
        foreach (Transform child in containItem.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
