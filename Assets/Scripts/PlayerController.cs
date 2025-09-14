using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float health = 100f;
    public float maxHealth = 100f;
    public float idleDefenseBuff = 0.5f; // 50% damage reduction in idle mode

    [Header("Pulse Settings")]
    public GameObject pulsePrefab;
    public float activePulseCooldown = 0.3f;
    public float idlePulseCooldown = 1f;
    public float pulseSpeed = 10f;
    public float pulseDamage = 25f;

    private bool isActiveMode = true;
    private float lastPulseTime;
    private Camera mainCamera;

    public bool IsActiveMode => isActiveMode;

    void Start()
    {
        mainCamera = Camera.main;
        // Create pulse prefab if it doesn't exist
        if (pulsePrefab == null)
        {
            CreatePulsePrefab();
        }
    }

    void Update()
    {
        // Handle mode switching with right click
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            ToggleMode();
        }

        // Handle pulse firing
        if (isActiveMode)
        {
            // Manual pulse firing with left click
            if (Input.GetMouseButtonDown(0) && Time.time - lastPulseTime > activePulseCooldown)
            {
                FirePulse();
            }
        }
        else
        {
            // Automatic pulse firing in idle mode
            if (Time.time - lastPulseTime > idlePulseCooldown)
            {
                FirePulseAtClosestEnemy();
            }
        }

        // Update UI
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateUI();
        }
    }

    void ToggleMode()
    {
        isActiveMode = !isActiveMode;
        Debug.Log("Switched to " + (isActiveMode ? "Active" : "Idle") + " mode");
    }

    void FirePulse()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = (mousePos - transform.position).normalized;

        CreatePulse(direction);
    }

    void FirePulseAtClosestEnemy()
    {
        Enemy closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
            CreatePulse(direction);
        }
    }

    Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length == 0) return null;

        Enemy closest = enemies[0];
        float closestDistance = Vector3.Distance(transform.position, closest.transform.position);

        for (int i = 1; i < enemies.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distance < closestDistance)
            {
                closest = enemies[i];
                closestDistance = distance;
            }
        }

        return closest;
    }

    void CreatePulse(Vector3 direction)
    {
        GameObject pulse = Instantiate(pulsePrefab, transform.position, Quaternion.identity);
        Pulse pulseScript = pulse.GetComponent<Pulse>();
        pulseScript.Initialize(direction, pulseSpeed, pulseDamage);

        lastPulseTime = Time.time;
    }

    void CreatePulsePrefab()
    {
        // Create a simple pulse prefab
        GameObject pulseObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pulseObj.name = "Pulse";
        pulseObj.transform.localScale = Vector3.one * 0.3f;

        // Add pulse component
        Pulse pulse = pulseObj.AddComponent<Pulse>();

        // Set color
        Renderer renderer = pulseObj.GetComponent<Renderer>();
        renderer.material.color = Color.yellow;

        // Remove collider (we'll handle collision in the Pulse script)
        Destroy(pulseObj.GetComponent<Collider>());

        pulsePrefab = pulseObj;
    }

    public void TakeDamage(float damage)
    {
        if (!isActiveMode)
        {
            damage *= (1f - idleDefenseBuff); // Apply defense buff in idle mode
        }

        health -= damage;
        Debug.Log($"Player took {damage} damage. Health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add game over logic here
        Time.timeScale = 0f;
    }
}
