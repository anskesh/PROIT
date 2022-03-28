using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int type; // 0 - мобы, 1 - мелкие ресурсы, 2 - крупные ресурсы
    [SerializeField] private int[] id; // тип ресурса
    private int[] _count = new int[2];
    private bool _isNear = false;
    private int _damage = 15;
    private Spawner _spawner;
    private int _health;

    private void Awake()
    {
        if (type == 1) _health = _damage;
        else if (type == 0) _health = 0;
        else _health = Random.Range(_damage + 1, _damage * 2) * type;
        _spawner = GetComponentInChildren<Spawner>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CanApplyDamage() && _isNear)
        {
            TakeDamage(_damage);
            _count[0] = Random.Range(3, 5);
            if (type == 0) type = 1;
            _count[1] = Random.Range(0, 3) * type;
            _spawner.SpawnResource(id, _count);
        }
    }

    private bool CanApplyDamage()
    {
        return _health > 0;
    }
    private void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerItemTrigger")
        {
            _isNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerItemTrigger")
        {
            _isNear = false;
        }
    }
}
