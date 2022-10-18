using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPositionObjectByScreen : UnityActiveSingleton<UIPositionObjectByScreen>
{
    private const float RatioDefault_9_16 = 9.0f / 16;
    private const float RatioDefault_9_18 = 9.0f / 18;
    private const float RatioDefault_3_4 = 3.0f / 4;
    private const float RatioDefault_2_3 = 2.0f / 3;
    private const float RatioDefault_9_19 = 9.0f / 19;
    private const float RatioDefault_9_22 = 9.0f / 22;

    [SerializeField] private Vector3 position_3_4 = Vector3.zero;
    [SerializeField] private Vector3 position_2_3 = Vector3.zero;
    [SerializeField] private Vector3 position_9_16 = Vector3.zero;
    [SerializeField] private Vector3 position_9_18 = Vector3.zero;
    [SerializeField] private Vector3 position_9_19 = Vector3.zero;
    [SerializeField] private Vector3 position_9_22 = Vector3.zero;

    private float cacheRatio;
    public float screen9_22 = RatioDefault_9_22;


    private void Awake()
    {
        ChangeObjectSize();
        cacheRatio = Screen.width * 1.0f / Screen.height;
    }
#if UNITY_EDITOR
    private void Update()
    {
        float ratio = Screen.width * 1.0f / Screen.height;
        if (Mathf.Abs(cacheRatio / ratio - 1) > 0.01f)
        {
            cacheRatio = ratio;
            ChangeObjectSize();
        }
    }
#endif

    private void ChangeObjectSize()
    {
        float ratio = Screen.width * 1.0f / Screen.height;

        Vector3[] positionValue = new Vector3[]
        {
                position_3_4,
                position_2_3,
                position_9_16,
                position_9_18,
                position_9_19,
                position_9_22
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

        transform.localPosition = positionValue[closetIndex];
    }
}
