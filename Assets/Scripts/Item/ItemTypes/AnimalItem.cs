using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Items/Animal")]

public class AnimalItem: Item
{
    [SerializeField] public RawHairItem hairToDrop;
    [SerializeField] public BaldAnimalItem baldAnimal;

    
}
