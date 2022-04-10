using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected Image hpBar;
    [SerializeField] private GameObject dropItem;
    [SerializeField] protected int itemID;
    [SerializeField] protected int minCount;
    [SerializeField] protected int maxCount;
    
    protected float _currentHealth;
    protected bool isDead;
    public bool IsNear { get; set; }

    public void Awake()
    {
        SetMaxHealth();
        UpdateHealthBar();
    }

    protected virtual void UpdateHealthBar()
    {
        hpBar.fillAmount = _currentHealth / maxHealth;
    }
    private void SetMaxHealth()
    {
        _currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void ApplyDamage(int changeValue)
    {
        if (isDead) return;
        StartCoroutine(ChangeColor());
        _currentHealth -= changeValue;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Dead();
        }
        UpdateHealthBar();
    }

    private IEnumerator ChangeColor()
    {
        var sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.Lerp(sprite.color, Color.red, 1f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.Lerp(sprite.color, Color.white, 1f);
    }
    
    public void ThrowItem()
    {       
        GameObject throwItem = Instantiate(dropItem, transform.position, new Quaternion());
        ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();

        itemPrefabClass.ID = itemID;
        itemPrefabClass.Count = Random.Range(minCount, maxCount);   
    }
    
    protected virtual void Dead()
    {
        isDead = true;
        ThrowItem();
        Destroy(gameObject);
    }
}
