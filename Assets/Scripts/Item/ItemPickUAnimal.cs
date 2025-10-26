using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpAnimal : MonoBehaviour
{

    [SerializeField] public List<AnimalItem>  items ;

    ItemHolder holder ;

    private void Awake()
    {
        ItemRenderer renderer = GetComponentInChildren<ItemRenderer>();
        holder = new ItemHolder(renderer);
    }
    private void Start()
    {
        holder.setItem(randomAnimal());
    }

    private AnimalItem randomAnimal() 
    {

        int rand = Random.Range(0, items.Count);
        return items[rand];
    }

    public AnimalItem pickAnimal()
    {
        AnimalItem currentAnimal = (AnimalItem)holder.dropItem();
        holder.setItem(randomAnimal()); 

        return currentAnimal;
    }
}
