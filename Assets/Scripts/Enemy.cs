using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health = 50f;
    public float maxHealth = 50f;
    public float moveSpeed = 2f;
    public float damage = 10f;
    public int soulReward = 1;

    private PlayerController player;
    private Rigidbody rb;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move toward player
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage player
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
            }

            // Destroy enemy
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        // Visual feedback - flash red
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Die();
        }
    }

    System.Collections.IEnumerator FlashRed()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color originalColor = renderer.material.color;
        renderer.material.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        renderer.material.color = originalColor;
    }

    void Die()
    {
        // Award souls
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddSouls(soulReward);
        }

        // Destroy enemy
        Destroy(gameObject);
    }
}
