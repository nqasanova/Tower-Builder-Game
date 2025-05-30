using UnityEngine;

public class MovingGround : MonoBehaviour
{
    [Header("DEBUG/Read-Only - Controlled by LevelManager")]
    [SerializeField] private float moveRange;
    [SerializeField] private float moveSpeed;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        // Don't fetch LevelManager here (unsafe order)
    }

    void Update()
    {
        if (LevelManager.Instance == null)
            return;

        var settings = LevelManager.Instance.ActiveSettings;

        if (!settings.hasMovingGround)
            return;

        moveRange = settings.groundMoveRange;
        moveSpeed = settings.groundMoveSpeed;

        float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }
}
