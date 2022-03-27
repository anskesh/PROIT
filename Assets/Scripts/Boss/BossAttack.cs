using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAttack : MonoBehaviour
{
    public int DamageValue = 25;
    private bool canDamage1 = false;
    private bool canDamage2 = true;
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
            if (canDamage1 && canDamage2)
            {
                attackCooldown = timer;
                StartCoroutine(Attack());
            }   


        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }

        if (specCooldown <= 0)
        {
            if (canDamage1 && canDamage2)
            {
                specCooldown = timer*10;
                StartCoroutine(SpecAttack());
            }


        }
        else
        {
            specCooldown -= Time.deltaTime;
        }
    }

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canDamage1 = true;
            player = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canDamage1 = false;
        }
    }

    IEnumerator Attack()
    {
        canDamage2 = false;
        bossAI.SpeedMultiply = 0;
        animator.SetBool("isattack", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isattack", false);
        canDamage2 = true;
        bossAI.SpeedMultiply = speed;
        player.GetComponent<PlayerHealth>().ApplyDamage(DamageValue);
    }
    IEnumerator SpecAttack()
    {
        canDamage2 = false;
        bossAI.SpeedMultiply = 0;
        animator.SetBool("isattack", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isattack", false);
        canDamage2 = true;
        bossAI.SpeedMultiply = speed;
        player.GetComponent<PlayerHealth>().ApplyDamage(DamageValue*2);
    }
}
