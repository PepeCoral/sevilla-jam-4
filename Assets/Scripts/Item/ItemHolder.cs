using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[System.Serializable]
public class ItemHolder
{
    [SerializeField ] Item item = null;

    ItemRenderer itemRenderer = null;

    public ItemHolder()
    {

    }
    public ItemHolder(ItemRenderer renderer)
    {
        itemRenderer = renderer;
    }

    public bool hasItem() {  return item != null; }

    public Item dropItem()
    {
        var tempItem = item;
        item = null;

        updateItemRender(null);

        return tempItem;


    }

    public Item getItem() { return item; }

    public void setItem(Item item_)
    {
        if (item != null) Debug.LogError("Trying to set item with item already set");
        updateItemRender(item_);

        this.item = item_;
    }

    public void updateItemRender(Item item_)
    {
        if (itemRenderer != null)
        {
            itemRenderer.OnItemChange(item_);
        }
    }
}
