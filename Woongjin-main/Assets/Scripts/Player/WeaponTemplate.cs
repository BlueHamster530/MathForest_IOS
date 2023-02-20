using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Projectile = 0, Thrower, Immediate, Cast}

[System.Serializable]
[CreateAssetMenu(menuName = "CreateTemplate/Create Weapon Data", fileName = "NewWeaponData")]
public class WeaponTemplate : ScriptableObject
{
    public int          nWeaponCode;        //�����ڵ�        
    public string       sWeaponName;        //�����̸�
    public WeaponType   eType;              //����Ÿ��
   
    public int          nWeaponMagazine;    //ȹ�� �� źâ
    public int          nRefillMagazine;    //��ȹ�� Ȥ�� źâ ������ ȹ�� �� �߰�źâ

    public SkillLevel[] optionByLevel;      //��ų ������ ���� �ɼ�

    public GameObject shotSound;        //���ݻ���

    [System.Serializable]
    public struct SkillLevel
    {
        public int      nWeaponDamage;      //�ߴ� ������
        public float    fWeaponDelay;       //������
        public int      nThrowCount;        //����� �Ѹ��� Ƚ��

        public float fAimDegree;            //���� ����
        public float fAimDistance;          //���� �Ÿ�

        public GameObject gWeaponPrefab;    //�տ� ��� ���� ������
        public GameObject gFireEffect;      //�߻� ����Ʈ ������
        public GameObject gBackEffect;      //����
        public GameObject gBulletPrefab;    //����ü ������

        public string   sAttackMethod;      //���� �� ������ �޼ҵ� �̸�        

        public float sizeDelta;
    }

}
