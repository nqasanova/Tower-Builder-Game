using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameDifficulty currentDifficulty = GameDifficulty.Easy;

    public LevelSettings easySettings;
    public LevelSettings mediumSettings;
    public LevelSettings hardSettings;

    public LevelSettings ActiveSettings { get; private set; }

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

    void Start()
    {
        SetDifficulty(currentDifficulty); // Default on startup
    }

    public void SetDifficulty(GameDifficulty difficulty)
    {
        currentDifficulty = difficulty;

        switch (difficulty)
        {
            case GameDifficulty.Easy:
                ActiveSettings = easySettings;
                break;
            case GameDifficulty.Medium:
                ActiveSettings = mediumSettings;
                break;
            case GameDifficulty.Hard:
                ActiveSettings = hardSettings;
                break;
        }

        Debug.Log("Difficulty set to: " + difficulty);
    }
}