using UnityEngine;

public class BossAI : MonoBehaviour
{
    private SpriteRenderer enemyRenderer;
    private Transform player;
    public Transform enemy;
    private bool IsTriggerPlayer = false;
    private Vector3 MoveDelta;
    public float SpeedMultiply = 1;
    private Animator _animator;

    void Start()
    {
        enemyRenderer = GetComponentInParent<SpriteRenderer>();
        _animator = GetComponentInParent<Animator>();
    }

    private void FixedUpdate()
    {
        if (IsTriggerPlayer && SpeedMultiply > 0)
        {
            _animator.SetBool("isMove", true);
            MoveDelta = Vector3.Normalize(player.position - transform.position);
            if (MoveDelta.x < 0)
            {
                enemyRenderer.flipX = true;
            }
            else if (MoveDelta.x > 0)
            {
                enemyRenderer.flipX = false;
            }
            enemy.Translate(MoveDelta * Time.deltaTime * SpeedMultiply);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            IsTriggerPlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsTriggerPlayer = false;
        }
    }
}
