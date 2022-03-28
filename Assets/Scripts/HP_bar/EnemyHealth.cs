using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IPointerClickHandler
{
    public float max_health = 100;
    private float cur_Health;   
    [SerializeField] private Image Hp_bar;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private int itemID;
    
    [SerializeField] private int minCount;
    [SerializeField] private int maxCounnt;
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

    public void AddHealth(float changeValue)
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

    public void ThrowItem()
    {       
        GameObject throwItem = Instantiate(dropItem, transform.position, new Quaternion());
        ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();

        itemPrefabClass.ID = itemID;
        itemPrefabClass.Count = Random.Range(minCount, maxCounnt);   
    }
    
    private void Died()
    {        
        ThrowItem();
        Destroy(transform.parent.gameObject);
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
