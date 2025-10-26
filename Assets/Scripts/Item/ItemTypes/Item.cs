using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Simple Item")]
public class Item : ScriptableObject
{
    [SerializeField] public Sprite sprite;
}
