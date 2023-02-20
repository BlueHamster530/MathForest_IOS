using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Common = 0, Rocket }

public class PlayerProjectile : MonoBehaviour
{
    public Transform target;
    Vector3 goalDir;
    Vector3 goDir;

    public Transform parent;

    [SerializeField] ProjectileType myType;
    [SerializeField] float speed;
    [SerializeField] bool isFierce;

    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject hitSound;

    bool isReturnMode;
    bool isSetup;
    float deg;

    private void Update()
    {
        Move();

        //if (isSetup && target == null) Destroy(gameObject);
    }
    public void Setup(Transform target)
    {       
        this.target = target;

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        goalDir = target.position + new Vector3(0,0.5f) - transform.position;


        deg = Mathf.Atan2(goalDir.y, goalDir.x);

        transform.rotation = Quaternion.Euler(0, 0, deg * Mathf.Rad2Deg);

        goDir.x = Mathf.Cos(deg);
        goDir.y = Mathf.Sin(deg);

        SetupByType();
    }

    private void SetupByType()
    {
        switch (myType)
        {
            case ProjectileType.Common:
                isSetup = true;
                break;
            case ProjectileType.Rocket:
                RandomDirectionSet();
                isSetup = true;
                break;
        }

    }

    private void RandomDirectionSet()
    {
        transform.Rotate(0, 0, Random.Range(-20f, 20f));
    }

    public void Turn()
    {
        goDir *= -1;
    }

    public void FollowPlayer()
    {
        isReturnMode = true;
    }

    private void Move()
    {
        if (isReturnMode)
        {
            transform.position = Vector3.MoveTowards(transform.position, parent.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, parent.position) < 0.2f) Destroy(gameObject);
        }
        else if (isSetup)
        {
            switch (myType)
            {
                case ProjectileType.Common:
                    transform.position += new Vector3(goDir.x * speed, goDir.y * speed, 0) * Time.deltaTime;
                    break;

                case ProjectileType.Rocket:
                    transform.position += transform.right * speed * Time.deltaTime;
                    break;

            }
        }
    }

    private void OnBecameInvisible()
    {
        if(isFierce == false)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Hit();
        }
    }

    public void Hit()
    {
        if (hitEffect != null)
        {
            Vector3 newVec = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            GameObject he = Instantiate(hitEffect, transform.position + newVec, Quaternion.identity);
            GameObject hs = Instantiate(hitSound, transform.position, Quaternion.identity);
        }

        if (!isFierce) Destroy(gameObject);
    }
}
