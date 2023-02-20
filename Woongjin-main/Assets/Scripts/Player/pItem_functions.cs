using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pItem_functions : MonoBehaviour
{
    PlayerStatus charStatus;
    PlayerAttack charAttack;
    PlayerMove charMove;

    [SerializeField] GameObject effectHeal, effectHeal2, effectMagazine;

    [SerializeField] GameObject charShield;
    [SerializeField] SoundSetter itemSoundPack;


    private void Awake()
    {
        charStatus = GetComponent<PlayerStatus>();
        charAttack = GetComponent<PlayerAttack>();
        charMove = GetComponent<PlayerMove>();
    }
    public void GetItem(int code)
    {
        StopCoroutine("d_"+code);
        StartCoroutine("d_"+code);
    }
    

    private IEnumerator d_201()
    {
        charStatus.hp += 30;
        charStatus.hp = Mathf.Clamp(charStatus.hp, 0, charStatus.maxHp);

        GameObject effect = Instantiate(effectHeal, transform.position+new Vector3(0,0,0), Quaternion.identity);
        effect.transform.SetParent(transform);

        ItemSoundPlay(0);
        yield return null;
    }

    private IEnumerator d_202()
    {
        charStatus.hp += 100;
        charStatus.hp = Mathf.Clamp(charStatus.hp, 0, charStatus.maxHp);

        GameObject effect = Instantiate(effectHeal2, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        effect.transform.SetParent(transform);
        ItemSoundPlay(0);
        yield return null;
    }

    private IEnumerator d_203()
    {
        charAttack.nMagazine += charAttack.cCurrentWeapon.nRefillMagazine;

        GameObject effect = Instantiate(effectMagazine, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
        effect.transform.SetParent(transform);
        ItemSoundPlay(1);
        yield return null;
    }

    private IEnumerator d_204()
    {
        charShield.SetActive(true);
        charStatus.isShield = true;
        ItemSoundPlay(2);
        yield return new WaitForSeconds(15f);
        charShield.SetActive(false);
        charStatus.isShield = false;
    }

    private IEnumerator d_205()
    {
        charMove.fPlusSpeed = 2;
        charMove.isBoost = true;
        ItemSoundPlay(3);
        yield return new WaitForSeconds(30f);

        charMove.fPlusSpeed = 0;
        charMove.isBoost = false;
    }

    public void ItemSoundPlay(int a)
    {
        SoundSetter sp = Instantiate(itemSoundPack, Vector3.zero, Quaternion.identity);

        sp.SoundSetup(a);
    }
}
