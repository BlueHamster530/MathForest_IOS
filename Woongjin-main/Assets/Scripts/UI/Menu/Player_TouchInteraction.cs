using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Spine;
using Spine.Unity;

public class Player_TouchInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string[] animationDataReaction;
    [SerializeField] string[] animationDataIdle;

    [SerializeField] SkeletonAnimation anims;
    Spine.AnimationState state;
    bool isPlaying;

    float lastTime;

    private void Awake()
    {
        state = anims.AnimationState;
    }
    private void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Time.time  > lastTime + 0.5f)
        {
            SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.click1);
            StartCoroutine("GoAnimation");
        }      
    }

    private void GoAnimation()
    {
        lastTime = Time.time;
        int a = Random.Range(0, animationDataReaction.Length);

        anims.ClearState();
        anims.skeleton.SetSlotsToSetupPose();

        state.AddAnimation(0, animationDataReaction[a], true, 0);
        state.AddAnimation(0, animationDataIdle[a], true, 0.25f);
    }

    private void EndAnimationPlay()
    {

    }
}
