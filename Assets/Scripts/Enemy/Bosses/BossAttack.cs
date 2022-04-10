using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAttack : MonoBehaviour
{
    public int DamageValue = 10;
    private bool isNear;
    private bool canDamage = true;
    private GameObject player;

    public float timer = 2;
    private float attackCooldown;
    private float specCooldown;
    private float speed;
    [SerializeField] private BossAI bossAI;
    [SerializeField] private Animator animator;

    private void Start()
    {
        speed = bossAI.SpeedMultiply;
    }

    public void FixedUpdate()
    {
        if (attackCooldown <= 0)
        {
            if (isNear && canDamage)
            {
                attackCooldown = timer;
                StartCoroutine(Attack());
            }
        }
        else attackCooldown -= Time.deltaTime;

        if (specCooldown <= 0)
        {
            if (isNear && canDamage)
            {
                specCooldown = timer * 2.5f;
                StartCoroutine(SpecAttack());
            }
        }
        else specCooldown -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
            player = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = false;
        }
    }

    IEnumerator Attack()
    {
        canDamage = false;
        bossAI.SpeedMultiply = 0;
        animator.SetBool("isMove", false);
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Player>().ApplyDamage(DamageValue);
        animator.SetBool("isAttack", false);
        canDamage = true;
        bossAI.SpeedMultiply = speed;
    }
    IEnumerator SpecAttack()
    {
        canDamage = false;
        bossAI.SpeedMultiply = 0;
        animator.SetBool("isMove", false);
        animator.SetBool("isSpecAttack", true);
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Player>().ApplyDamage(DamageValue*2);
        animator.SetBool("isSpecAttack", false);
        canDamage = true;
        bossAI.SpeedMultiply = speed;
    }
}
