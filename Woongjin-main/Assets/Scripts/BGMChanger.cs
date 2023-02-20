using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChanger : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource source;

    public void BGMChange(int a)
    {
        source.clip = clips[a];
        source.Play();
    }

}
