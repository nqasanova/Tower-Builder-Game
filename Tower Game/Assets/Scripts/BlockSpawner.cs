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
            if (Input.GetMouseButtonDown(0)) 
            {
                DropBlock();
            }
        }
    }

    void SpawnBlock()
    {
        Vector3 spawnPos = new Vector3(Mathf.Round(Random.Range(-3f, 3f)), 10, 0);
        currentBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
        currentBlock.GetComponent<Rigidbody>().isKinematic = true;
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
    Rigidbody rb = currentBlock.GetComponent<Rigidbody>();
    rb.isKinematic = false;

    //  Freeze all rotation so the block drops straight
    rb.constraints = RigidbodyConstraints.FreezeRotation;

    currentBlock = null;
    Invoke("SpawnBlock", 0.5f);
}
}