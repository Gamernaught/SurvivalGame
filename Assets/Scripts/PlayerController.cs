using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private AudioSource playerAudio;
    public float horizontalInput;
    public float verticalInput;
    public float speed;
    public float xRange = 21.0f;
    public float yRange = 9.0f;
    public HealthBar healthBar;
    public Rigidbody2D bulletPrefab;
    public float attackSpeed = 0.5f;
    public float coolDown;
    public float projectileSpeed = 500;
    public AudioClip pistolFire;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AimAtMouse();
        ShootBullet();


    }
    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        // Move player
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += movement * speed * Time.deltaTime;

        // Clamp player's position within the defined ranges
        float clampedX = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float clampedY = Mathf.Clamp(transform.position.y, -yRange, yRange);
        transform.position = new Vector3(clampedX, clampedY, 0f);

        // Rotate player
        Vector3 rotation = new Vector3(0f, 0f, -horizontalInput * 10f);
        transform.Rotate(rotation);

    }
    void AimAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = mousePos - transform.position;
        aimDir.z = 0; // Set the z-component to zero to ensure 2D rotation
        aimDir = aimDir.normalized;

        // Use LookRotation to rotate the player towards the aim direction
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            healthBar.Damage(0.002f);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthBar.Damage(0.002f);
        }
    }
    private void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > coolDown)
        {
            // Calculate direction from player towards mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - transform.position).normalized;

            // Offset the bullet's starting position
            Vector3 offset = direction * 0.5f; // Change the offset value as needed
            Vector3 startPos = transform.position + offset;

            // Launch projectile from player in the calculated direction
            Rigidbody2D bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity);
            bullet.velocity = direction * projectileSpeed;


            // Rotate the bullet to face its direction of travel
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Update cooldown
            coolDown = Time.time + attackSpeed;

            // Audio sound for the guns
            playerAudio.PlayOneShot(pistolFire, 0.5f);
        }
    }


    public void Heal(float amount)
    {
        healthBar.IncreaseHealth(amount);
    }

}
