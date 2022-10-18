using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingSoonDialog : MonoBehaviour
{
    [SerializeField] private RectTransform comingSoonUI;
    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        comingSoonUI.gameObject.SetActive(true);
    }

    public void Hide()
    {
        comingSoonUI.gameObject.SetActive(false);
    }
}
