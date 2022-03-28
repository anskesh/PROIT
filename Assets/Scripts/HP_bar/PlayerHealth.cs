using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isAlive;
    [SerializeField] private Image hpBar;
    public GameObject DeathImg;
    private Inventory inventory;

    public int Health
    {
        get => currentHealth;
        set
        {
            if (value > maxHealth) value = maxHealth;
            currentHealth = value;
        }
    }

    public void Awake()
    {
        SetMaxHealth();
        isAlive = true;
        UpdateHealthBar();
        inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
        Save save = FindObjectOfType<Save>();
        SetSaveHealth(save.LoadHealth());
    }

    private void UpdateHealthBar()
    {
        hpBar.fillAmount = currentHealth / (float) maxHealth;
    }
    private void SetMaxHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void SetSaveHealth(int health)
    {
        currentHealth = health;
        UpdateHealthBar();
    }

    public void ApplyDamage(int changeValue)
    {
        if (isAlive)
        {
            currentHealth -= changeValue;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Died();
            }
            UpdateHealthBar();
        }
    }

    public void AddHealth(int changeValue)
    {
        if (isAlive)
        {
            currentHealth += changeValue;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Died();
            }
            else if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateHealthBar();
        }
    }

    IEnumerator DeathScreen()
    {
        DeathImg.SetActive(true);
        float speed = gameObject.GetComponent<PlayerConfig>().SpeedMultiply;
        gameObject.GetComponent<PlayerConfig>().SpeedMultiply = 0;
        transform.position = new Vector3(0, 0, 0);
        SetMaxHealth();

        yield return new WaitForSeconds(2);
        DeathImg.SetActive(false);
        isAlive = true;
        gameObject.GetComponent<PlayerConfig>().SpeedMultiply = speed;
    }

    private void Died()
    {
        isAlive = false;
        for (int i = 0; i < inventory.maxCount; i++)
        {
            inventory.ThrowItem(i);
        }
        StartCoroutine(DeathScreen());
        
    }
}
