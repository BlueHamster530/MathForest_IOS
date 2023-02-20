using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    BossController Controller;

    [SerializeField]
    GameObject goFireBall;
    //Vector3(-5.36999989,5.28999996,0)attack3�ִϸ��̼��Ҷ� �ҳ����� ��ġ(������ǥ)
    int nAttackType;

    [Header("���̾������")]
    [SerializeField]
    int Spawncount = 3; //�����
    [Header("���̾���ʸ��� ����")]
    [SerializeField]
    float SpawnTick = 0.3f;//���ʸ��� ����

    [SerializeField] SoundSetter bossSoundPack;

    Vector3 vFireBallSpawnPosition
    {
        get {
            return new Vector3(-5.37f, 5.29f, 0.0f);
            }
    }

    public void Init(BossController bossController)
    {
        Controller = bossController;
    }
    public void AttackStartTrigger()
    {
        nAttackType = Random.Range(0, 2);
        if (nAttackType == 1) nAttackType = 2;
        Controller.GetBossAnim().StartAttackAnim(nAttackType);
    }
    public void AttackEvent()
    {
        switch (nAttackType)
        {
            case 0:
                AttckType1();
                break;
            case 1:
                print("�̰Կֳ��� 1�ȳ������س���");
                break;
            case 2:
                AttckType3();
                break;
            default:
                {
                    print("�� ����Ʈ?");
                }
                break;
        }
    }
    private void AttckType1()
    {
        //�Ϲݸ���ȯ
        if (Controller.GetEnemyManger()== null)
        {
            print("���� ���ͼ�ȯ �Ұ� : �����ʵ�Ͼȵ�");
            return;
        }
#if UNITY_ANDROID
        if (PlayerPrefs.GetInt("setting_vibration") == 1)
            Vibration.Vibrate(300);
#endif
        int rand = Random.Range(0, 100);
        if (rand <= 10)
        {
            Controller.GetEnemyManger().EliteEnemySpawn(true);
            //for (int i = 0; i < 4; i++)
            //{
            //    enemyspawner.NormalEnemySpawn(true);
            //}
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                Controller.GetEnemyManger().NormalEnemySpawn(true);
            }
        }

        SoundSetter qkrworjs = Instantiate(bossSoundPack, Vector3.zero, Quaternion.identity);
        qkrworjs.SoundSetup(1);
        Debug.Log("���纣�纣 ����");
    }
    private void AttckType3()
    {
        StartCoroutine("SpawnFireBall");
        //���̾
    }
    private IEnumerator SpawnFireBall()
    {
        int count = Spawncount;
        while(count > 0)
        {
            GameObject clone = Instantiate(goFireBall, transform.position + vFireBallSpawnPosition, Quaternion.identity);
            clone.GetComponent<BossFireBall>().Init(Controller.GetPlayerObject().transform.position,Controller.GetInfo().nDamage);
            count--;

            SoundSetter qkrworjs = Instantiate(bossSoundPack, Vector3.zero, Quaternion.identity);
            qkrworjs.SoundSetup(0);

            yield return new WaitForSeconds(SpawnTick);
        }
        yield return 0;
    }


}
