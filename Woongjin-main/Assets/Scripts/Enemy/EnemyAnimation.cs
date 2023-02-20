using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class EnemyAnimation : MonoBehaviour
{
    EnemyController Controller;

    SkeletonAnimation charAnim;

    bool IsStartHitAnim = false;


    #region 히트이팩트용 변수들
    public float HitEffectSpeed = 5.0f;
    private float HitEffect_HitColor = 0.745f;
    private float HitEffect_time = 0.0f;
    private float HitEffect_originxSize;
    private float HitEffect_startxSize;
    private float HitEffect_currentxSize;
    private MeshRenderer renderer;
    private MaterialPropertyBlock block;
    private int tintid;
    #endregion



    private void Awake()
    {
        Init(GetComponent<EnemyController>());
    }
    public void Init(EnemyController _Controller)
    {
        Controller = _Controller;

        charAnim = GetComponent<SkeletonAnimation>();
        renderer = GetComponent<MeshRenderer>();
        block = new MaterialPropertyBlock();
        renderer.SetPropertyBlock(block);
        tintid = Shader.PropertyToID("_Black");
        block.SetColor(tintid, Color.black);
        renderer.SetPropertyBlock(block);

        IsStartHitAnim = false;

        //print("enemy_" + this.transform.name);
        //ChangeAnim("enemy_" + this.transform.name, true);
    }
    public bool GetIsStartHitAnim()
    {
        return IsStartHitAnim;
    }
    public void HitAnimStart()
    {
        if (IsStartHitAnim == false)
        {
            print("HitEffect");
            HitEffect_HitColor = 0.745f;
            HitEffect_time = 0.0f;
            transform.localScale = Controller.GetOriginScale();
            HitEffect_originxSize = transform.localScale.x;
            HitEffect_startxSize = transform.localScale.x * 0.75f;
            HitEffect_currentxSize = HitEffect_startxSize;

            block.SetColor(tintid, Color.black);
            renderer.SetPropertyBlock(block);

            IsStartHitAnim = true;
        }
    }
    private void ChangeAnim(string _name, bool isRoop)
    {
        if (charAnim.AnimationName != _name)
            charAnim.AnimationState.SetAnimation(0, _name, isRoop);
    }
    private void HitEffect()
    {
        if (IsStartHitAnim == false) return;

        HitEffect_time += Time.deltaTime * HitEffectSpeed;
        HitEffect_HitColor = Mathf.Lerp(0.745f, 0.0f, HitEffect_time);
        HitEffect_currentxSize = Mathf.Lerp(HitEffect_startxSize, HitEffect_originxSize, HitEffect_time);

        Color HitColor = Color.Lerp(new Color(HitEffect_HitColor, HitEffect_HitColor, HitEffect_HitColor,1), Color.black, HitEffect_time);
        block.SetColor(tintid, HitColor);
        renderer.SetPropertyBlock(block);

        transform.localScale = new Vector3(HitEffect_currentxSize, HitEffect_currentxSize, transform.localScale.z);

        if (HitEffect_time >= 1.0f)
        {
            HitEffect_time = 0;
            block.SetColor(tintid, Color.black);
            renderer.SetPropertyBlock(block);
            transform.localScale = new Vector3(HitEffect_originxSize, HitEffect_originxSize, transform.localScale.z);
            IsStartHitAnim = false;
        }
    }
    private void Update()
    {
        if (Controller.GetIsDead() == false)
        {
            HitEffect();
        }
    }
    public void IsDeadTrigger()
    {
        transform.localScale = Controller.GetOriginScale();
        //material.SetColor("_Tint", new Color(1.0f, 1.0f, 1.0f, 0));
    }

}
