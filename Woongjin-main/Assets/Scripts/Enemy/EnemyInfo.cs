using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Normal = 0, Elite, Boss }

[CreateAssetMenu(menuName = "CreateTemplate/Create Enemy Data", fileName = "NewEnemyData")]
public class EnemyInfo : ScriptableObject
{
    public int nIndex;        //몬스터번호
    public string       strName;        //몬스터이름
    public EnemyType eType;             //적 종류
    public int          nHp;            //체력
    public float        fMoveSpeed;     //이동속도    
    public float        fAttackDistance; //공격범위
    public int          nDamage;        //데미지
}
