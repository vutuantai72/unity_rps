using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jacpost : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    private void Start()
    {
        gameObject.SetActive(true);
        Invoke(nameof(OnDisableJacpos), 3.0f);
    }

    private void OnDisableJacpos()
    {
        gameObject.SetActive(false);
    }
}
