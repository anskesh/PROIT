using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    private float health = 100;
    private bool isdead = false;
    private SpriteRenderer PlayerSpriteRender;
    private float x, y;
    private Vector3 MoveDelta;
    public float SpeedMultiply = 1;
    
    void Start()
    {
        PlayerSpriteRender = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal") * SpeedMultiply;
        y = Input.GetAxisRaw("Vertical") * SpeedMultiply;
        MoveDelta = new Vector3(x, y, 0);
        if (x < 0)
        {
            PlayerSpriteRender.flipX = false;
        }
        else if (x > 0)
        {
            PlayerSpriteRender.flipX = true;
        }
        transform.Translate(MoveDelta * Time.deltaTime);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogError("Объект с тегом: " + collision.tag);
    }

    public void TakeDamage(float damageValue)
    {
        health -= damageValue;
        Debug.Log(health);
        if (health <= 0)
        {
            isdead = true;
        }
    }
}
