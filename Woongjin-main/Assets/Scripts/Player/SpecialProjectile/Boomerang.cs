using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float turnDistance;

    PlayerProjectile pProjectile;

    Vector3 startLocation;
    bool isTurned;

    void Start()
    {
        pProjectile = GetComponent<PlayerProjectile>();

        startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if(!isTurned)
        {
            if (Vector3.Distance(startLocation, transform.position) >= turnDistance)
            {
                isTurned = true;
                pProjectile.FollowPlayer();
            }
        }
        else
        {
            if (Vector3.Distance(startLocation, transform.position) <= 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
