using UnityEngine;

public class BlockLandingHandler : MonoBehaviour
{
    private bool hasLanded = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasLanded) return;

        // Only stick if touching ground or a stuck block
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Stuck"))
        {
            hasLanded = true;

            // Freeze the block in place
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;

            // This block is now part of the tower
            gameObject.tag = "Stuck";

            GameManager.Instance.AddScore(10);

            Debug.Log("✅ Block has landed and stuck.");
        }
    }

    void Update()
    {
        if (!hasLanded && transform.position.y < -5f)
        {
            hasLanded = true;
            GameManager.Instance.GameOver();
        }
    }
}
