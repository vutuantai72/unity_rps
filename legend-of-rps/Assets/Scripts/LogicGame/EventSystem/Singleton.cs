using UnityEngine;
public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    static object _lock = new object();
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new T();
                }
            }
            return _instance;
        }
    }
}

public class UnitySingleton<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = (T)obj.AddComponent(typeof(T));
                }
            }

            return _instance;
        }
    }
    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void ResetSingleton()
    {
        if (_instance != null)
        {
            print("cal reset singleton");
            Destroy(_instance.gameObject);
            _instance = null;
        }
    }
}


public class UnityActiveSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = (typeof(T)).ToString();
                    _instance = (T)obj.AddComponent(typeof(T));
                }
            }

            return _instance;
        }
    }
    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void ResetSingleton()
    {
        if (_instance != null)
        {
            print("cal reset singleton");
            Destroy(_instance.gameObject);
            _instance = null;
        }
    }
}
