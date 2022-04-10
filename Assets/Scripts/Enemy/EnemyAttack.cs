using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttack : MonoBehaviour
{
    public int damageValue = 25;
    private bool canDamage = false;
    private Player _player;
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
                _player.ApplyDamage(damageValue);
                //StartCoroutine(Attack());
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
        _player.ApplyDamage(damageValue);
        tempTimer -= 0.5f;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            canDamage = true;
            _player = player;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            canDamage = false;
            _player = null;
        }
    }

    
}
