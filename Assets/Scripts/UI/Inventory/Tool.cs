using UnityEngine;

[CreateAssetMenu(menuName = "Items/Tool", fileName = "New tool")]
public class Tool : ItemAsset
{
    [SerializeField] private int damage;
}
