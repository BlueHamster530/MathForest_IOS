using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class CostumeApplier : MonoBehaviour
{
    [SerializeField] SkeletonAnimation anim;
    PlayerAttack IsInGamePlay;
    private void Start()
    {
        anim.AnimationName = "Idle";
        SkinApply();
    }
    private void OnEnable()
    {
        anim.AnimationName = "Idle";
        SkinApply();
    }

    private void SkinApply()
    {
        if (anim != null && PlayerPrefs.HasKey("PlayerSkin"))
        {
            anim.skeleton.SetSkin(PlayerPrefs.GetString("PlayerSkin"));
            anim.skeleton.SetSlotsToSetupPose();
            TryGetComponent<PlayerAttack>(out IsInGamePlay);
            if (IsInGamePlay != null)
            {
                if (PlayerPrefs.GetInt("Challenge_Faker") == 0)
                {
                    PlayerPrefs.SetInt("Challenge_Faker", 1);

                    if (ChallengeManager.instance != null)
                    {
                        ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Faker);
                    }
                }
            }
        }
        else
        {
            anim.skeleton.SetSkin("basic");
            anim.skeleton.SetSlotsToSetupPose();

        }
            
    }
}
