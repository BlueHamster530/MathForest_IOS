using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJ_APIColliderTest : MonoBehaviour
{
    [SerializeField]
    WJ_Sample sample;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sample.OnClick_MakeQuestion();
        }
    }
}
