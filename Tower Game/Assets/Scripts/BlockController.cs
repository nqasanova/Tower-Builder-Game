using UnityEngine;

public class BlockController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;

    private bool hasLanded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && collision.gameObject.CompareTag("Ground"))
        {
            hasLanded = true;
            GameManager.Instance.AddScore(10); 
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        rb.isKinematic = true; 
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
    }

    void Update()
    {
        if (transform.position.y < -5f) 
        {
            if (GameManager.Instance != null)
{
    GameManager.Instance.GameOver();
}
else
{
    Debug.LogWarning("GameManager.Instance is null!");
}

        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
