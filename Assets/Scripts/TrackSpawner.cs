using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public GameObject trackPrefab;      // Track prefab
    public GameObject obstaclePrefab;   // Red cube obstacle prefab
    public Transform player;            // Ball Transform

    public float trackLength = 20f;     // Length of each track
    public int initialTracks = 5;       // How many tracks to start with
    public float spawnDistance = 40f;   // Distance ahead of player before spawning new track

    private float spawnZ = 0f;
    private List<GameObject> activeTracks = new List<GameObject>();

    void Start()
    {
        // Align first segment with player
        spawnZ = Mathf.Floor(player.position.z / trackLength) * trackLength;

        for (int i = 0; i < initialTracks; i++)
        {
            SpawnTrack();
        }
    }

    void Update()
    {
        if (player.position.z + spawnDistance > spawnZ)
        {
            SpawnTrack();
            DeleteOldTrack();
        }
    }

    void SpawnTrack()
    {
        Vector3 spawnPosition = new Vector3(0, 0, spawnZ);
        GameObject newTrack = Instantiate(trackPrefab, spawnPosition, Quaternion.identity);
        activeTracks.Add(newTrack);

        // Spawn a random obstacle on this track
        TrySpawnObstacle(spawnZ);

        spawnZ += trackLength; 



    }

    void TrySpawnObstacle(float zPos)
    {
        if (Random.value < 0.6f) // 60% chance to spawn obstacle
        {
            float randomX = Random.Range(-2f, 2f); // within track width
            float obstacleY = 0.5f; // should sit on the track

            Vector3 obstaclePos = new Vector3(randomX, obstacleY, zPos + Random.Range(-8f, 8f));
            Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);
        }
    }

    void DeleteOldTrack()
    {
        if (activeTracks.Count > initialTracks)
        {
            Destroy(activeTracks[0]);
            activeTracks.RemoveAt(0);
        }
    }
}
