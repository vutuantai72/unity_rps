using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RankingContent : MonoBehaviour
{
    [SerializeField] private DialogRanking dialogRanking;
    [SerializeField] private RankingItemContain rankingItem;
    [SerializeField] private Transform container;
    [SerializeField] private Sprite spriteAvatar;
    private GameService gameService = GameService.@object;

    #region Flexible content layout
    [SerializeField] RectTransform contentRanking;
    [SerializeField] GridLayoutGroup contentLayout;
    #endregion

    public void Show()
    {
        Destroy();
#if UNITY_EDITOR
        string maleAvaPath = $"{Application.dataPath}/Resources/Avatar/Male";
        string femaleAvaPath = $"{Application.dataPath}/Resources/Avatar/Female";
#else
        string maleAvaPath = $"{Application.persistentDataPath}/Resources/Avatar/Male";
        string femaleAvaPath = $"{Application.persistentDataPath}/ResourcesAvatar/Female";
#endif
        var rankingData = gameService.rankingModel.Value;
        contentRanking.sizeDelta = new Vector2(contentRanking.sizeDelta.x, contentLayout.cellSize.y * /*rankingData.total*/ 11);
        try
        {
            rankingData.data.ForEach(data =>
            {
                Sprite avatar = null;
                var item = Instantiate(rankingItem, container);
                if (Resources.Load<Texture2D>($"Avatar/Male/{data.avatar}") != null)
                {
                    var texture = Resources.Load<Texture2D>($"Avatar/Male/{data.avatar}");

                    if (texture != null)
                    {
                        avatar = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                    }
                }
                else
                {
                    if (Resources.Load<Texture2D>($"Avatar/Female/{data.avatar}") != null)
                    {
                        var texture = Resources.Load<Texture2D>($"Avatar/Female/{data.avatar}");

                        if (texture != null)
                        {
                            avatar = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                        }
                    }
                }
                item.Setup(data.name, data.coin, rankingData.data.IndexOf(data) + 1, avatar != null ? avatar : spriteAvatar, rankingData.data.IndexOf(data) == 0);
            });
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private void Destroy()
    {
        foreach (Transform child in container.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
