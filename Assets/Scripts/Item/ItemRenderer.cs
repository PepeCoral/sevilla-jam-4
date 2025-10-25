using UnityEngine;

public class ItemRenderer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    Color32[] wigColors = {new Color32(241, 99, 85,255), new Color32(146, 88, 62,255) , new Color32(241, 196, 85, 255), new Color32(255,255,255,255) };

    public void OnItemChange(Item item_) {

        
        if(item_?.GetType()== typeof(WigItem))
        {

            WigItem wig = (WigItem)item_;
            spriteRenderer.sprite = item_.sprite;
            spriteRenderer.color = wigColors[(int)wig.color];
        }
        else if (item_ != null)
        {
            spriteRenderer.sprite = item_.sprite;
        }
        else
        {
            spriteRenderer.sprite = null;
            spriteRenderer.color= Color.white;
        }
    }
}
