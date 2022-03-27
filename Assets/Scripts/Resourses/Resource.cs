using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int type; // 0 - мобы, 1 - мелкие ресурсы, 2 - крупные ресурсы
    [SerializeField] private int id; // тип ресурса
    private bool _isNear = false;
    private int _damage = 15;
    private Spawner _spawner;
    private int _health;

    private void Awake()
    {
        if (type == 1) _health = _damage;
        else _health = Random.Range(_damage + 1, _damage * 2) * type;
        _spawner = GetComponentInChildren<Spawner>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CanApplyDamage() && _isNear)
        {
            TakeDamage(_damage);
            _spawner.SpawnResource(id, Random.Range(2, 5));
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
