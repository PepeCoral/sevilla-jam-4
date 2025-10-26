using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundHelper: HandyScripts.Singleton<SoundHelper>
{

    [SerializeField] private GameObject soundGO;
    public void PlaySound(AudioClip clip)
    {
        GameObject sound = GameObject.Instantiate(soundGO);
        AudioSource source = sound.GetComponent<AudioSource>();
        source.loop = false;
        source.clip = clip;
        source.Play();
    }

    public void PlayRandomSound(List<AudioClip> clips)
    {
        int rd = Random.Range(0, clips.Count);
        PlaySound(clips[rd]);   
    }

}
