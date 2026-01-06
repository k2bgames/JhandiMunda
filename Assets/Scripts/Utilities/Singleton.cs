using Unity.VisualScripting;
using UnityEngine;

public interface ISingletonType { }
/// <summary>
/// Singleton that is found and is not destroyed on load.
/// </summary>
public interface Pure : ISingletonType { }

/// <summary>
/// Singleton that is created if not found and is not destroyed on load.
/// </summary>
public interface Creation : ISingletonType { }

/// <summary>
/// Not singleton, they are only referenced if they exist
/// </summary>
public interface Exisiting : ISingletonType { }

public abstract class Singleton<T, IType> : MonoBehaviour where T : MonoBehaviour where IType : ISingletonType
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                CheckCreation();
                CheckDontDestroyOnLoad();
            }

            return _instance;
        }
    }

    /// <summary>
    /// Make sure to call base.Awake() if overridden.
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            CheckDontDestroyOnLoad();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected static void CheckDontDestroyOnLoad()
    {
        if(_instance != null && !(typeof(IType) is Exisiting))
            DontDestroyOnLoad(_instance.gameObject);
    }

    private static void CheckCreation()
    {
        if (_instance == null && typeof(IType) is Creation)
        {
            _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
        }
    }
}
