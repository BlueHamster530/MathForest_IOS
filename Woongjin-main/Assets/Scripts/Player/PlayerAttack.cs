using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] StageManager   cStageManager;
    [SerializeField] VirtualPad     cMyPad;
    LineRenderer                    cLineRenderer;
    SpriteRenderer                  cSpriteRenderer;
    PlayerAim                       cPlayerAim;
    PlayerStatus charStatus;

    pAttack_functions               cAttackFunctions;
    //Current Equipped Weapon

    [SerializeField] [Range(0,10f)] float fRange;

    public List<GameObject> enemyList = new List<GameObject> ();

    public WeaponTemplate cCurrentWeapon;
    [Header("Current Weapon Status")]
    public WeaponPrefab   gCurrentWeaponPrefab;

    public int    nDamage;
    public int    nMagazine;
    public float  fDelay;
    public bool   bIsWeaponReady;
    public int    nWeaponLevel;

    [SerializeField] Transform      weaponParent;
    [SerializeField] Vector3        weaponPositionOffset;
    bool isEnemySetted;
    [SerializeField] Transform currentEnemy;

    [SerializeField] Vector3 aimDir = Vector3.right;
    public Vector3 AimDir => aimDir;

    [SerializeField] Vector3 lookVector;
    int lookDir = 1;
    public float weaponLookValue;
    public float angle;

    private void Start()
    {
        cSpriteRenderer         = GetComponent<SpriteRenderer>();
        cPlayerAim              = GetComponent<PlayerAim>();   
        cAttackFunctions        = GetComponent<pAttack_functions>();
        charStatus = GetComponent<PlayerStatus>();

        if (cStageManager == null)
        {
            cStageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
            cMyPad = cStageManager.gPadAim;
        }
        else cMyPad = cStageManager.gPadAim;

        lookVector = transform.localScale;

        WeaponSetup(101);
    }

    public void WeaponSetup(int code)
    {
        if (gCurrentWeaponPrefab != null) Destroy(gCurrentWeaponPrefab.gameObject);

        cCurrentWeapon = Resources.Load($"WeaponData/{code}") as WeaponTemplate;

        if (code != 101 && SceneManager.GetActiveScene().name != "Tutorial")
            ChallengeManager.instance.bIsPlayerWeaponChanged = true;

        nWeaponLevel = PlayData.instance.weaponLevelData[code];
        nMagazine = cCurrentWeapon.nWeaponMagazine;
        fDelay = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fWeaponDelay;

        Debug.Log(nWeaponLevel);
        gCurrentWeaponPrefab =  GameObject.Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gWeaponPrefab, 
                                weaponParent.position + weaponPositionOffset, Quaternion.identity).
                                GetComponent<WeaponPrefab>();
        
        gCurrentWeaponPrefab.transform.localScale = transform.localScale;
        gCurrentWeaponPrefab.transform.position = weaponParent.position + weaponPositionOffset;
        gCurrentWeaponPrefab.transform.SetParent(weaponParent);        

        float weaponLookValue = (aimDir.y / 2) + 0.5f;
        gCurrentWeaponPrefab.GetComponent<Animator>().SetFloat("Angle", weaponLookValue);

        cPlayerAim.angleRange = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fAimDegree;
        cPlayerAim.radius = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fAimDistance;
        UpdateWeaponUI();
    }

    public void WeaponSetupWithoutMagazine(int code)
    {
        if (gCurrentWeaponPrefab != null) Destroy(gCurrentWeaponPrefab.gameObject);

        cCurrentWeapon = Resources.Load($"WeaponData/{code}") as WeaponTemplate;

        nWeaponLevel = PlayData.instance.weaponLevelData[code];
        fDelay = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fWeaponDelay;

        gCurrentWeaponPrefab = GameObject.Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gWeaponPrefab,
                                weaponParent.position + weaponPositionOffset, Quaternion.identity).
                                GetComponent<WeaponPrefab>();

        gCurrentWeaponPrefab.transform.localScale = transform.localScale;
        gCurrentWeaponPrefab.transform.position = weaponParent.position + weaponPositionOffset;
        gCurrentWeaponPrefab.transform.SetParent(weaponParent);

        float weaponLookValue = (aimDir.y / 2) + 0.5f;
        gCurrentWeaponPrefab.GetComponent<Animator>().SetFloat("Angle", weaponLookValue);

        cPlayerAim.angleRange = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fAimDegree;
        cPlayerAim.radius = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fAimDistance;
        UpdateWeaponUI();
    }

    public void WeaponRenew()
    {
        if (cCurrentWeapon == null) return;
        nWeaponLevel = PlayData.instance.weaponLevelData[cCurrentWeapon.nWeaponCode];

        UpdateWeaponUI();
    }

    private void Update()
    {
        if (charStatus != null && charStatus.isDeath) return;
        AttackCheck();
    }

    private void FixedUpdate()
    {
        if (charStatus != null && charStatus.isDeath) return;
        AimManage();
    }

    private void AttackCheck()
    {
        if (cCurrentWeapon == null) return;

        if (fDelay > 0)
        {
            fDelay -= Time.deltaTime;
            bIsWeaponReady = false;
        }
        else bIsWeaponReady = true;

        if(bIsWeaponReady && enemyList.Count >=1)
        {
            for(int i=0; i<enemyList.Count; i++)
            {
                if (enemyList[i] == null) continue;

                if (cPlayerAim.EnemyChecker(enemyList[i].transform))
                {
                    SetEnemy(true, enemyList[i].transform);

                    AttackStart(enemyList[i].transform);
                    return;
                }
            }
            SetEnemy(false, null);
            cAttackFunctions.ExitAttack();
        }
    }

    private void SetEnemy(bool a, Transform enemy)
    {
        isEnemySetted = a;
        currentEnemy = enemy;
    }

    private void AttackStart(Transform target)
    {
        cAttackFunctions.AttackByFunction(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].sAttackMethod, target);
        if(ChallengeManager.instance != null) ChallengeManager.instance.bIsPlayerAttacked = true;
        ConsumeAmmo();
        UpdateWeaponUI();
    }

    private void ConsumeAmmo()
    {
        if (nMagazine > 0) nMagazine--;
        else if(cCurrentWeapon.nWeaponCode != 101) WeaponSetup(101);
    }

    private void UpdateWeaponUI()
    {
        cStageManager.UpdateDisplayWeapon(cCurrentWeapon.sWeaponName, nMagazine, nWeaponLevel);
    }

    private void AimManage()
    {
        angle = 0;
        if (cMyPad == null) return;

        if (cMyPad.vPadOutput != Vector2.zero && (Vector3)cMyPad.vPadOutput != aimDir)
        {
            aimDir = new Vector3(cMyPad.vPadOutput.normalized.x, cMyPad.vPadOutput.normalized.y, 0);
            lookDir = cMyPad.vPadOutput.x > 0 ? 1 : -1;
        }

        if (gCurrentWeaponPrefab != null && currentEnemy != null && cPlayerAim.IsCollision && !gCurrentWeaponPrefab.isDirectionFix)
        {
            if(transform.localScale.x > 0)
            {
                Vector3 direction = (currentEnemy.transform.position + new Vector3(0, 0.5f, 0)) - gCurrentWeaponPrefab.ShootPos.position;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                Vector3 direction = gCurrentWeaponPrefab.ShootPos.position - (currentEnemy.transform.position + new Vector3(0, 0.5f, 0));
                angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            
            weaponLookValue = Mathf.Lerp(weaponLookValue, (((angle / 180f) + 0.5f)), Time.deltaTime * 20f);

            gCurrentWeaponPrefab.GetComponent<Animator>().SetFloat("Angle", weaponLookValue);
        }
        else
        {
            weaponLookValue = Mathf.Lerp(weaponLookValue, (aimDir.y / 2) + 0.5f, Time.deltaTime * 20f);
            gCurrentWeaponPrefab.GetComponent<Animator>().SetFloat("Angle", weaponLookValue);
        }

        if (lookVector != Vector3.zero)
        {
            transform.localScale = lookDir >= 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }
}
