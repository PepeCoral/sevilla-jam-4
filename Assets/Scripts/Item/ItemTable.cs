using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    [SerializeField] ItemHolder holder;
    [SerializeField] GameObject particle;

    private float particleDelay;
    [SerializeField] private float totalParticleDelay;

    [SerializeField] private List<AudioClip> cat;
    [SerializeField] private List<AudioClip> dog;
    [SerializeField] private List<AudioClip> erizo;
    [SerializeField] private List<AudioClip> oveja;
    [SerializeField] private AudioClip cutting;
    [SerializeField] private AudioClip creating;

    private void Awake()
    {
        ItemRenderer renderer = GetComponentInChildren<ItemRenderer>();
        holder = new ItemHolder(renderer);
    }

    public Item getItem() => holder.getItem();
    public bool hasItem() => holder.hasItem();
    public void setItem(Item item) {
    
        holder.setItem(item);
    }
    public Item pickItem() {
        return holder.dropItem();
    }

    public void SetParticle() {
        particleDelay = totalParticleDelay;
    }
    private void Update()
    {
        if (particleDelay>=0 )
        {
            particleDelay -= Time.deltaTime;
        }

        particle.SetActive(particleDelay >= 0);

    }

    public int currentProgress = 0;
    public static int totalProgress = 10;

    public bool processAndCheck()
    {
        if (currentProgress == 0)
        {
            if (holder.getItem().GetType() == typeof(AnimalItem))
            {
                switch (holder.getItem().name)
                {
                    case "Gato":
                        SoundHelper.Instance.PlayRandomSound(cat);
                        break;
                    case "Perro":
                        SoundHelper.Instance.PlayRandomSound(dog);
                        break;
                    case "Erizo":
                        SoundHelper.Instance.PlayRandomSound(erizo);
                        break;
                    case "Oveja":
                        SoundHelper.Instance.PlayRandomSound(oveja);
                        break;
                }

                SoundHelper.Instance.PlaySound(cutting);
            }
             if (holder.getItem().GetType() == typeof(RawHairItem))
            {
                SoundHelper.Instance.PlaySound(creating);
            }
        }

        currentProgress++;

        bool isDone = currentProgress >= totalProgress;
        if (isDone) { currentProgress = 0; }
        return isDone;

    }
}
