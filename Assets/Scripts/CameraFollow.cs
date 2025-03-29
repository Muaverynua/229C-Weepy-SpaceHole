using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The object to follow (your Ball)
    public Vector3 offset = new Vector3(0, 5f, -10f);  // Position offset from the ball
    public float smoothSpeed = 5f;      // Camera follow smoothness

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
        //transform.LookAt(target);
    }
}
