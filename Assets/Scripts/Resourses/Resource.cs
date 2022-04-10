using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour, IMineable
{
    [SerializeField] private int type; // 0 - мобы, 1 - собираются без инструментов, 2 - собираются при помощи инструментов
    [SerializeField] private int typeSize; // 1 - мелкий, 2 - средний, 3 - большой
    [SerializeField] private int[] id; // типы ресурса, которые выпадают
    [SerializeField] private int typeTool; // 1 - меч, 2 - кирка, 3 - топор
    
    private int[] _count = new int[2];
    private int _minHealth = 15;
    private Spawner _spawner;
    private int _health = 100;
    public bool IsNear { get; set; }

    private void Awake()
    {
        if (typeSize == 1) _health = _minHealth;
        if (type == 2) _health = _health / 2;
        _spawner = GetComponentInChildren<Spawner>();
    }

    public void MineResource()
    {
        if (CanApplyDamage() && HasTool())
        {
            float muliplierDamage = FindObjectOfType<TakeTools>().MultiplierDamage;
            TakeDamage((int) (_minHealth * muliplierDamage));
            StartCoroutine(ChangeColor());
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
                StartCoroutine(Utils.ShowText(toolName));
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
    private IEnumerator ChangeColor()
    {
        var sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.Lerp(sprite.color, Color.red, 1f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.Lerp(sprite.color, Color.white, 1f);
    }
    
    private void Destroy()
    {
        if (typeSize == 1) {
            _count[0] = (int) (Random.Range(1, 4));
        }
        else if (typeSize == 2)
        {
            _count[0] = (int) (Random.Range(1, 4));
            _count[1] = (int) (Random.Range(0, 2));
        }
        else if (typeSize == 3)
        {
            _count[0] = (int) (Random.Range(3, 6));
            _count[1] = (int) (Random.Range(0, 3));
        }
        _spawner.SpawnResource(id, _count);
        Destroy(gameObject);
    }
}
