using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GlobalData : MonoBehaviour
{
    private static readonly Dictionary<string, GameObject> Cache = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Cache.ContainsKey(name))
        {
#if UNITY_EDITOR
            Debug.LogWarning("Object [" + name + "] exists. Destroy new one");
#endif
            Object.DestroyImmediate(this.gameObject);
        }
        else
        {
            Cache[name] = gameObject;
        }
    }
}