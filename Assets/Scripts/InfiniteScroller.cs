using UnityEngine;

public class InfiniteScroller : MonoBehaviour
{
    public Transform player;                // Assign your Ball here
    public GameObject trackPrefab;          // Assign your Track prefab (with walls + floor inside)
    public GameObject obstaclePrefab;       // Assign your Obstacle prefab (red cube)
    public float trackLength = 50f;         // Should match your prefab's Z scale

    private GameObject track1;
    private GameObject track2;

    void Start()
    {
        // Instantiate the first two tracks
        track1 = Instantiate(trackPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        track2 = Instantiate(trackPrefab, new Vector3(0, 0, trackLength), Quaternion.identity);

        // Spawn initial obstacles
        InitObstacles(track1);
        InitObstacles(track2);
    }

    void Update()
    {
        // If the player passes track1, move it ahead
        if (player.position.z > track1.transform.position.z + trackLength)
        {
            MoveTrack(track1, track2);
        }

        // If the player passes track2, move it ahead
        if (player.position.z > track2.transform.position.z + trackLength)
        {
            MoveTrack(track2, track1);
        }
    }

    void MoveTrack(GameObject movingTrack, GameObject otherTrack)
    {
        // Move this track ahead of the other one
        movingTrack.transform.position = new Vector3(0, 0, otherTrack.transform.position.z + trackLength);

        // Spawn new obstacles for the moved track
        InitObstacles(movingTrack);
    }

    void InitObstacles(GameObject track)
    {
        // Look for the TrackObstacleSpawner component on the track
        TrackObstacleSpawner spawner = track.GetComponent<TrackObstacleSpawner>();
        if (spawner != null)
        {
            spawner.obstaclePrefab = obstaclePrefab; 
            spawner.SpawnObstacles();               
        }
    
    }
}
