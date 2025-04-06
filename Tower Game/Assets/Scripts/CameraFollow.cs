using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;   // Slower follow speed for smooth following
    public float yOffset = 5f;       // Distance between the camera and the highest block
    private float highestY = 0f;     // The highest Y position of the blocks

    void Start()
    {
        // Set the starting position of the camera based on the initial offset
        float startingY = 5f + yOffset;
        transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
    }

    void Update()
    {
        float currentHighest = GetHighestBlockY();

        // Only update if the block is significantly higher than the previous highest
        if (currentHighest > highestY)
        {
            highestY = currentHighest;
        }

        // Target camera Y position is always above the highest block
        float targetY = highestY + yOffset;

        // Smoothly move the camera towards the target Y position
        float smoothedY = Mathf.Lerp(transform.position.y, targetY, followSpeed * Time.deltaTime);

        // Update the camera's position with a smooth transition
        transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);
    }

    float GetHighestBlockY()
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
}