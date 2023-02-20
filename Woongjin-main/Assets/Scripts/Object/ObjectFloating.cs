using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloating : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float radius;
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float runningTime = Time.time * speed;
        float newY = radius * Mathf.Sin(runningTime);

        transform.position = startPos + new Vector3(0,newY);

    }
}