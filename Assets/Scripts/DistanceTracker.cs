using UnityEngine;
using TMPro;
public class DistanceTracker : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI finalScoreText;


    private float startZ;
    private float finalDistance = 0f;
    void Start()
    {
        if (player != null)
            startZ = player.position.z;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Mathf.Floor(player.position.z - startZ);
            distance = Mathf.Max(0, distance);
            distanceText.text = distance + " m";
        }
    }

    public void ShowFinalScore()
{
    if (player != null && finalScoreText != null)
    {
        float distance = Mathf.Floor(player.position.z - startZ);
        distance = Mathf.Max(0, distance);

        finalScoreText.text = "Distance: " + distance + " m";
    }
}

}
