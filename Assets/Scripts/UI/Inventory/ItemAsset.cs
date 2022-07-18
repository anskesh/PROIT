using UnityEngine;

public abstract class ItemAsset : ScriptableObject, IItem
{
    [SerializeField] private int id;
    [SerializeField] private string nameItem;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;

    public string Name => nameItem;
    public Sprite Image => image;
    public int Count { get; set; }
    public int ID
    {
	    get => id;
	    set => id = value;
    }
}
