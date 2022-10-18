using UnityEngine;
using UnityEngine.UI;

public class CameraRatioManager : MonoBehaviour
{
    public float originWidth = 560;
    public float originHeight = 960;

    public CanvasScaler canvasScaler;

    void Awake()
    {
        if (canvasScaler != null)
        {
            originWidth = canvasScaler.referenceResolution.x;
            originHeight = canvasScaler.referenceResolution.y;
        }
    }

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float ratio = originWidth / originHeight;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float width = screenRatio / ratio > 1 ? ratio / screenRatio : 1;
        float height = screenRatio / ratio > 1 ? 1 : screenRatio / ratio;
        Rect camRect = cam.rect;
        camRect.width = width;
        camRect.height = height;
        camRect.x = (1 - width) / 2.0f;
        camRect.y = (1 - height) / 2.0f;
        cam.rect = camRect;
    }
}