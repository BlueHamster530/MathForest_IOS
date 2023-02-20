using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;

    [SerializeField] float speed;

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);

        if(Vector3.Distance(transform.position, endPos) < 0.001f)
        {
            transform.position = startPos;
        }
    }
}
