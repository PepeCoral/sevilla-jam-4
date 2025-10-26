using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] ItemHolder holder;
    [SerializeField] Transform playerCenter;

    PlayerInput control;

    private PlayerController playerController;

    [SerializeField] AudioClip placeWig;
    [SerializeField] AudioClip dropItem;
    [SerializeField] AudioClip trashItem;
    [SerializeField] List<AudioClip> pickUps;
    [SerializeField] AudioClip trashSound;

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


    private void Act()
    {
        var cols = Physics2D.OverlapCircleAll(this.transform.position, 1f);
        cols = sortByDistance(cols);

        Item itemToPickUp = findComponent<ItemPickUpElement>(cols)?.item;
        ItemTable table = findComponent<ItemTable>(cols);
        ColoringStation coloringStation = findComponent<ColoringStation>(cols);
        ItemPickUpAnimal animalPickUp = findComponent<ItemPickUpAnimal>(cols);
        BaldChair baldChair = findComponent<BaldChair>(cols);


       

        if (holder.hasItem() && holder.getItem()?.GetType() == typeof(WigItem) && baldChair != null && baldChair.isBusy) 
        {
            SoundHelper.Instance.PlaySound(placeWig);
            baldChair.giveWig((WigItem)holder.dropItem());
        }

        else if (!holder.hasItem() && animalPickUp != null)
        {
            SoundHelper.Instance.PlayRandomSound(pickUps);

            holder.setItem(animalPickUp.pickAnimal());
        }
        
        else if (!holder.hasItem())
        {
            if(table != null && table.hasItem())
            {
                tableLogic(table);
            }
            else if(coloringStation!= null && coloringStation.hasItem())
            {
                coloringStationLogic(coloringStation);
            }
            else if (itemToPickUp != null)
            {
                holder.setItem(itemToPickUp);
            }


        }
        else if(coloringStation!=null && coloringStation.hasItem()|| coloringStation!=null && holder.hasItem() && holder.getItem().GetType() == typeof(WigItem))
        {
            print(4);
            coloringStationLogic(coloringStation);
        }
        else if (table != null)
        {
            tableLogic(table);
        }

        

        else if (holder.hasItem() && !(holder.getItem().GetType() == typeof(AnimalItem) || holder.getItem().GetType() == typeof(BaldAnimalItem)) && isTagNear(cols, "TrashCan"))
        {
            holder.dropItem();
        }

        else if (holder.hasItem() && (holder.getItem().GetType() == typeof(AnimalItem) || holder.getItem().GetType() == typeof(BaldAnimalItem)) && isTagNear(cols, "Trasportin"))
        {
            holder.dropItem();
        }

        
    }

    private void Act(InputAction.CallbackContext _)
    {
        Act();
    }
    private void coloringStationLogic(ColoringStation coloringStation)
    {
        if (holder.getItem()?.GetType() == typeof(WigItem) && !coloringStation.hasItem())
        {

            coloringStation.setItem(holder.dropItem());
            SoundHelper.Instance.PlaySound(dropItem);

        }
        else if (coloringStation.hasItem())
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
            else if (!holder.hasItem())
            {
                holder.setItem(coloringStation.pickItem());
                SoundHelper.Instance.PlayRandomSound(pickUps);

            }

        }
    }

    private void tableLogic(ItemTable table)
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
                wigItem.wigType = rawHairItem.wigProduct.wigType;
                wigItem.wigForBald = rawHairItem.wigProduct.wigForBald;
                wigItem.color = WigColor.uncolored;

                table.setItem(wigItem);
            }
        }
        else if (table.hasItem() && !holder.hasItem())
        {
            holder.setItem(table.pickItem());
            SoundHelper.Instance.PlayRandomSound(pickUps);
            SoundHelper.Instance.PlaySound(trashItem);


        }

        else if (!table.hasItem() && holder.hasItem())
        {
            table.setItem(holder.dropItem());
            SoundHelper.Instance.PlaySound(dropItem);
            SoundHelper.Instance.PlaySound(trashItem);


        }
    }


    private Collider2D[] sortByDistance(Collider2D[] cols)
    {
        return cols.OrderBy(c => Vector3.Distance(playerCenter.position, c.transform.position)).ToArray();

    }

    private bool isTagNear(Collider2D[] cols, String tag)
    {

        foreach (Collider2D col in cols)
        {
            if (col.transform.tag == tag)
            {
                return true;
            }
        }

        return false;
    }


    private T findComponent<T>(Collider2D[] cols) 
    {
        foreach (Collider2D col in cols)
        {
            T component;
            if (col.transform.TryGetComponent<T>(out component))
            {
                return component;

            }
        }
        return default(T);
    }

    private void Update()
    {
        if (playerController.isPlayer1)
        {
            if (Gamepad.all.Count > 0 && (Gamepad.all[0].buttonEast.wasPressedThisFrame || Gamepad.all[0].buttonSouth.wasPressedThisFrame))
            {
                Act();
            }
        }
        else
        {
            if (Gamepad.all.Count > 1 && (Gamepad.all[1].buttonEast.wasPressedThisFrame || Gamepad.all[1].buttonSouth.wasPressedThisFrame))
            {
                Act();
            }
        }
    }






}
