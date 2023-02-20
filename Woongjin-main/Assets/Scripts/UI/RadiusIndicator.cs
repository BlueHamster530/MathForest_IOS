using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusIndicator : MonoBehaviour
{
    Material myMaterial;
    PlayerAttack playerAttack;

    [Range(0, 360)] public float angle;
    [Range(0, 360)] public float arcPoint1;
    [Range(0, 360)] public float arcPoint2;

    private void Awake()
    {
        myMaterial = GetComponent<SpriteRenderer>().material;        
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void Update()
    {
        myMaterial.SetFloat("_Angle", angle);
        myMaterial.SetFloat("_Arc1", arcPoint1);
        myMaterial.SetFloat("_Arc2", arcPoint2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.name == "Dragon_Boss")
            {
                print("asd");
            }
            playerAttack.enemyList.Add(collision.gameObject);
            Debug.Log("ÀûÃß°¡");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.name == "Dragon_Boss")
            {
                print("°ö»©±â");
            }
            playerAttack.enemyList.Remove(collision.gameObject);
            Debug.Log("Àû•û±â");
        }
    }

}
