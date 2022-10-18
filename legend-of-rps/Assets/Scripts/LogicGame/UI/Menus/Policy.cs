using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy : MonoBehaviour
{
    [SerializeField] private RectTransform policyUI;

    private void Start()
    {
        policyUI.gameObject.SetActive(false);   
    }

    public void ShowPolicy()
    {
        policyUI.gameObject.SetActive(true);
    }

    public void HidePolicy()
    {
        policyUI.gameObject.SetActive(false);
    }
}
