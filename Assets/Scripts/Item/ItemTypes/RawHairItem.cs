using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Raw Hair")]

public class RawHairItem: Item
{
    [SerializeField] public  WigItem wigProduct;
}
