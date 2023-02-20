using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public class PlayerStatus : MonoBehaviour
{
    PlayerAnimation charAnim;

    public int maxHp;
    public int hp;

    [SerializeField] bool isInvincible;
    public bool isSuperInvincible;
    public bool isDeath;
    public bool isDamaging;
    public bool isShield;
    //애니메이션 전환용 대미지 변수

    SliderStatus mySlider;
    [Header("Prefabs")]
    //[SerializeField] SliderStatus sliderHP;
    [SerializeField] GameObject sliderHP;

    [Header("Damage Related")]
    [SerializeField] GameObject pBody;

    bool IsStartHitAnim = false;

    bool bIsMinoBurn = false;

    #region 히트이팩트용 변수들
    public float HitEffectSpeed = 5.0f;
    private float HitEffect_HitColor = 0.745f;
    private float HitEffect_time = 0.0f;
    private MeshRenderer renderer;
    private MaterialPropertyBlock block;
    private int tintid;
    #endregion

    private void Awake()
    {
        charAnim = GetComponent<PlayerAnimation>();
        renderer = pBody.GetComponent<MeshRenderer>();
        block = new MaterialPropertyBlock();
        renderer.SetPropertyBlock(block);
        tintid = Shader.PropertyToID("_Black");
        block.SetColor(tintid, Color.black);
        renderer.SetPropertyBlock(block);
        IsStartHitAnim = false;
        bIsMinoBurn = false;
        sliderHP.GetComponent<SliderStatus>().Setup(this);
        //if (mySlider == null)
        //{
        //    mySlider = Instantiate(sliderHP, transform.position, Quaternion.identity);
        //    mySlider.Setup(this);
        //
        //    mySlider.transform.SetParent(GameObject.Find("Canvas_Back").transform);
        //
        //    mySlider.GetComponent<RectTransform>().localScale = Vector3.one;
        //}
    }

    private void OnEnable()
    {
        if (mySlider != null) mySlider.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!isDeath && hp <= 0)
        {
            Death();
        }
        if (isDeath == false && hp > 0)
        {
            HitEffect();
        }
    }

    public void Damage(int a,string _DamageType = "Null")
    {
        if (isInvincible || isSuperInvincible || isShield) return;

        isDamaging = true;
        charAnim.ChangeAnim("shot", false);

        hp -= a;

        if (hp <= 0)
        {
            if (PlayerPrefs.GetInt("Challenge_Firepunch") == 0)
            { 
               if (_DamageType == "FireBall")
                 {
                    PlayerPrefs.SetInt("Challenge_Firepunch", 1);

                    if (ChallengeManager.instance != null)
                    {
                        ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Firepunch);
                    }
                }
            }
            Death();
        }
        else
        {
            HitAnimStart();
            StopCoroutine("InvincibleGo");
            StartCoroutine("InvincibleGo");
        }
    }

    private IEnumerator InvincibleGo()
    {
        
        isInvincible = true;

        yield return new WaitForSeconds(0.2f);

        isInvincible = false;
        isDamaging = false;
    }

    private void Death()
    {
        isDeath = true;
        charAnim.Death();

        if (PlayerPrefs.GetInt("Challenge_Nicetry") == 0)
        {
            PlayerPrefs.SetInt("Challenge_Nicetry", 1);

            if (ChallengeManager.instance != null)
            {
                ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Nicetry);
            }
        }
        WaveManager.instance.GameSet(0);
    }
    public void HitAnimStart()
    {
        if (IsStartHitAnim == false)
        {
            HitEffect_HitColor = 0.745f;
            HitEffect_time = 0.0f;

            block.SetColor(tintid, Color.black);
            renderer.SetPropertyBlock(block);

            IsStartHitAnim = true;
        }
    }
    private void HitEffect()
    {
        if (IsStartHitAnim == false) return;

        HitEffect_time += Time.deltaTime * HitEffectSpeed;
        HitEffect_HitColor = Mathf.Lerp(0.745f, 0.0f, HitEffect_time);

        Color HitColor = Color.Lerp(new Color(HitEffect_HitColor, HitEffect_HitColor, HitEffect_HitColor, 1), Color.black, HitEffect_time);
        block.SetColor(tintid, HitColor);
        renderer.SetPropertyBlock(block);


        if (HitEffect_time >= 1.0f)
        {
            HitEffect_time = 0;
            block.SetColor(tintid, Color.black);
            renderer.SetPropertyBlock(block);
            IsStartHitAnim = false;
        }
    }

}
