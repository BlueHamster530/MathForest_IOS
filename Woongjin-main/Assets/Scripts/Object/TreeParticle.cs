using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class TreeParticle : MonoBehaviour
{
    [SerializeField] GameObject[] myParticle;
    [SerializeField] Transform particlePos;

    public void CreateParticle(int a)
    {
        GameObject myPat = Instantiate(myParticle[a], particlePos.position, Quaternion.identity);
    }
}
