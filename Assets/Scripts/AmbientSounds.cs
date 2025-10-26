using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{

    [SerializeField] List<AudioClip> clips;
    float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = Random.Range(2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            currentTime = Random.Range(2f, 5f);
            SoundHelper.Instance.PlayRandomSound(clips);
        }
    }
}
