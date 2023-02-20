using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] GameObject myboss;

    private void Awake()
    {
        myboss.SetActive(true);
    }

    private void OnEnabled()
    {
        myboss.SetActive(true);
    }
}
