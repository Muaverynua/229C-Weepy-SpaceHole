using UnityEngine;

public class BallController : MonoBehaviour
{
    public float forwardSpeed = 10f;           // Starting speed
    public float turnSpeed = 5f;               // Left/right movement speed
    public float maxSpeed = 30f;               // Max forward speed
    public float speedIncreaseRate = 0.333f;   // How fast speed increases over time

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component on this GameObject
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get current forward speed (Z direction only)
        float currentForwardSpeed = Vector3.Dot(rb.linearVelocity, Vector3.forward);

        // Gradually increase speed over time
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += speedIncreaseRate * Time.deltaTime;
        }

        // Only apply forward force if not already at max forward speed
        if (currentForwardSpeed < maxSpeed)
        {
            rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.Acceleration);
        }

        // Player-controlled left/right movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        rb.AddForce(Vector3.right * moveHorizontal * turnSpeed, ForceMode.Acceleration);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Wall"))
        {
        FindFirstObjectByType<GameOverManager>()?.TriggerGameOver();
        gameObject.SetActive(false);
        }
    }


}
