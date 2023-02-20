using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{
    //생성된 적 저장
    List<GameObject> spawnedEnemys;


    [SerializeField]
    GameObject[] EnemyPrefabs;
    [SerializeField]
    EnemyInfo[] EnemyInfo;

    [SerializeField]
    EnemyInfo[] EliteEnemyInfo;

    [SerializeField]
    [Header("적 주기별 생성 여부")]
    public bool IsEnemySpawnByTime = true;
    [SerializeField]
    [Header("적 생성 주기")]
    float fEnemySpawnTime = 1.0f;
    float fCurrentEnemySpawnTime = 0;

    [SerializeField]
    [Header("적 생성 개수")]
    int nSpawnNumber = 1;

    GameObject PlayerObject;


    [SerializeField]
    [Header("적생성기준카메라")]
    Camera PlayerCam;
    [SerializeField]
    [Header("적 부모 오브젝트")]
    GameObject enemyTransformParent;

    bool IsGhostSpawned;
    public int WorngAnswerCount;
    [SerializeField]
    [Header("오답고스트오브젝트")]
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
        //카메라 바깥 기준 좌 우는 10, 상 하 는7 정도
        switch (_index)
        {
            case 0://플레이어 좌측에서 적 출현
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(-0.3f, Random.Range(0.0f, 1.0f), 0.0f));
                break;
            case 1://플레이어 우측에서 적 출현
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(1.3f, Random.Range(0.0f, 1.0f), 0.0f));
                break;
            case 2://플레이어 아래에서 적 출현
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), -0.3f, 0.0f));
                break;
            case 3://플레이어 위에서 적 출현
                Result = PlayerCam.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.3f, 0.0f));
                break;
            default://혹시모를 오류시 좌측에서 출현으로 설정
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
        //카메라 바깥 기준 좌 우는 10, 상 하 는7 정도
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
        //카메라 바깥 기준 좌 우는 10, 상 하 는7 정도
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
