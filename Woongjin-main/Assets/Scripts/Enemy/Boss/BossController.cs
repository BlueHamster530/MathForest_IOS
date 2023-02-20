using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField]
    EnemyInfo eInfo;
    [SerializeField]
    private GameObject PlayerObject;

    [SerializeField]
    EnemyManger enemyspawner;
    [SerializeField]
    [Header("공격쿨타임")]
    float fAttackCoolTime = 4.0f;
    float fCurrentAttckCoolTime;

    [Header("Prefabs")]
    [SerializeField] GameObject damageText;
    [SerializeField] Slider bossHPBar;

    private BossAnimation anim;
    private BossAttack bossAttack;
    private Slider myHPBar;

    public bool bIsAttacking { get; set; }
    public bool bIsDead { get; set; }
    int nCurrentHp;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<BossAnimation>();
        bossAttack = GetComponent<BossAttack>();
        Init();
        anim.Init(this);
        bossAttack.Init(this);

        CreateHpSlider();
    }
    public BossAnimation GetBossAnim()
    {
        return anim;
    }
    public BossAttack GetBossAttack()
    {
        return bossAttack;
    }
    public EnemyManger GetEnemyManger()
    {
        return enemyspawner;
    }
    public GameObject GetPlayerObject()
    {
        return PlayerObject;
    }
    public EnemyInfo GetInfo()
    {
        return eInfo;
    }
    private void Init()
    {
        fCurrentAttckCoolTime = fAttackCoolTime;
        bIsAttacking = false;
        nCurrentHp = eInfo.nHp;
        bIsDead = false;
    }

    public void CreateHpSlider()
    {
        myHPBar = Instantiate(bossHPBar, transform.position, Quaternion.identity);

        myHPBar.transform.SetParent(GameObject.Find("Canvas").transform);

        myHPBar.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    private void HitTrigger(ProjectileStatus _projectileoinfo = null)
    {
        if (_projectileoinfo == null)
            print("총알정보오류:정보없음");
        else
        {
            if (_projectileoinfo.IsFierce == false)
            {
                Destroy(_projectileoinfo.gameObject);
            }
            HitDamage(_projectileoinfo.Damage);
        }
    }
    public void HitDamage(int _Damage)
    {
        if (bIsDead == true) return;
        nCurrentHp -= _Damage;

        DamageTextEffect clone = Instantiate(damageText, transform.position + new Vector3(0.0f, 0.2f, 0.0f), Quaternion.identity).GetComponent<DamageTextEffect>();
        clone.Init(_Damage, false);
        //print(_Damage);
        if (nCurrentHp <= 0)
        {

            if (PlayerPrefs.GetInt("Challenge_Dragonslayer") == 0)
            {
                PlayerPrefs.SetInt("Challenge_Dragonslayer", 1);

                if (ChallengeManager.instance != null)
                {
                    ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Dragonslayer);
                }
            }
            ChallengeManager.instance.PlayerBasicWeaponClearCheck();
            nCurrentHp = 0;
            anim.StartDeadAnim();
            bIsDead = true;
            if (WaveManager.instance != null) WaveManager.instance.huntCount++;

            WaveManager.instance.ClearAllEnemy();

            StartCoroutine("GameEnd");
        }
    }

    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(2f);
        WaveManager.instance.GameSet(1);
        yield return null;
    }

    private void AttackFunction()
    {
        if (bIsDead == true || bIsAttacking == true) return;
        fCurrentAttckCoolTime -= Time.deltaTime;
        if (fCurrentAttckCoolTime <= 0)
        {
            fCurrentAttckCoolTime = fAttackCoolTime;
            bIsAttacking = true;
            bossAttack.AttackStartTrigger();
        }
    }

    // Update is called once per frame
    void Update()
    {
        AttackFunction();
        SliderManage();
    }

    private void SliderManage()
    {
        if(myHPBar != null)
        {
            myHPBar.value = (float)nCurrentHp / (float)eInfo.nHp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bIsDead == false)
        {
            if (collision.CompareTag("WeaponHit"))
            {
                HitTrigger(collision.GetComponent<ProjectileStatus>());
            }
        }
    }
}
