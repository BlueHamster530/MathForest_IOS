using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChecker : MonoBehaviour
{
    AudioSource ase;

    private void Awake()
    {
        ase = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!ase.isPlaying) Destroy(gameObject);
    }
}
