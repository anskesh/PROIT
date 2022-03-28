using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.Translate(MoveDelta * Time.deltaTime);        
    }
}
