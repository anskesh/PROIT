using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AI_TakeDamage : MonoBehaviour
{
    public int DamageValue = 25;
    private bool canDamage = false;
    private PlayerHealth player;

    public float timer = 2;
    private float tempTimer;

    public void FixedUpdate()
    {
        if (canDamage)
        {
            if (tempTimer <= 0)
            {
                tempTimer = timer;
                player.ApplyDamage(DamageValue);
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
            player = collision.gameObject.GetComponent<PlayerHealth>();
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
