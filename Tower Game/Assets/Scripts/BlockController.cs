using UnityEngine;

public class BlockController : MonoBehaviour
{
    private bool hasLanded = false;
    private bool isFirstBlock = false;

    void Start()
    {
        // Determine if this is the first block based on Y position
        if (transform.position.y <= 5.5f) // Rough threshold to detect the first block
        {
            isFirstBlock = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Ground")))
        {
            hasLanded = true;

            // Game Over if this block hits the ground and it's not the first block
            if (collision.gameObject.CompareTag("Ground") && !isFirstBlock)
            {
                GameManager.Instance.GameOver();
                return;
            }

            // Only add score if it lands on another block (not the ground)
            if (collision.gameObject.CompareTag("Block"))
            {
                GameManager.Instance.AddScore(10);
            }

            // Stick this block to the one it collided with
            Rigidbody otherRb = collision.rigidbody;
            if (otherRb != null)
            {
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = otherRb;
            }

            // Change tag to "Stuck" so it's part of the tower now
            gameObject.tag = "Stuck";
        }
    }

    void Update()
    {
        if (transform.position.y < -5f)
        {
            GameManager.Instance.GameOver();
        }
    }
}
