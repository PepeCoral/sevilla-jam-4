using UnityEngine;

public enum WigColor { red, brown, yellow, uncolored }

[CreateAssetMenu(fileName = "Item", menuName = "Items/Wig Item")]
public class WigItem: Item
{
   public WigColor color = WigColor.uncolored;
}
