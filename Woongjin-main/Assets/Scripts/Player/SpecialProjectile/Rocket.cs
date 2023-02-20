using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float setupTime;
    [SerializeField] float randomWeight;
    [SerializeField] float turnSpeed;

    GameObject myEnemy;
    bool isSetup;

    private void Awake()
    {
        Invoke("EnemySearch", setupTime + Random.Range(-randomWeight, randomWeight));
    }

    private bool EnemySearch()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyList == null || enemyList.Length == 0)
        {
            Debug.Log("³Î");
            return false;
        }

        for(int i=0; i<enemyList.Length; i++)
        {
            if (i == 0)
            {
                myEnemy = enemyList[i];
                GetComponent<PlayerProjectile>().target = myEnemy.transform;
            }
            else if (Vector3.Distance(enemyList[i].transform.position, transform.position) <
                    Vector3.Distance(enemyList[i - 1].transform.position, transform.position))
            {
                myEnemy = enemyList[i];
                GetComponent<PlayerProjectile>().target = myEnemy.transform;
            }
        }

        isSetup = true;
        return true;
    }

    void Update()
    {
        if(isSetup)
        {
            if (myEnemy == null)
            {
                if (EnemySearch() == false) SelfDestroy();
                return;
            }
            Vector3 goalDir = myEnemy.transform.position - transform.position;

            float deg = Mathf.Atan2(goalDir.y, goalDir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(deg, Vector3.forward), turnSpeed * Time.deltaTime);
        }
        
    }

    void SelfDestroy()
    {
        GetComponent<PlayerProjectile>().Hit();
    }
}
