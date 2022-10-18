using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : /*UnityActiveSingleton<LoadingBar>*/ MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float progress = 0f;
    public Image barProgress;
    public bool isLoadingScene;

    private void Start()
    {
        isLoadingScene = false;
    }

    private void Update()
    {
        progress += Time.deltaTime * 0.3f;
        barProgress.fillAmount = progress;

        if (progress >= 0.9f && !isLoadingScene)
        {
            isLoadingScene = true;
            GameSceneServices.Instance.LoadScene(Constants.GAMEPLAY_SCENE);
        }
    }
}
