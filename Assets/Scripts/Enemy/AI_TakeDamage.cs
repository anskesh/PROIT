using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AI_TakeDamage : MonoBehaviour
{
    public int DamageValue = 25;
    private bool canDamage = false;
    private PlayerHealth player;
    private Animator _anim;

    public float timer = 2;
    private float tempTimer;

    private void Start()
    {
        _anim = transform.GetComponentInParent<Animator>();
    }

    public void FixedUpdate()
    {
        if (canDamage)
        {
            if (tempTimer <= 0)
            {
                tempTimer = timer;
                StartCoroutine(Attack());
            }
            else
            {
                tempTimer -= Time.deltaTime;
            }
        }
    }

    private IEnumerator Attack()
    {
        _anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.35f);
        _anim.SetBool("isAttack", false);
        player.ApplyDamage(DamageValue);
        tempTimer -= 0.5f;
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
