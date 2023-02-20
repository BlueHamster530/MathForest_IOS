using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Normal = 0, Elite, Boss }

[CreateAssetMenu(menuName = "CreateTemplate/Create Enemy Data", fileName = "NewEnemyData")]
public class EnemyInfo : ScriptableObject
{
    public int nIndex;        //���͹�ȣ
    public string       strName;        //�����̸�
    public EnemyType eType;             //�� ����
    public int          nHp;            //ü��
    public float        fMoveSpeed;     //�̵��ӵ�    
    public float        fAttackDistance; //���ݹ���
    public int          nDamage;        //������
}
