using UnityEngine;

// This is a generic Singleton class for Unity.
// Basically makes sure only ONE of this object exists at a time.
// T is just whatever type/class you plug in (like GameManager, AudioManager, etc.)
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Static means it's shared across everything (kinda like global),
    // so you can do Something.Instance to access it from anywhere.
    public static T Instance { get; private set; }

    // This runs when the object is first created in the scene
    protected virtual void Awake()
    {
        // If an instance already exists, we don’t need another one, so just delete this new one
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // If no instance yet, set this one as the main one
        Instance = this as T;
    }

    // When you quit the game (like in editor or build), clear the reference and destroy it
    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

// This is the same Singleton class but keeps the object between scenes
// So like if you’re loading a new level and you still want your GameManager to stay alive
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    // We override the original Awake, but still call it first using base.Awake()
    // Then we just say “don’t destroy this thing when loading a new scene”
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject); // magic line that keeps this object alive forever (unless we destroy it)
    }
}
