using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerAnimation : MonoBehaviour
{
    SkeletonAnimation   charAnim;
    PlayerMove          charMove;
    PlayerStatus        charStatus;

    [SerializeField] ExposedList<Bone> bone;


    float eyeBlinkCount;

    private void Awake()
    {
        charAnim = GetComponentInChildren<SkeletonAnimation>();
        charMove = GetComponent<PlayerMove>();
        charStatus = GetComponent<PlayerStatus>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (charStatus != null && (charStatus.isDeath || charStatus.isDamaging)) return;

        if (charMove.FPadInput != Vector2.zero)
        {
            ChangeAnim("walk", true);
        }
        else ChangeAnim("idle", true);
    }

    public void ChangeAnim(string _name, bool isRoop)
    {
        if(charAnim.AnimationName != _name)
            charAnim.AnimationState.SetAnimation(0, _name, isRoop);
    }

    public void Attack()
    {
        
    }

    public void Death()
    {
        ChangeAnim("die", false);
    }

}
