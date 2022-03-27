using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float max_health = 100;
    private float cur_Health;
    private bool isAlive;
    [SerializeField] private Image Hp_bar;
    public GameObject DeathImg;
    private Inventory inventory;

    public void Awake()
    {
        SetMaxHealth();
        isAlive = true;
        UpdateHealthBar();
        inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
    }

    public void UpdateHealthBar()
    {
        Hp_bar.fillAmount = cur_Health / max_health;
    }
    public void SetMaxHealth()
    {
        cur_Health = max_health;
        UpdateHealthBar();
    }

    public void ApplyDamage(float changeValue)
    {
        if (isAlive)
        {
            cur_Health -= changeValue;

            if (cur_Health <= 0)
            {
                cur_Health = 0;
                Died();
            }
            else if (cur_Health > max_health)
            {
                cur_Health = max_health;
            }
            UpdateHealthBar();
        }
    }

    public void AddHealth(float changeValue)
    {
        if (isAlive)
        {
            cur_Health += changeValue;

            if (cur_Health <= 0)
            {
                cur_Health = 0;
                Died();
            }
            else if (cur_Health > max_health)
            {
                cur_Health = max_health;
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
        ShowDeathSprite();

        yield return new WaitForSeconds(2);
        DeathImg.SetActive(false);
        isAlive = true;
        gameObject.GetComponent<PlayerConfig>().SpeedMultiply = speed;
    }

    private void ShowDeathSprite()
    {

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
