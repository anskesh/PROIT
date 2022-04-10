using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private float speedMultiply = 1;
    [SerializeField] private Transform enemy;
    
    private Transform _player;
    private bool isPlayerNear = false;
    private Vector3 _direction;

    private void FixedUpdate()
    {
        if (isPlayerNear)
        {
            _direction = Vector3.Normalize(_player.position - enemy.position);
            if (_direction.x <= 0)
            {
                enemyRenderer.flipX = true;
            }
            else if (_direction.x > 0)
            {
                enemyRenderer.flipX = false;
            }

            //Debug.Log(Vector2.Distance(_direction, transform.position));
            //if (Vector2.Distance(_direction, transform.position) > 12f)
            enemy.Translate(_direction * speedMultiply * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _player = player.transform;
            isPlayerNear = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {            
            isPlayerNear = false;
        }
    }

}
