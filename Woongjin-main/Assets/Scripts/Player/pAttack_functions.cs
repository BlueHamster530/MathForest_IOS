using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pAttack_functions : MonoBehaviour
{
    PlayerAttack playerAttack;
    WeaponFireEffect ShootEffect;
    WeaponFireEffect BackEffect;
    WeaponTemplate cCurrentWeapon;
    int code;

    GameObject currentBullet;
    GameObject currentSound;
    bool isLaserShooting;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void AttackByFunction(string arg, Transform target)
    {
        cCurrentWeapon = playerAttack.cCurrentWeapon;
        code = cCurrentWeapon.nWeaponCode;
        
        StartCoroutine(arg, target);
    }
    private void ViveratorActive()
    {

#if UNITY_ANDROID
        if (PlayerPrefs.GetInt("setting_vibration") == 1)
            Vibration.Vibrate(250);
#endif
    }
    public void ExitAttack()
    {
        if (currentBullet == null) return;
        else
        {
            Animator anim;
            ParticleSystem part;

            if (currentBullet.TryGetComponent<Animator>(out anim)) anim.SetTrigger("exit");
            else if (currentBullet.TryGetComponent<ParticleSystem>(out part)) part.Stop();
        }

        if (currentSound != null) Destroy(currentSound);
    }

    private IEnumerator LaserAttack(Transform target)
    {
        int nWeaponLevel = PlayData.instance.weaponLevelData[code];
        WeaponPrefab gCurrentWeaponPrefab = playerAttack.gCurrentWeaponPrefab;

        playerAttack.fDelay = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fWeaponDelay;

        if (currentBullet == null)
        {
            GameObject myProjectile = Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gBulletPrefab, gCurrentWeaponPrefab.ShootPos.position, Quaternion.identity);
            myProjectile.GetComponent<ProjectileStatus>().Setup(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nWeaponDamage);

            myProjectile.transform.SetParent(gCurrentWeaponPrefab.ShootPos);

            myProjectile.transform.localScale = Vector3.one * cCurrentWeapon.optionByLevel[nWeaponLevel - 1].sizeDelta;
            

            currentBullet = myProjectile;
            myProjectile.GetComponent<LaserBeam>().Setup(playerAttack);
        }
        else
        {
            currentBullet.GetComponent<Collider2D>().enabled = false;
            currentBullet.GetComponent<Collider2D>().enabled = true;
        }        

        if (currentSound == null)
        {
            currentSound = Instantiate(cCurrentWeapon.shotSound, transform.position, Quaternion.identity);
            currentSound.transform.SetParent(gCurrentWeaponPrefab.transform);
        }

        yield return null;
    }

    private IEnumerator ShootProjectile(Transform target)
    {        
        int nWeaponLevel = PlayData.instance.weaponLevelData[code];
        WeaponPrefab gCurrentWeaponPrefab = playerAttack.gCurrentWeaponPrefab;

        for (int i = 0; i < cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nThrowCount; i++)
        {
            if (target != null)
            {
                playerAttack.fDelay = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fWeaponDelay;

                PlayerProjectile myProjectile = Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gBulletPrefab, gCurrentWeaponPrefab.ShootPos.position, Quaternion.identity).GetComponent<PlayerProjectile>();

                myProjectile.parent = transform;

                if (ShootEffect == null && cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gFireEffect != null)
                {
                    ShootEffect = GameObject.Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gFireEffect, gCurrentWeaponPrefab.ShootPos.position, Quaternion.identity).GetComponent<WeaponFireEffect>();
                    ShootEffect.transform.SetParent(transform);

                    //ShootEffect.transform.rotation = Quaternion.Euler(0, 0, (weaponLookValue * 180f) - 90);
                    ShootEffect.Setup(target);
                }

                myProjectile.Setup(target);
                myProjectile.GetComponent<ProjectileStatus>().Setup(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nWeaponDamage);

                GameObject wSound = Instantiate(cCurrentWeapon.shotSound, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.07f);
        }
    }

    private IEnumerator ShootImmediate(Transform target)
    {
        int nWeaponLevel = PlayData.instance.weaponLevelData[code];
        WeaponPrefab gCurrentWeaponPrefab = playerAttack.gCurrentWeaponPrefab;

        for (int i = 0; i < cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nThrowCount; i++)
        {
            if(target != null)
            {
                playerAttack.fDelay = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fWeaponDelay;

                GameObject myProjectile = Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gBulletPrefab, gCurrentWeaponPrefab.ShootPos.position, Quaternion.identity);

                Vector3 goalDir = target.position + new Vector3(0, 0.5f) - transform.position;
                float deg = Mathf.Atan2(goalDir.y, goalDir.x);

                myProjectile.transform.rotation = Quaternion.Euler(0, 0, deg * Mathf.Rad2Deg);

                if (ShootEffect == null && cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gFireEffect != null)
                {
                    ShootEffect = GameObject.Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gFireEffect, gCurrentWeaponPrefab.ShootPos.position, Quaternion.identity).GetComponent<WeaponFireEffect>();
                    //ShootEffect.transform.SetParent(transform);

                    ShootEffect.Setup(target);
                }

                //myProjectile.Setup(target);
                myProjectile.GetComponent<ProjectileStatus>().Setup(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nWeaponDamage);
                ViveratorActive();
                GameObject wSound = Instantiate(cCurrentWeapon.shotSound, transform.position, Quaternion.identity);
            }
            


            yield return new WaitForSeconds(0.07f);
        }
    }

    private IEnumerator ShootRocket(Transform target)
    {
        int nWeaponLevel = PlayData.instance.weaponLevelData[code];
        WeaponPrefab gCurrentWeaponPrefab = playerAttack.gCurrentWeaponPrefab;

        for (int i = 0; i < cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nThrowCount; i++)
        {
            playerAttack.fDelay = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fWeaponDelay;

            PlayerProjectile myProjectile = Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gBulletPrefab, gCurrentWeaponPrefab.ShootPos.position, Quaternion.identity).GetComponent<PlayerProjectile>();
            myProjectile.parent = transform;

            if (ShootEffect == null && cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gFireEffect != null)
            {
                ShootEffect = GameObject.Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gFireEffect, gCurrentWeaponPrefab.ShootPos.position, Quaternion.identity).GetComponent<WeaponFireEffect>();
                ShootEffect.transform.SetParent(transform);

                float angleGo = playerAttack.transform.localScale.x == 1 ?
                Mathf.Lerp(-90, 90, playerAttack.weaponLookValue) :
                Mathf.Lerp(-90, -270, playerAttack.weaponLookValue);

                ShootEffect.transform.rotation = Quaternion.Euler(0, 0, angleGo);
            }

            if (BackEffect == null && cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gBackEffect != null)
            {
                BackEffect = GameObject.Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gBackEffect, gCurrentWeaponPrefab.BackPos.position, Quaternion.identity).GetComponent<WeaponFireEffect>();
                BackEffect.transform.SetParent(gCurrentWeaponPrefab.BackPos.transform);

                //BackEffect.transform.rotation = Quaternion.Euler(0, 0, transform.parent.rotation.z);

                float angleGo = playerAttack.transform.localScale.x == 1 ?
                Mathf.Lerp(-90, 90, playerAttack.weaponLookValue) :
                Mathf.Lerp(-90, -270, playerAttack.weaponLookValue);

                BackEffect.transform.rotation = Quaternion.Euler(0, 0, angleGo);
                BackEffect.transform.localScale = playerAttack.transform.localScale * 0.6f;
            }

            myProjectile.Setup(target);
            myProjectile.GetComponent<ProjectileStatus>().Setup(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nWeaponDamage);
            
            GameObject wSound = Instantiate(cCurrentWeapon.shotSound, transform.position, Quaternion.identity);


            yield return new WaitForSeconds(0.07f);
        }
    }

    private IEnumerator OrbitalSaw(Transform target)
    {
        int nWeaponLevel = PlayData.instance.weaponLevelData[code];
        WeaponPrefab gCurrentWeaponPrefab = playerAttack.gCurrentWeaponPrefab;

        playerAttack.fDelay = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fWeaponDelay;

        for (int i = 0; i < cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nThrowCount; i++)
        {
            GameObject mS = Instantiate(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].gBulletPrefab, transform.position, Quaternion.identity);

            mS.GetComponent<Saw>().Setup(transform);
            mS.GetComponent<Saw>().range = cCurrentWeapon.optionByLevel[nWeaponLevel - 1].fAimDistance + 2;
            //mS.transform.parent = transform;
            mS.GetComponent<Saw>().theta = i * (360 / cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nThrowCount);

            mS.GetComponent<ProjectileStatus>().Setup(cCurrentWeapon.optionByLevel[nWeaponLevel - 1].nWeaponDamage);
        }

        GameObject wSound = Instantiate(cCurrentWeapon.shotSound, transform.position, Quaternion.identity);
        yield return null;
    }

}
