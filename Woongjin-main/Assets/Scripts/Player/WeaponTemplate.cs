using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Projectile = 0, Thrower, Immediate, Cast}

[System.Serializable]
[CreateAssetMenu(menuName = "CreateTemplate/Create Weapon Data", fileName = "NewWeaponData")]
public class WeaponTemplate : ScriptableObject
{
    public int          nWeaponCode;        //무기코드        
    public string       sWeaponName;        //무기이름
    public WeaponType   eType;              //무기타입
   
    public int          nWeaponMagazine;    //획득 시 탄창
    public int          nRefillMagazine;    //재획득 혹은 탄창 아이템 획득 시 추가탄창

    public SkillLevel[] optionByLevel;      //스킬 레벨에 따른 옵션

    public GameObject shotSound;        //공격사운드

    [System.Serializable]
    public struct SkillLevel
    {
        public int      nWeaponDamage;      //발당 데미지
        public float    fWeaponDelay;       //딜레이
        public int      nThrowCount;        //방사기류 뿌리는 횟수

        public float fAimDegree;            //조준 각도
        public float fAimDistance;          //조준 거리

        public GameObject gWeaponPrefab;    //손에 드는 무기 프리팹
        public GameObject gFireEffect;      //발사 이펙트 프리팹
        public GameObject gBackEffect;      //연기
        public GameObject gBulletPrefab;    //투사체 프리팹

        public string   sAttackMethod;      //공격 시 실행할 메소드 이름        

        public float sizeDelta;
    }

}
