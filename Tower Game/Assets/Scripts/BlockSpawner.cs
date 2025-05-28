using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public float moveSpeed = 1.5f;
    public Transform ropeAnchorPoint;  // The top anchor point where the rope is attached
    public Material ropeMaterial;  // The rope texture material

    private GameObject currentBlock;
    private float lastSpawnHeight = 5f;
    private LineRenderer lineRenderer;
    private bool isFirstBlock = true;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false; // Hide rope until a block is spawned

        // Assign the material to the LineRenderer
        lineRenderer.material = ropeMaterial;

        // Adjust the width of the rope
        lineRenderer.startWidth = 0.1f; // Rope thickness at the start
        lineRenderer.endWidth = 0.1f;   // Rope thickness at the end

        SpawnBlock();
    }

    void Update()
    {
        if (currentBlock != null)
        {
            MoveBlock();

            // Update the rope each frame
            UpdateRope();

            if (Input.GetMouseButtonDown(0))
            {
                DropBlock();
            }
        }
    }

    void SpawnBlock()
    {
        Vector3 spawnPos = ropeAnchorPoint.position + new Vector3(0, -3f, 0);
        currentBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);

        // Disable physics initially
        Rigidbody rb = currentBlock.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        currentBlock.tag = "Untagged";

        lineRenderer.enabled = true;
    }

    float GetSpawnHeight()
    {
        return lastSpawnHeight;
    }

    void MoveBlock()
    {
        float swingAmplitude = 2f; // How far it swings side to side
        float swingHeight = 0.5f;  // How much the height changes during swing
        float swingSpeed = 2f;     // How fast it swings

        float time = Time.time * swingSpeed;

        float x = Mathf.Sin(time) * swingAmplitude;
        float y = GetSpawnHeight() - Mathf.Cos(time) * swingHeight;

        currentBlock.transform.position = new Vector3(x, y, 0);
    }

    void DropBlock()
    {
        // Stop manual movement
        Rigidbody rb = currentBlock.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        currentBlock.tag = "Block";

        lastSpawnHeight = currentBlock.transform.position.y + 1.2f;

        lineRenderer.enabled = false;

        if (isFirstBlock)
        {
            currentBlock.GetComponent<BlockController>().SetAsFirstBlock();
            isFirstBlock = false;
        }

        currentBlock = null;

        Invoke("SpawnBlock", 0.5f);
        AudioManager.Instance.PlayBlockDrop();
    }

    void UpdateRope()
    {
        if (currentBlock != null)
        {
            lineRenderer.SetPosition(0, ropeAnchorPoint.position);
            lineRenderer.SetPosition(1, currentBlock.transform.position + Vector3.up * 0.5f); // Top of the block
        }
    }
}
