using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ItemHolder : MonoBehaviour
{
    [SerializeField ] Item item = null;

    PlayerInput control;
    [SerializeField] bool isPlayer1;


    private void Awake()
    {
        control = new PlayerInput();
    }

    private void OnEnable()
    {
        control.Enable();


        if (isPlayer1)
        {
            control.Player1.Act.performed += Act;
        }
        else
        {
            control.Player2.Act.performed += Act;
        }
    }


    private void Act(InputAction.CallbackContext _) 
    {

        var cols = Physics2D.OverlapCircleAll(this.transform.position, 1.2f);

        Item itemToPickUp = getItemToPickUp(cols);

        if (item == null && itemToPickUp != null )
        {

            print("Item was picked up");
            item = itemToPickUp;
        }

        if (item != null && isTrashCanNear(cols)) {
            item = null;
        }
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

    private bool isTrashCanNear(Collider2D[] cols)
    { 
    
        foreach (Collider2D col in cols)
        {
            if (col.transform.tag == "TrashCan") {
            return true;}
        }

        return false;
    }






    private void OnDisable()
    {
        control.Disable();
    }
}
