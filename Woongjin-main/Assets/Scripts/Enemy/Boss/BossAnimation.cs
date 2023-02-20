using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class BossAnimation : MonoBehaviour
{
    SkeletonAnimation charAnim;
    BossController Controller;
    bool bIsDeadTimeSlowEnd;
    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponent<SkeletonAnimation>();
        charAnim.AnimationState.Event += AnimationState_Event;
        bIsDeadTimeSlowEnd = false;
    }

    private void Update()
    {
        if (Controller.bIsDead == true)
        {
            if (bIsDeadTimeSlowEnd == false)
            {
                if (Time.timeScale > 0.3f)
                {
                    Time.timeScale -= Time.deltaTime * 0.5f;
                }
            }
            else
            {
                if (Time.timeScale < 1.0f)
                {
                    Time.timeScale += Time.deltaTime * 0.5f;
                    if (Time.timeScale >= 1.0f)
                    {
                        Time.timeScale = 1.0f;
                    }
                }
                else
                {
                    Time.timeScale = 1.0f;
                }
            }
        }
    }
    public void Init(BossController bossController)
    {
        Controller = bossController;
    }
    private void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        if (charAnim.AnimationName == "enemy_dragon_die")
        {
            if (e.Data.Name == "end")
            {
                bIsDeadTimeSlowEnd = true;

            }
        }
        else if (charAnim.AnimationName.Substring(0, 19) == "enemy_dragon_attack")
        {
            if (e.Data.Name == "attack")
            {
                Controller.GetBossAttack().AttackEvent();
            }
            else if (e.Data.Name == "end")
            {
                ChangeAnim("enemy_dragon_idle", true);
                Controller.bIsAttacking = false;
            }
        }
    }
    public void StartDeadAnim()
    {
        ChangeAnim("enemy_dragon_die",false);
    }
    private void ChangeAnim(string _name, bool isRoop)
    {
        if (charAnim.AnimationName != _name)
            charAnim.AnimationState.SetAnimation(0, _name, isRoop);
    }
    public void StartAttackAnim(int _index)
    {
        string AttackAnimName = "enemy_dragon_attack" + (_index + 1).ToString();
        ChangeAnim(AttackAnimName, false);
    }
}
