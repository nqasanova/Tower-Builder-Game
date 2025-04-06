using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
{
    Debug.Log("Game Over! Final Score: " + score);

    // Break all joints to let the blocks fall naturally
    BreakAllBlockJoints();

    // Restart the game after a short delay
    Invoke(nameof(RestartScene), 2f); // optional delay to let the fall play out
}

    void Update()
    {
        if (GetHighestBlockHeight() > 20f)
        {
            GameOver();
        }
    }

    float GetHighestBlockHeight()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Stuck");
        float highest = 0f;

        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y > highest)
                highest = block.transform.position.y;
        }

        return highest;
    }

    

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void BreakAllBlockJoints()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Stuck");
        foreach (GameObject block in blocks)
        {
            FixedJoint joint = block.GetComponent<FixedJoint>();
            if (joint != null)
            {
                Destroy(joint);
            }
        }
    }
}