using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IItem, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _count;

    public event Action<Cell, string> Clicked;
    public int ID { get; set; }
    public Sprite Image => _image.sprite;
    public int Count { get; set; }

    public void Render(IItem item)
    {
        if (item.Image != null) _image.gameObject.SetActive(true);
        else _image.gameObject.SetActive(false);
        _image.sprite = item.Image;
        Count = item.Count;
        if (item.Count != 0 && item.Count != 1) _count.text = item.Count.ToString();
        else _count.text = "";
        ID = item.ID;
    }
    
    public void ChangeCount()
    {
        if (Count != 0 && Count != 1) _count.text = Count.ToString();
    }

    public void Clear()
    {
        _image.gameObject.SetActive(false);
        Count = 0;
        ID = 0;
        _image.sprite = null;
        _count.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick.transform.parent.TryGetComponent(out InventoryCell inventory))
        {
            Clicked?.Invoke(this, gameObject.name);
        }
    }
}
