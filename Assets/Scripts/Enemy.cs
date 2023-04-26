using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    int speed = 4;
    private Rigidbody2D enemy;
    private GameObject player;
    




    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
