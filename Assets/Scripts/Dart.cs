﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public GameObject particlePrefab;
    public float speed = 5;
    public bool followPlayer;


    private Vector3 playerPos;

    private void Start()
    {
        if (followPlayer)
        {
            playerPos = FindObjectOfType<Player>().transform.position;

            Vector2 lookDir = playerPos - new Vector3(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.localEulerAngles = new Vector3(0, 0, angle);

            Vector2 moveDirection = (playerPos - transform.position).normalized * speed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -20);
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().TakeHit(collision);
        }
        Destroy(gameObject);
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
}
