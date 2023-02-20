using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile_VFXOnly : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Hit(collision);
        }
    }

    public void Hit(Collider2D collision)
    {
        if (hitEffect != null)
        {
            Vector3 newVec = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            GameObject he = Instantiate(hitEffect, collision.transform.position + newVec, Quaternion.identity);
            GameObject hs = Instantiate(hitSound, Vector3.zero, Quaternion.identity);
        }
    }
}
