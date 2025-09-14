using UnityEngine;

public class Pulse : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float damage;
    private float lifetime = 5f;

    void Start()
    {
        // Destroy after lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move in direction
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Initialize(Vector3 dir, float spd, float dmg)
    {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Pulse hit: {other.name} with tag: {other.tag}");

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log($"Dealing {damage} damage to enemy");
                enemy.TakeDamage(damage);
            }

            // Destroy pulse on hit
            Destroy(gameObject);
        }
    }
}
