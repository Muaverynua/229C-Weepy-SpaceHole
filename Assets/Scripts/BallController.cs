using UnityEngine;

public class BallController : MonoBehaviour
{
    public float acceleration = 5f;        // Custom acceleration (a)
    public float maxSpeed = 30f;           // Max velocity
    public float turnSpeed = 5f;           // Left/right force multiplier
    public float speedIncreaseRate = 0.05f;  // How fast speed increases over time

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component on this GameObject
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Gradually increase acceleration
        if (acceleration < maxSpeed)
        {
            acceleration += speedIncreaseRate * Time.fixedDeltaTime;
        }

        // ✅ Calculate force manually: F = m * a
        float mass = rb.mass;
        float forwardForce = mass * acceleration;

        Vector3 forceDirection = Vector3.forward;
        rb.AddForce(forceDirection * forwardForce);

        // Side movement
        float input = Input.GetAxis("Horizontal");
        rb.AddForce(Vector3.right * input * turnSpeed, ForceMode.Acceleration);
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
