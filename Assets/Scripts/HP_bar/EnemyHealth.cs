using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IPointerClickHandler
{
    public float maxHealth = 100;
    private float currentHealth;   
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private int itemID;
    [SerializeField] private int minCount;
    [SerializeField] private int maxCount;
    
    private bool _isNear;
    private int _damage = 20;
    public DataBase data;

    public void Awake()
    {
        SetMaxHealth();
        UpdateHealthBar();
        data = FindObjectOfType<DataBase>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isNear)
        {
            ApplyDamage(_damage);
        }
    }

    private void UpdateHealthBar()
    {
        hpBar.fillAmount = currentHealth / maxHealth;
    }
    private void SetMaxHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void ApplyDamage(int changeValue)
    {
        currentHealth -= changeValue;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
        }
        UpdateHealthBar();
    }

    public void AddHealth(float changeValue)
    {
        currentHealth += changeValue;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    public void ThrowItem()
    {       
        GameObject throwItem = Instantiate(dropItem, transform.position, new Quaternion());
        ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();

        itemPrefabClass.ID = itemID;
        itemPrefabClass.Count = Random.Range(minCount, maxCount);   
    }
    
    private void Dead()
    {        
        ThrowItem();
        Destroy(transform.parent.gameObject);
    }
     
    private void OnTriggerStay2D(Collider2D collision)
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
