using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    private int kills;
    public bool isGameActive;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button quitButton;
    public HealthBar healthBar;
    public Enemy enemy;
    public GameObject titleScreen;
    public GameObject player;
    public GameObject powerUpPrefab;

    public float enemyHealth = 1f;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        UpdateScore(0);
        isGameActive = true; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageEnemy(float damageAmount)
    {
        enemyHealth -= damageAmount;
        if (enemyHealth <= 0f)
        {
            // Enemy is defeated
            // Add points to kills and update kills text
            UpdateScore(enemy.pointValue);

            SpawnPowerup();
            Debug.Log("Powerup spawned!");

            // Destroy the enemy game object
            Destroy(enemy.gameObject);
        }
    }

    // Updates the score of the game
    public void UpdateScore(int scoreToAdd)
    {
        kills += scoreToAdd;
        scoreText.text = "Kills: " + kills;
    }
    // Ends the Game
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    // Restarts the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        healthBar.Reset();
    }
    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
    // Starts the game
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        enemy.maxHealth = difficulty;
        StartCoroutine(SpawnWaves(10, 5, 5f, 2f));
        kills = 0;
        UpdateScore(0);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
    // Spawns the power up
    public void SpawnPowerup()
    {
        Debug.Log("Calculating spawn position...");
        // Choose a random position within a range around the player
        Vector3 spawnPosition = player.transform.position + new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), 0f);
        Debug.Log("Spawn position: " + spawnPosition);

        // Instantiate the powerup at the random position with no rotation
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
    // Waves of enemies that spawn
    IEnumerator SpawnWaves(int waveCount, int enemiesPerWave, float timeBetweenWaves, float timeBetweenEnemies)
    {
        for (int wave = 0; wave < waveCount; wave++)
        {
            for (int enemyIndex = 0; enemyIndex < enemiesPerWave; enemyIndex++)
            {
                float randomX = UnityEngine.Random.Range(-18f, 18f); // Generate a random x coordinate within range
                float randomY = UnityEngine.Random.Range(-8f, 8f); // Generate a random y coordinate within range
                Vector3 spawnPosition = new Vector3(randomX, randomY, 0f); // Create a new vector with the random coordinates
                Instantiate(enemy, spawnPosition, Quaternion.identity); // Instantiate the enemy with the random position
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
