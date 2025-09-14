using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float enemySpawnRate = 2f;
    public float enemySpawnDistance = 15f;
    public int maxEnemies = 20;

    [Header("UI References")]
    public Text modeText;
    public Text soulsText;
    public Image modeIndicator;

    [Header("Colors")]
    public Color idleColor = new Color(0.5f, 0.8f, 1f, 0.3f); // Light blue
    public Color activeColor = new Color(1f, 0.5f, 0.5f, 0.3f); // Light red

    public static GameManager Instance { get; private set; }

    private int souls = 0;
    private float lastEnemySpawn;
    private Camera mainCamera;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        UpdateUI();
    }

    void Update()
    {
        // Spawn enemies
        if (Time.time - lastEnemySpawn > enemySpawnRate)
        {
            SpawnEnemy();
            lastEnemySpawn = Time.time;
        }
    }

    void SpawnEnemy()
    {
        if (FindObjectsOfType<Enemy>().Length >= maxEnemies) return;

        // Spawn outside camera view
        Vector2 spawnPos = GetRandomSpawnPosition();

        GameObject enemyObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        enemyObj.name = "Enemy";
        enemyObj.transform.position = new Vector3(spawnPos.x, spawnPos.y, 0);
        enemyObj.transform.localScale = Vector3.one * 0.8f;

        // Add enemy component
        Enemy enemy = enemyObj.AddComponent<Enemy>();

        // Set color
        Renderer renderer = enemyObj.GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPos;
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Randomly choose which edge to spawn from
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Top
                spawnPos = new Vector2(Random.Range(-cameraWidth/2, cameraWidth/2), cameraHeight/2 + enemySpawnDistance);
                break;
            case 1: // Bottom
                spawnPos = new Vector2(Random.Range(-cameraWidth/2, cameraWidth/2), -cameraHeight/2 - enemySpawnDistance);
                break;
            case 2: // Left
                spawnPos = new Vector2(-cameraWidth/2 - enemySpawnDistance, Random.Range(-cameraHeight/2, cameraHeight/2));
                break;
            default: // Right
                spawnPos = new Vector2(cameraWidth/2 + enemySpawnDistance, Random.Range(-cameraHeight/2, cameraHeight/2));
                break;
        }

        return spawnPos;
    }

    public void AddSouls(int amount)
    {
        souls += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (modeText != null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                modeText.text = player.IsActiveMode ? "ACTIVE MODE" : "IDLE MODE";
            }
        }

        if (soulsText != null)
        {
            soulsText.text = "Souls: " + souls;
        }

        if (modeIndicator != null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                modeIndicator.color = player.IsActiveMode ? activeColor : idleColor;
            }
        }
    }
}
