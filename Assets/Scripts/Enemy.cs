using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int speed = 4;
    private float currentHealth;
    private Rigidbody2D enemy;
    private GameObject player;
    public GameManager gameManager;
    public int pointValue;
    public float maxHealth = 1;
    public ParticleSystem explosionParticle;
    public GameObject heartPowerUpPrefab;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        gameManager = GameObject.FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                // Disable the Rigidbody2D component to prevent knockback
            }
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Drop a heart power-up
        Instantiate(heartPowerUpPrefab, transform.position, Quaternion.identity);

        gameManager.UpdateScore(pointValue);

        // Instantiate the explosion particle system
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        // Disable the enemy game object
        gameObject.SetActive(false);

        // Delay destroying the enemy game object to allow the particle system to finish
        Destroy(gameObject, explosionParticle.main.duration);
    }

    IEnumerator EnableRigidbody2D(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.simulated = true;
    }
}
