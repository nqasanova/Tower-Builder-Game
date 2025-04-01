using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public float moveSpeed = 1.5f;
    private GameObject currentBlock;
    private bool movingRight = true;

    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        if (currentBlock != null)
        {
            MoveBlock();
            if (Input.GetMouseButtonDown(0)) // Tap to drop
            {
                DropBlock();
            }
        }
    }

    void SpawnBlock()
    {
        Vector3 spawnPos = new Vector3(Mathf.Round(Random.Range(-3f, 3f)), 10, 0);
        currentBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
        currentBlock.GetComponent<Rigidbody>().isKinematic = true; // Temporarily disable physics
    }

    void MoveBlock()
    {
        float direction = movingRight ? 1 : -1;
        currentBlock.transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        if (currentBlock.transform.position.x > 3f)
            movingRight = false;
        else if (currentBlock.transform.position.x < -3f)
            movingRight = true;
    }

    void DropBlock()
    {
        // Get the world position of the click
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(currentBlock.transform.position).z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Move the block to the clicked position
        currentBlock.transform.position = new Vector3(worldPos.x, currentBlock.transform.position.y, currentBlock.transform.position.z);

        // Enable physics
        currentBlock.GetComponent<Rigidbody>().isKinematic = false;
        
        // Allow a new block to be spawned
        currentBlock = null; 
        Invoke("SpawnBlock", 0.5f);
    }
}