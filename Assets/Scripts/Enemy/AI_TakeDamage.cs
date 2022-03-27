using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_TakeDamage : MonoBehaviour
{
    public float DamageValue = -25;
    private bool canDamage = false;
    private GameObject player;

    public float timer = 2;
    private float tempTimer;

    public void FixedUpdate()
    {
        if (canDamage)
        {
            if (tempTimer <= 0)
            {
                tempTimer = timer;
                player.GetComponent<PlayerHealth>().ChangeHealth(-DamageValue);

            }
            else
            {
                tempTimer -= Time.deltaTime;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canDamage = true;
            player = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canDamage = false;
        }
    }
}
