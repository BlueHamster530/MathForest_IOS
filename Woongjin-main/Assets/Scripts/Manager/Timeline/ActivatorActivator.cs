using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorActivator : MonoBehaviour
{
    [SerializeField] TimelineActivator activator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            activator.ActiveGo();
        }
    }
}
