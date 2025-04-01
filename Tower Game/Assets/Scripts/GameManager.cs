using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Score: " + score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    void Update()
    {
        if (GetHighestBlockHeight() > 20) // Example height limit
        {
            GameOver();
        }
    }

    float GetHighestBlockHeight()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        float highest = 0;
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y > highest)
                highest = block.transform.position.y;
        }
        return highest;
    }

    
}