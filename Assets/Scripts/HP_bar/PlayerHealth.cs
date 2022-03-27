using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float max_health = 100;
    private float cur_Health;
    [SerializeField] private Image Hp_bar;
    private Inventory inventory;

    public void Awake()
    {
        SetMaxHealth();
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

    public void ChangeHealth(float changeValue)
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
        //Debug.Log(cur_Health);
    }

    private IEnumerator DeathScreen(float waitTime)
    {
        gameObject.GetComponent<PlayerConfig>().SpeedMultiply = 0;
        yield return new WaitForSeconds(waitTime);
        ShowDeathSprite();
        transform.position = new Vector3(0, 0, 0);
        SetMaxHealth();
    }

    private void ShowDeathSprite()
    {

    }

    private void Died()
    {
        for (int i = 0; i < inventory.maxCount; i++)
        {
            inventory.ThrowItem(i);
        }
        DeathScreen(2);
        
    }
}
