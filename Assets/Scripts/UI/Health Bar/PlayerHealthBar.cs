using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        healthBar.fillAmount = health;
    }
}
