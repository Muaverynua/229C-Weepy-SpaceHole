using UnityEngine;

public class TrackObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // 👈 Drag multiple prefabs here
    public int obstacleCount = 30;
    public float trackWidth = 40f;
    public float trackLength = 500f;

    public void SpawnObstacles()
    {
        // Remove old obstacles
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < obstacleCount; i++)
        {
            float randomX = Random.Range(-trackWidth / 2f, trackWidth / 2f);
            float randomZ = Random.Range(-trackLength / 2f, trackLength / 2f);
            Vector3 localPos = new Vector3(randomX, 0.5f, randomZ);

            // 👇 Pick a random obstacle prefab
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            GameObject obstacle = Instantiate(prefab, transform);
            obstacle.transform.localPosition = localPos;
            obstacle.tag = "Obstacle";
        }
    }
}
