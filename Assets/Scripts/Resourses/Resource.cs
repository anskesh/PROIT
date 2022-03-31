using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int type; // 0 - мобы, 1 - собираются без инструментов, 2 - собираются при помощи инструментов
    [SerializeField] private int typeSize; // 1 - мелкий, 2 - большой
    [SerializeField] private int[] id; // типы ресурса, которые выпадают
    [SerializeField] private int typeTool; // 1 - меч, 2 - кирка, 3 - топор
    private int[] _count = new int[2];
    private bool _isNear = false;
    private int _damage = 15;
    private Spawner _spawner;
    private int _health;

    private void Awake()
    {
        if (typeSize == 1) _health = _damage;
        else _health = Random.Range(_damage + 1, _damage * 2 + 1) * typeSize;
        _spawner = GetComponentInChildren<Spawner>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (CanApplyDamage() && _isNear && HasTool())
        {
            float muliplierDamage = FindObjectOfType<TakeTools>().MultiplierDamage;
            TakeDamage((int) (_damage * muliplierDamage));
            if (typeSize == 1) muliplierDamage = 1; // если ресурс ломается с одного удара - не увеличиваем количество предметов 
            _count[0] = (int) (Random.Range(2, 5) * muliplierDamage);
            _count[1] = (int) (Random.Range(0, 3) * typeSize * muliplierDamage);
            _spawner.SpawnResource(id, _count);
        }
    }

    private bool HasTool()
    {
        if (type == 2)
        {
            string[] tools = {"меч", "кирка", "топор" };
            var tool = FindObjectOfType<TakeTools>();
            if (tool == null) return false;
            if (tool.Name.Contains(tools[typeTool - 1])) return true;
            else
            {
                string toolName = tools[typeTool - 1]; 
                StartCoroutine(FindObjectOfType<Coroutines>().ShowText(toolName));
                return false;
            }
        }
        else return true;
    }
  
    private bool CanApplyDamage() =>_health > 0;
    private void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
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
