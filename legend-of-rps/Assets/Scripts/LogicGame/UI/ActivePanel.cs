using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePanel : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;

    // Update is called once per frame
    void Update()
    {
        if (LoseActive.isLose)
        {
            losePanel.SetActive(false);
        }
    }
}
