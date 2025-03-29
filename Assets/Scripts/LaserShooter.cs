using UnityEngine;
using UnityEngine.UI;

public class LaserShooter : MonoBehaviour
{
    [Header("Laser Settings")]
    public float laserRange = 100f;
    public float maxEnergy = 5f;
    public float energyDrainRate = 1f;
    public float energyRechargeRate = 1.5f;
    public KeyCode fireKey = KeyCode.Space;

    [Header("References")]
    public LineRenderer laserLine;      // Drag your LineRenderer here
    public Slider energyBar;            // Drag your UI Slider here
    private AudioSource audioSource;

    [Header("Sound Effects")]
    public AudioClip overheatSound;   
    public AudioClip laserFireSound;  

    private float currentEnergy;
    private bool isCoolingDown = false;

    void Start()
    {
        currentEnergy = maxEnergy;

        // Get AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
        

        // Disable laser at start
        if (laserLine != null)
        {
            laserLine.enabled = false;
        }
            
    }

    void Update()
    {
        bool holdingFire = Input.GetKey(fireKey);

        // Firing laser
        if (!isCoolingDown && holdingFire && currentEnergy > 0f)
        {
            FireLaser();
            currentEnergy -= energyDrainRate * Time.deltaTime;

            if (currentEnergy <= 0f)
            {
                currentEnergy = 0f;
                EnterCooldown();
            }
        }
        else
        {
            StopLaser();
            currentEnergy += energyRechargeRate * Time.deltaTime;

            if (isCoolingDown && currentEnergy >= maxEnergy)
            {
                currentEnergy = maxEnergy;
                ExitCooldown();
            }
        }

        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);

        // Update UI energy bar
        if (energyBar != null)
        {
            energyBar.value = currentEnergy / maxEnergy;
        }
    }

    void FireLaser()
    {
        if (laserFireSound != null && audioSource != null && !audioSource.isPlaying && !isCoolingDown)
        {
            audioSource.clip = laserFireSound;
            audioSource.loop = true;
            audioSource.Play();
        }



        if (laserLine == null) return;

        Vector3 start = transform.position;
        Vector3 direction = Vector3.forward;
        Ray ray = new Ray(start, direction);
        RaycastHit hit;

        Vector3 end = start + direction * laserRange;

        if (Physics.Raycast(ray, out hit, laserRange))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Destroy(hit.collider.gameObject);
                ScoreManager.Instance?.AddScore(1);
            }

            end = hit.point;
        }

        laserLine.enabled = true;
        laserLine.SetPosition(0, start);
        laserLine.SetPosition(1, end);
    }

    void StopLaser()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (laserLine != null && laserLine.enabled)
        {
            laserLine.enabled = false;
        }
    }

    void EnterCooldown()
    {
        isCoolingDown = true;
        Debug.Log("Laser Overheated!");

        if (audioSource != null)
        {
            audioSource.Stop(); // Stop the laser loop sound

            if (overheatSound != null)
            {
                audioSource.PlayOneShot(overheatSound);
            }
        }
    }

    void ExitCooldown()
    {
        isCoolingDown = false;
        Debug.Log("Laser Cooled Down");
    }
}
