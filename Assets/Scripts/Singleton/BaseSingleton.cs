using UnityEngine;

public abstract class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool dontDestroyOnLoad;

    private static volatile T _instance;
    private static object _lock = new object();
    
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType((typeof(T))) as T;

                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            GameObject singletonObject = new GameObject(typeof(T).ToString());
                            _instance = singletonObject.AddComponent<T>();
                        }
                    }
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}