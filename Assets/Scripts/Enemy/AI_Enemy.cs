using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy : MonoBehaviour
{
    private SpriteRenderer enemyRenderer;
    private Transform player;
    public Transform enemy;
    private bool IsTriggerPlayer = false;
    private Vector3 MoveDelta;
    public float SpeedMultiply = 1;

    void Start()
    {
        enemyRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (IsTriggerPlayer)
        {
            MoveDelta = player.position - transform.position;
            if (MoveDelta.x < 0)
            {
                enemyRenderer.flipX = false;
            }
            else if (MoveDelta.x > 0)
            {
                enemyRenderer.flipX = true;
            }
            enemy.Translate(MoveDelta * Time.deltaTime * SpeedMultiply);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.transform;
            IsTriggerPlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {            
            IsTriggerPlayer = false;
        }
    }

}
