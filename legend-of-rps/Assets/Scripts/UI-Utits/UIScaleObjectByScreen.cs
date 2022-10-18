using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleObjectByScreen : MonoBehaviour
{
    private const float RatioDefault_9_16 = 9.0f / 16;
    private const float RatioDefault_9_18 = 9.0f / 18;
    private const float RatioDefault_3_4 = 3.0f / 4;
    private const float RatioDefault_2_3 = 2.0f / 3;
    private const float RatioDefault_9_19 = 9.0f / 19;
    private const float RatioDefault_9_22 = 9.0f / 22;

    [SerializeField] private float scale_3_4 = 1;
    [SerializeField] private float scale_2_3 = 1;
    [SerializeField] private float scale_9_16 = 1;
    [SerializeField] private float scale_9_18 = 1;
    [SerializeField] private float scale_9_19 = 1;
    [SerializeField] private float scale_9_22 = 1;

    private float cacheRatio;
    private void Awake()
    {
        ChangeObjectPosition();
        cacheRatio = Screen.width * 1.0f / Screen.height;
    }

#if UNITY_EDITOR
    private void Update()
    {
        float ratio = Screen.width * 1.0f / Screen.height;
        if (Mathf.Abs(cacheRatio / ratio - 1) > 0.01f)
        {
            cacheRatio = ratio;
            ChangeObjectPosition();
        }
    }
#endif

    private void ChangeObjectPosition()
    {
        float ratio = Screen.width * 1.0f / Screen.height;

        float[] scaleValue = new float[]
        {
                scale_3_4,
                scale_2_3,
                scale_9_16,
                scale_9_18,
                scale_9_19,
                scale_9_22
        };

        float[] distance = new float[]
        {
                Mathf.Abs(RatioDefault_3_4 / ratio - 1),
                Mathf.Abs(RatioDefault_2_3 / ratio - 1),
                Mathf.Abs(RatioDefault_9_16 / ratio - 1),
                Mathf.Abs(RatioDefault_9_18 / ratio - 1),
                Mathf.Abs(RatioDefault_9_19 / ratio - 1),
                Mathf.Abs(RatioDefault_9_22 / ratio - 1)
        };

        int closetIndex = 0;
        for (int i = 1; i < distance.Length; i++)
        {
            if (distance[closetIndex] > distance[i])
            {
                closetIndex = i;
            }
        }

        transform.localScale = new Vector3(scaleValue[closetIndex], scaleValue[closetIndex], scaleValue[closetIndex]);
    }
}
