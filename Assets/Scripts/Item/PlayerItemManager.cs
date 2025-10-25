using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] ItemHolder holder;
    [SerializeField] Transform playerCenter;

    PlayerInput control;

    private PlayerController playerController;



    private void Awake()
    {
        ItemRenderer renderer = GetComponentInChildren<ItemRenderer>();
        if(renderer)
        {
            holder= new ItemHolder(renderer);
        }
        else {
            holder = new ItemHolder();
        }

        control = new PlayerInput();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        control.Enable();


        if (playerController.isPlayer1)
        {
            control.Player1.Act.performed += Act;
        }
        else
        {
            control.Player2.Act.performed += Act;
        }
    }

    private void OnDisable()
    {
        control.Disable();
    }


    private void Act(InputAction.CallbackContext _)
    {
        var cols = Physics2D.OverlapCircleAll(this.transform.position, 1.35f);
        cols = sortByDistance(cols);

        Item itemToPickUp = getItemToPickUp(cols);
        ItemTable table = isTableNear(cols);
        ColoringStation coloringStation = isColoringStationNear(cols);
        ItemPickUpAnimal animalPickUp = getAnimalPickUp(cols);


        if (!holder.hasItem() && itemToPickUp != null)
        {
            holder.setItem(itemToPickUp);
        }

        else if ( !holder.hasItem() && animalPickUp!= null)
        {
            holder.setItem(animalPickUp.pickAnimal());
        }

        else if (table != null)
        {
            if (holder.getItem()?.GetType() == typeof(ToolItem) && holder.getItem().name == "Maquinilla" && table.getItem()?.GetType() == typeof(AnimalItem))
            {
                table.SetParticle();

                if (table.processAndCheck())
                {
                    AnimalItem animalItem = (AnimalItem)table.getItem();
                    holder.dropItem();
                    holder.setItem(animalItem.hairToDrop);
                    table.pickItem();
                    table.setItem(animalItem.baldAnimal);
                }
            }
            else if (holder.getItem()?.GetType() == typeof(ToolItem) && holder.getItem().name == "Pegamento" && table.getItem()?.GetType() == typeof(RawHairItem))
            {
                table.SetParticle();

                if (table.processAndCheck())
                {
                    RawHairItem rawHairItem = (RawHairItem)table.getItem();
                    holder.dropItem();
                    table.pickItem();

                    WigItem wigItem = ScriptableObject.CreateInstance<WigItem>();
                    wigItem.sprite = rawHairItem.wigProduct.sprite;
                    wigItem.color = WigColor.uncolored;

                    table.setItem(wigItem);
                }
            }
            else if (table.hasItem() && !holder.hasItem())
            {
                holder.setItem(table.pickItem());
            }

            else if (!table.hasItem() && holder.hasItem())
            {
                table.setItem(holder.dropItem());
            }

        }

        else if (coloringStation != null)
        {
            if (holder.getItem()?.GetType() == typeof(WigItem)&& !coloringStation.hasItem()) { 
            
                coloringStation.setItem(holder.dropItem());
            }
            else if(coloringStation.hasItem()) 
            {

                WigItem wig = (WigItem)coloringStation.getItem();
                if (wig.color != coloringStation.color)
                {
                    coloringStation.SetParticle();

                    if (coloringStation.processAndCheck())
                    {
                        wig.color = coloringStation.color;
                        coloringStation.updateColor();
                    }
                }
                else
                {
                    holder.setItem(coloringStation.pickItem());
                }

            }
        }   

        else if (holder.hasItem() && isTrashCanNear(cols))
        {
            holder.dropItem();
        }
    }

    private ItemPickUpAnimal getAnimalPickUp(Collider2D[] cols)
    {
        foreach (Collider2D col in cols)
        {
            ItemPickUpAnimal animal;
            if (col.transform.TryGetComponent<ItemPickUpAnimal>(out animal))
            {
                return animal;
            }
        }

        return null;
    }

    private Collider2D[] sortByDistance(Collider2D[] cols)
    {
        return cols.OrderBy(c => Vector3.Distance(playerCenter.position, c.transform.position)).ToArray();

    }

    private Item getItemToPickUp(Collider2D[] cols)
    {

        foreach (Collider2D col in cols)
        {
            ItemPickUpElement itemBox;
            if (col.transform.TryGetComponent<ItemPickUpElement>(out itemBox))
            {
                return itemBox.item;
            }
        }

        return null;
    }

    private ColoringStation isColoringStationNear(Collider2D[] cols)
    {
        ColoringStation coloringStation;
        foreach (Collider2D col in cols)
        {
            if (col.transform.TryGetComponent<ColoringStation>(out coloringStation))
            {
                return coloringStation;
            }
        }

        return null;
    }

    private ItemTable isTableNear(Collider2D[] cols)
    {
        foreach(Collider2D col in cols)
        {
            ItemTable table;
            if(col.transform.TryGetComponent<ItemTable>(out table))
            {
                return table;

            }
        }
        return null;
    }

    private bool isTrashCanNear(Collider2D[] cols)
    {

        foreach (Collider2D col in cols)
        {
            if (col.transform.tag == "TrashCan")
            {
                return true;
            }
        }

        return false;
    }




}
