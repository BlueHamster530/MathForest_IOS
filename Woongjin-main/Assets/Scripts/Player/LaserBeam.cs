using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    PlayerAttack pAttack;
    Transform target;
    bool isSetup;

    float angle;

    public void Setup(PlayerAttack pAttack)
    {
        this.pAttack = pAttack;
        isSetup = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSetup)
        {
            float angleGo = pAttack.transform.localScale.x == 1 ?
                Mathf.Lerp(-90, 90, pAttack.weaponLookValue) :
                Mathf.Lerp(-90, -270, pAttack.weaponLookValue);

            transform.rotation = Quaternion.Euler(0,0,angleGo);
            transform.localScale = pAttack.transform.localScale;
        }
    }
}
