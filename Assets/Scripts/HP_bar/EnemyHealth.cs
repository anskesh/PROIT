using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float max_health = 100;
    private float cur_Health;
    [SerializeField] private Image Hp_bar;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private int itemID;
    
    [SerializeField] private int minCount;
    [SerializeField] private int maxCounnt;

    public DataBase data;

    public void Awake()
    {
        SetMaxHealth();
        UpdateHealthBar();
        data = FindObjectOfType<DataBase>();
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
        Debug.Log(cur_Health);
    }

    public void ThrowItem()
    {       
        GameObject throwItem = Instantiate(dropItem, transform.position, new Quaternion());
        ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();

        itemPrefabClass.item.id = itemID;
        itemPrefabClass.count = Random.Range(minCount,maxCounnt);
        itemPrefabClass.item.img = data.items[itemID].img;
        throwItem.GetComponent<SpriteRenderer>().sprite = data.items[itemID].img;
        itemPrefabClass.item.name = data.items[itemID].name;            
    }
    private IEnumerator DeathScreen(float waitTime)
    {
        gameObject.GetComponent<PlayerConfig>().SpeedMultiply = 0;
        yield return new WaitForSeconds(waitTime);
        star.color = new Color(star.color.r, star.color.g, star.color.b, 255);
        blinkCoroutine = null;
    }
    private void Died()
    {
        ThrowItem();
        Destroy(gameObject);
    }
}
