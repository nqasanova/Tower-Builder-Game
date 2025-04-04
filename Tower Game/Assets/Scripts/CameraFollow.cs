using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float offsetY = 5f;
    public float smoothSpeed = 2f;
    public string blockTag = "Stuck";

    void LateUpdate()
    {
        float highestY = FindHighestBlockY();

        if (highestY > transform.position.y - offsetY)
        {
            float targetY = highestY + offsetY;
            Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    float FindHighestBlockY()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(blockTag);
        float highestY = 0f;

        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y > highestY)
                highestY = block.transform.position.y;
        }

        return highestY;
    }
}
