using UnityEngine;

public class ColoringStation: MonoBehaviour 
{
    [SerializeField] ItemHolder holder;
    [SerializeField] GameObject particle;
    [SerializeField] public WigColor color;
    [SerializeField] private AudioClip sound;

    private float particleDelay;
    [SerializeField] private float totalParticleDelay;

    private void Awake()
    {
        ItemRenderer renderer = GetComponentInChildren<ItemRenderer>();
        holder = new ItemHolder(renderer);
    }

    public Item getItem() => holder.getItem();
    public bool hasItem() => holder.hasItem();
    public void setItem(Item item)
    {

        holder.setItem(item);
    }
    public Item pickItem()
    {
        return holder.dropItem();
    }

    public void SetParticle()
    {
        particleDelay = totalParticleDelay;
    }
    private void Update()
    {
        if (particleDelay >= 0)
        {
            particleDelay -= Time.deltaTime;
        }

        particle.SetActive(particleDelay >= 0);

    }

    public int currentProgress = 0;
    public static int totalProgress = 10;

    public bool processAndCheck()
    {
        if( currentProgress == 0)
        {
            SoundHelper.Instance.PlaySound(sound);
        }
        currentProgress++;


        bool isDone = currentProgress >= totalProgress;
        if (isDone) { currentProgress = 0; }
        return isDone;

    }

    public void updateColor()
    {
       
        holder.updateItemRender(holder.getItem());
    }
}
