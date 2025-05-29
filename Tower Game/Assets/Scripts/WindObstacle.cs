using UnityEngine;
using System.Collections;
using System.Linq;

public class WindObstacle : MonoBehaviour
{
    public float windStrength = 2f;
    public float windInterval = 4f;
    public float windDuration = 1f;

    private float timer;

    void Update()
    {
        if (!LevelManager.Instance.ActiveSettings.hasWindObstacle)
            return;

        timer += Time.deltaTime;

        if (timer >= windInterval)
        {
            timer = 0f;
            StartCoroutine(ApplyWind());
        }
    }

    IEnumerator ApplyWind()
    {
        float elapsed = 0f;
        while (elapsed < windDuration)
        {
            var blocks = GameObject.FindGameObjectsWithTag("Block")
                .Select(go => go.GetComponent<Rigidbody>())
                .Where(rb => rb != null);

            foreach (var rb in blocks)
            {
                rb.AddForce(Vector3.right * windStrength);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
