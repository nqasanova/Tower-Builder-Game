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
        Debug.Log("Score: " + score);

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        int targetScore = LevelManager.Instance.ActiveSettings.scoreToWin;
        if (score >= targetScore)
        {
            GameWin(); // Trigger win condition
        }
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

            Rigidbody rb = block.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // Freeze physics
            }
        }
    }
    
    private bool gameWon = false;

    public void GameWin()
    {
        if (gameOverTriggered || gameWon) return;

        gameWon = true;
        Debug.Log("🎉 You Win! Final Score: " + score);
        AudioManager.Instance.PlayWin(); // Optional: you may want a separate win sound
        BreakAllBlockJoints(); // Freeze blocks like in GameOver
        // Optional: Display Win UI
        Invoke(nameof(RestartScene), 2f);
    }


    public void RegisterBlockHitGround()
    {
        GameOver();  // Immediately ends the game if a non-first block hits ground
    }
}