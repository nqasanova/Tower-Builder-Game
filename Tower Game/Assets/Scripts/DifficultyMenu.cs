using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void SetEasy()
    {
        LevelManager.Instance.SetDifficulty(GameDifficulty.Easy);
        LoadGame();
    }

    public void SetMedium()
    {
        LevelManager.Instance.SetDifficulty(GameDifficulty.Medium);
        LoadGame();
    }

    public void SetHard()
    {
        LevelManager.Instance.SetDifficulty(GameDifficulty.Hard);
        LoadGame();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace with your actual game scene name
    }
}