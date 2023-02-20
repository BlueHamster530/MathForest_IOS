using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetter : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void SoundSetup(int a)
    {
        source.clip = clips[a];
        source.Play();
    }

    public void RandomSoundSetup()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }
}
