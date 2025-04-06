using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public float moveSpeed = 1.5f;

    private GameObject currentBlock;
    private bool movingRight = true;
    private float lastSpawnHeight = 5f;  // The starting height for the first block.

    void Start()
    {
        SpawnBlock();  // Spawn the first block.
    }

    void Update()
    {
        if (currentBlock != null)
        {
            MoveBlock();  // Move the current block back and forth.

            if (Input.GetMouseButtonDown(0))  // If the player clicks, drop the block.
            {
                DropBlock();
            }
        }
    }

    void SpawnBlock()
    {
        // Get the spawn position, dynamically adjusting the height of the spawn point.
        Vector3 spawnPos = new Vector3(Mathf.Round(Random.Range(-3f, 3f)), GetSpawnHeight(), 0);
        currentBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
        currentBlock.GetComponent<Rigidbody>().isKinematic = true;  // Prevent physics from affecting it until dropped.
        currentBlock.tag = "Untagged";  // Tag the block as "Untagged" initially so it isn't counted in the height calculation.
    }

    // This method calculates the height for spawning new blocks
    float GetSpawnHeight()
    {
        // The spawn height is the highest Y value among the already spawned blocks
        return lastSpawnHeight;
    }

    void MoveBlock()
    {
        // Move the current block back and forth between the left and right
        float direction = movingRight ? 1f : -1f;
        currentBlock.transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // Change direction when the block reaches the boundaries
        if (currentBlock.transform.position.x > 3f)
            movingRight = false;
        else if (currentBlock.transform.position.x < -3f)
            movingRight = true;
    }

    void DropBlock()
    {
        // Allow the block to interact with physics after it is dropped
        currentBlock.GetComponent<Rigidbody>().isKinematic = false;
        currentBlock.tag = "Block";  // Tag the block as "Block" so it will be part of the stack.

        // Update the spawn height based on the current block's position
        lastSpawnHeight = currentBlock.transform.position.y + 1.2f;

        // Clear the reference to the current block so a new one can be spawned
        currentBlock = null;

        // Spawn a new block after a short delay
        Invoke("SpawnBlock", 0.5f);
    }
}