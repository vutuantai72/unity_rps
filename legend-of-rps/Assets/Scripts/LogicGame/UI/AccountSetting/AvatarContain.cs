using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarContain : MonoBehaviour
{
    [SerializeField] private RectTransform bg_Avatar;
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI nameAvatar;

    public void SetUpAvatar(Sprite avatarSprite, string name)
    {
        avatar.sprite = avatarSprite;
        avatar.rectTransform.sizeDelta = new Vector2(320, 320);
        nameAvatar.text = name;
    }
}
