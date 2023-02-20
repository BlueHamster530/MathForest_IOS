using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IH_DNAnswerImageAnimation : MonoBehaviour
{
    Animator anim;
    private void OnEnable()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetTrigger("AnimStart");
    }
    // Update is called once per frame
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
