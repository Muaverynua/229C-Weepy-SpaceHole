using UnityEngine;

public class TrackObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int obstacleCount = 5;
    public float trackWidth = 4f;
    public float trackLength = 50f;

    public Transform floor; // 👈 Drag your "Track" (floor cube) here in the prefab

    public void SpawnObstacles()
    {
        // Clear old obstacles (children tagged as "Obstacle")
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                Destroy(child.gameObject);
            }
        }

        // Spawn new obstacles as children of this track
        for (int i = 0; i < obstacleCount; i++)
        {
            float randomX = Random.Range(-trackWidth / 2f, trackWidth / 2f);
            float randomZ = Random.Range(-trackLength / 2f, trackLength / 2f);
            Vector3 localPos = new Vector3(randomX, 0.5f, randomZ);

            GameObject obstacle = Instantiate(obstaclePrefab, transform);
            obstacle.transform.localPosition = localPos;
            obstacle.tag = "Obstacle";
        }
    }
}
