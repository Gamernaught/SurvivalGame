using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 1f;
    public float destroyDelay = 0.1f;
    public float bulletForce = 100f; // New public variable for the force of the bullet

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the bullet colliedes with the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Finds the enemy
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                //enemy takes damage
                enemy.TakeDamage(damage);

                // Apply a force to the hit enemy
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                if (enemyRigidbody != null)
                {
                    Vector2 bulletDirection = transform.right; // assuming bullet moves to the right
                    enemyRigidbody.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
                }
            }
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
