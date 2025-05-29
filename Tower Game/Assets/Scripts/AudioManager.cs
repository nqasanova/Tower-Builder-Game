using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource backgroundSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip blockDropSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayWin()
    {
        if (backgroundSource.isPlaying)
        {
            backgroundSource.Stop();
        }

        sfxSource.PlayOneShot(winSound);
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundSource.clip = backgroundMusic;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }
    }

    public void PlayBlockDrop()
    {
        sfxSource.PlayOneShot(blockDropSound);
    }

    public void PlayGameOver()
    {
        // Stop the background music before playing game over sound
        if (backgroundSource.isPlaying)
        {
            backgroundSource.Stop();
        }

        sfxSource.PlayOneShot(gameOverSound);
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && !backgroundSource.isPlaying)
        {
            backgroundSource.clip = backgroundMusic;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }
    }
}