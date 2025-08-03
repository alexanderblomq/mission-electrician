using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;
    public static MusicManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    // Returns the state of music after toggle
    public bool ToggleMusic()
    {
        if (musicSource != null)
        {
            switch (musicSource.isPlaying)
            {
                case true:
                    musicSource.Stop(); return false;
                case false:
                    musicSource.Play(); return true;
            }

        }
        return false;
    }
}
