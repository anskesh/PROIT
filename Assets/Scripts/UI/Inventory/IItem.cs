using UnityEngine;

public interface IItem
{
    public Sprite Image { get; }
    public int Count { get; set; }
    public int ID { get; set; }
}
