using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform uiHandleToggle;
    [SerializeField] private RectTransform uiLabel;
    [SerializeField] private Toggle ToggleUI;

    Vector2 handlePos;
    Vector2 labelPos;

    [SerializeField] private Sprite[] newImg;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI txt;

    private void Awake()
    {
        handlePos = uiHandleToggle.anchoredPosition;
        labelPos = uiLabel.anchoredPosition;

        ToggleUI.onValueChanged.AddListener(OnSwitch);

        if (!ToggleUI.isOn)
            OnSwitch(true);
    }

    void OnSwitch(bool isOff)
    {
        if (isOff)
        {
            uiHandleToggle.anchoredPosition = handlePos;
            uiLabel.anchoredPosition = labelPos;
            txt.text = "Off";
            img.sprite = newImg[0];
        }
        else
        {
            uiHandleToggle.anchoredPosition = handlePos * -1;
            uiLabel.anchoredPosition = handlePos * 3/2;
            txt.text = "On";
            img.sprite = newImg[1];
        }
           
    }

    private void OnDestroy()
    {
        ToggleUI.onValueChanged.RemoveListener(OnSwitch);
    }
}
