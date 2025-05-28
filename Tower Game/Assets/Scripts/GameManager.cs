using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    

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
    Debug.Log("Score: " + score); // Optional: Keep for debug
    if (scoreText != null)
        scoreText.text = "Score: " + score;
}
    private bool gameOverTriggered = false;

    public void GameOver()
    {
        if (gameOverTriggered) return;
        gameOverTriggered = true;

        Debug.Log("Game Over! Final Score: " + score);
        AudioManager.Instance.PlayGameOver();
        BreakAllBlockJoints();
        Invoke(nameof(RestartScene), 2f);
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
        AudioManager.Instance.PlayBackgroundMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        score = 0;
        if (scoreText != null)
            scoreText.text = "Score: 0";
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


    public void RegisterBlockHitGround()
{
    GameOver();  // Immediately ends the game if a non-first block hits ground
}
}