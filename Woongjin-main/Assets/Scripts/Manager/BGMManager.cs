using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] bool isRandom;
    [SerializeField] AudioClip[] randomMusicLists;

    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (isRandom)
        {
            audioSource.clip = randomMusicLists[Random.Range(0, randomMusicLists.Length)];
        }

        audioSource.Play();
    }
}
