using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;
    private Image barImage;
    private GameManager gameManager;
    public float initialHealth = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bar = GetComponent<RectTransform>();
        barImage = GetComponent<Image>();

        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        SetSize(Health.totalHealth);
    }

    //Updates player health
    public void Damage(float damage)
    {
        if ((Health.totalHealth -= damage) >= 0f)
        {
            Health.totalHealth -= damage;
        }
        else
        {
            Health.totalHealth = 0f;
            gameManager.GameOver();
        }

        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }

        
        SetSize(Health.totalHealth);
    }

    //Updates size of health bar
    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
    public void Reset()
    {
        Health.totalHealth = initialHealth;
        SetSize(initialHealth);
        barImage.color = Color.green;
    }
    public void IncreaseHealth(float amount) 
    {
        if ((Health.totalHealth + amount) <= 1f)
        {
            Health.totalHealth += amount;
        }
        else
        {
            Health.totalHealth = 1f;
        }

        if (Health.totalHealth >= 0.3f)
        {
            barImage.color = Color.green;
        }

        SetSize(Health.totalHealth);
    }
}
