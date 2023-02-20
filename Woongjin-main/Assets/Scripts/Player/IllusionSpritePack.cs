using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionSpritePack : MonoBehaviour
{
    [SerializeField] Sprite[] mySpritePack;
    SpriteRenderer spriteRenderer;

    bool isSetup;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(int a)
    {
        spriteRenderer.sprite = mySpritePack[a];
        isSetup = true;
    }

    public void Update()
    {
        if(isSetup)
        {
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime * 1.5f;
            spriteRenderer.color = color;

            if (color.a < 0) Destroy(gameObject);
        }
    }
}
