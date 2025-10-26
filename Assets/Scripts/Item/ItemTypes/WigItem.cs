using UnityEngine;

public enum WigColor { red, brown, yellow, uncolored }
public enum WigType { galgo, gato, erizo, oveja}

[CreateAssetMenu(fileName = "Item", menuName = "Items/Wig Item")]
public class WigItem: Item
{
   public WigColor color = WigColor.uncolored;
    public WigType wigType;
    public Sprite wigForBald;
}
