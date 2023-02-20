using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class ss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<BoneFollower>().boneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
