using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceThrower : MonoBehaviour
{
    [SerializeField] GameObject iceFlakeParticle;

    SpriteRenderer spriteRenderer;
    float currentSize;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject ifP = Instantiate(iceFlakeParticle, transform.position, Quaternion.identity);
        ifP.transform.SetParent(transform);
    }
    void Start()
    {
        currentSize = 0.2f;        
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        currentSize += Time.deltaTime;
        currentSize = Mathf.Clamp(currentSize, 0f, 2f);

        transform.localScale = new Vector3(currentSize, currentSize, currentSize);
        
        if(currentSize >= 1.0f)
        {
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime;

            spriteRenderer.color = color;
        }

        if (spriteRenderer.color.a <= 0f) Destroy(gameObject);

    }
}
