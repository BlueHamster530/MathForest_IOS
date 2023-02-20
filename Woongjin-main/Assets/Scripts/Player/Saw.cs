using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] GameObject chara;
    public float theta;
    [SerializeField] float speed;
    public float range;

    float n;

    bool isSetup;

    public void Setup(Transform parent)
    {
        transform.SetParent(parent);
        isSetup = true;
    }

    void Update()
    {
        if(isSetup)
        {
            n += Time.deltaTime;
            n = n % 1;

            range = -12 * Mathf.Pow((n - 0.5f), 2) + 3;

            theta += Time.deltaTime * speed;

            Vector2 CharPos = transform.parent.position;

            Vector2 myPos = new Vector2(range * Mathf.Cos(theta * Mathf.Deg2Rad), range * Mathf.Sin(theta * Mathf.Deg2Rad));

            Vector2 FinalPos = CharPos + myPos;

            transform.position = FinalPos;
        }
        
    }
}
