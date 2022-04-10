using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float _currentHealth;
    private Inventory _inventory;

    public int Damage { get; set; } = 15;
    public event Action<float> HealthChanged;
    public event Action Dead;
    
    public float Health
    {
        get => _currentHealth;
        set
        {
            if (value > maxHealth) value = maxHealth;
            else if (value < 0) value = 0;
            _currentHealth = value;
        }
    }

    public void Start()
    {
        FindObjectOfType<Load>().LoadHealth();
        HealthChanged?.Invoke(Health / maxHealth);
        _inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if (Health == 0) Die();
        HealthChanged?.Invoke(Health / maxHealth);
    }

    public void AddHealth(int heal)
    {
        Health += heal;
        HealthChanged?.Invoke(Health / maxHealth);
    }

    public void SetMaxHealth()
    {
        Health = maxHealth;
        HealthChanged?.Invoke(Health / maxHealth);
    }
    private void Die()
    {
        Dead?.Invoke();
        for (var i = 0; i < _inventory.maxCount; i++)
        {
            _inventory.ThrowItem(i, transform);
        }
    }
}
