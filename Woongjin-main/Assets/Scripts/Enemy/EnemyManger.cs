using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{
    //������ �� ����
    List<GameObject> spawnedEnemys;


    [SerializeField]
    GameObject[] EnemyPrefabs;
    [SerializeField]
    EnemyInfo[] EnemyInfo;

    [SerializeField]
    EnemyInfo[] EliteEnemyInfo;

    [SerializeField]
    [Header("�� �ֱ⺰ ���� ����")]
    public bool IsEnemySpawnByTime = true;
    [SerializeField]
    [Header("�� ���� �ֱ�")]
    float fEnemySpawnTime = 1.0f;
    float fCurrentEnemySpawnTime = 0;

    [SerializeField]
    [Header("�� ���� ����")]
    int nSpawnNumber = 1;

    GameObject PlayerObject;


    [SerializeField]
    [Header("����������ī�޶�")]
    Camera PlayerCam;
    [SerializeField]
    [Header("�� �θ� ������Ʈ")]
    GameObject enemyTransformParent;

    bool IsGhostSpawned;
    public int WorngAnswerCount;
    [SerializeField]
    [Header("�����Ʈ������Ʈ")]
    GameObject GhostEnemyObject;

    [SerializeField]
    private int nMaxSpawnMonster = 50;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("Challenge_First") == 0)
        {
            PlayerPrefs.SetInt("Challenge_First", 1);

            if(ChallengeManager.instance != null)
            {
                ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_First);
            }
        }

        spawnedEnemys = new List<GameObject>();

        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        IsGhostSpawned = false;
        WorngAnswerCount = 0;
        //GhostEnemyObject.SetActive(false);
    }
    private void EnemySpawnByTimeFunction()
    {
        if (IsEnemySpawnByTime == false || WaveManager.instance.wavePlayMode != PlayingMode.Play) return;

        fCurrentEnemySpawnTime -= Time.deltaTime;
        if (fCurrentEnemySpawnTime <= 0)
        {
            fCurrentEnemySpawnTime = fEnemySpawnTime;
            for (int i = 0; i < nSpawnNumber; i++)
            {
                NormalEnemySpawn();
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        EnemySpawnByTimeFunction();
        GhostSpawnFunction();
    }
    private void GhostSpawnFunction()
    {
       // if (IsGhostSpawned == true) return;

        if (WorngAnswerCount >= 5)
        {
            int RandomSpawnPosition = Random.Range(0, 4);

            GameObject clone = Instantiate(GhostEnemyObject, GetSpawnPosition(RandomSpawnPosition), Quaternion.identity);
            clone.transform.position =
                new Vector3(clone.transform.position.x, clone.transform.position.y, 0);
            clone.GetComponent<Enemy_Ghost>().init(PlayerObject);
            GhostEnemyObject.SetActive(true);
            WorngAnswerCount = 0;

            QuestionManager.instance.DangerUI.value = 0.0f;
            //IsGhostSpawned = true;
        }

    }
    private Vector3 GetSpawnPosition(int _index)
    {
        Vector3 Result = Vector3.zero;
        //ī�޶� �ٱ� ���� �� ��� 10, �� �� ��7 ����
        switch (_index)
        {
            case 0://�÷��̾� �������� �� ����
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(-0.3f, Random.Range(0.0f, 1.0f), 0.0f));
                break;
            case 1://�÷��̾� �������� �� ����
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(1.3f, Random.Range(0.0f, 1.0f), 0.0f));
                break;
            case 2://�÷��̾� �Ʒ����� �� ����
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), -0.3f, 0.0f));
                break;
            case 3://�÷��̾� ������ �� ����
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.3f, 0.0f));
                break;
            default://Ȥ�ø� ������ �������� �������� ����
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(-0.3f, Random.Range(0.0f, 1.0f), 0.0f));
                break;
        }

        if (Result.x <= -36.0f)
        {
            Result = PlayerCam.ViewportToWorldPoint(new Vector3(1.3f, Random.Range(0.0f, 1.0f), 0.0f));
        }
        else if (Result.x >= 60.0f)
        {
            Result = PlayerCam.ViewportToWorldPoint(new Vector3(-0.3f, Random.Range(0.0f, 1.0f), 0.0f));
        }
        if (Result.y >= 33.3f)
        {
            Result = PlayerCam.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), -0.3f, 0.0f));
        }
        else if (Result.y <= -35.2f)
        {
            Result = PlayerCam.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.3f, 0.0f));
        }

        return Result;
    }
    public void EliteEnemySpawn(bool isSpawnbyBoss = false)
    {
        int RandomSpawnPosition = Random.Range(0, 4);
        if (isSpawnbyBoss == true)
        {
            if (RandomSpawnPosition == 1)
                RandomSpawnPosition = 0;
        }
        Vector3 SpawnPosition = GetSpawnPosition(RandomSpawnPosition);
        //ī�޶� �ٱ� ���� �� ��� 10, �� �� ��7 ����
        SpawnPosition.z = 0;
        int _index = Random.Range(0, EnemyPrefabs.Length);
        GameObject Clone = Instantiate(EnemyPrefabs[_index],
            SpawnPosition, Quaternion.identity);
        spawnedEnemys.Add(Clone);
        EnemyController cloneController = Clone.GetComponent<EnemyController>();
        cloneController.Init(EliteEnemyInfo[_index], PlayerObject);
        WaveManager.instance.nNowSurviveMonster++;
    }

    public void NormalEnemySpawn(bool isSpawnbyBoss = false)
    {
        if (WaveManager.instance.nNowSurviveMonster >= nMaxSpawnMonster)
            return;

        int RandomSpawnPosition = Random.Range(0, 4);
        if (isSpawnbyBoss == true)
        {
            if (RandomSpawnPosition == 1)
                    RandomSpawnPosition = 0;
        }
        Vector3 SpawnPosition = GetSpawnPosition(RandomSpawnPosition);
        //ī�޶� �ٱ� ���� �� ��� 10, �� �� ��7 ����
        SpawnPosition.z = 0;
        int _EnemyIndex = Random.Range(0, EnemyPrefabs.Length);

        GameObject Clone = Instantiate(EnemyPrefabs[_EnemyIndex],
            SpawnPosition, Quaternion.identity);

        Clone.transform.SetParent(enemyTransformParent.transform);
        spawnedEnemys.Add(Clone);
        EnemyController cloneController = Clone.GetComponent<EnemyController>();
        cloneController.Init(EnemyInfo[_EnemyIndex], PlayerObject);
        WaveManager.instance.nNowSurviveMonster++;
    }
    public void EnemySpawnSet(bool arg)
    {
        IsEnemySpawnByTime = arg;

        if(arg == false)
        {
            foreach(GameObject enemys in spawnedEnemys)
            {
                Destroy(enemys);
            }
        }
    }


    public void CheckSmartChallenge()
    {
        if (IsGhostSpawned == false)
        {
            if (PlayerPrefs.GetInt("Challenge_Smart") == 0)
            {
                PlayerPrefs.GetInt("Challenge_Smart", 1);

                if (ChallengeManager.instance != null)
                {
                    ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Smart);
                }
            }
        }
    }
}
