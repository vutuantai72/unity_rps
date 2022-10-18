using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleVectorObjectByScreen : MonoBehaviour
{
    private const float RatioDefault_9_16 = 9.0f / 16;
    private const float RatioDefault_9_18 = 9.0f / 18;
    private const float RatioDefault_3_4 = 3.0f / 4;
    private const float RatioDefault_2_3 = 2.0f / 3;
    private const float RatioDefault_9_19 = 9.0f / 19;
    private const float RatioDefault_9_22 = 9.0f / 22;

    [SerializeField] private Vector3 scale_3_4 = Vector3.one;
    [SerializeField] private Vector3 scale_2_3 = Vector3.one;
    [SerializeField] private Vector3 scale_9_16 = Vector3.one;
    [SerializeField] private Vector3 scale_9_18 = Vector3.one;
    [SerializeField] private Vector3 scale_9_19 = Vector3.one;
    [SerializeField] private Vector3 scale_9_22 = Vector3.one;

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

        Vector3[] scaleValue = new Vector3[]
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

        transform.localScale = new Vector3(scaleValue[closetIndex].x, scaleValue[closetIndex].y, scaleValue[closetIndex].z);
    }
}
