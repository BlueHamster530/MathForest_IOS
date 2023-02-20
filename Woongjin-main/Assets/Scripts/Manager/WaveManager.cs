using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Playables;

public enum PlayingMode { Wait, Play, End, Boss }

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("related Wave")]   
    [SerializeField] float waveTime;
    [SerializeField] TextMeshProUGUI waveText;
    public Slider waveSlider;
    float playTime;

    public float playTotalTime;
    public int huntCount;
    public int currentWave;

    PlayerStatus player;

    [Header("보물상자 관련")]
    [SerializeField] float boxCreateCurrentTime;
    float boxCreateTime;
    [SerializeField] float boxCreateCurrentTime2;
    float boxCreateTime2;

    [Header("Prefabs")]
    [SerializeField] GameObject WaveStartText;
    [SerializeField] LevelupItemSetter upgradePanel;
    [SerializeField] PlayerStatus playerStatus;

    [SerializeField] GameObject tresureBox, tresureBox2;

    public PlayingMode wavePlayMode;

    [Header("뭐라고 써야할지 모르겠음 ")]
    [SerializeField] UI_PanelResult waveResultPanel;
    [SerializeField] GameObject uiMain;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] GameObject levelupObject;

    [Header("보스관련")]
    [SerializeField] int bossWave;
    [SerializeField] GameObject BossOnlyCam;
    [SerializeField] GameObject NormalCam;
    [SerializeField] GameObject BossGamemap;
    [SerializeField] GameObject NormalGameMap;
    [SerializeField] GameObject BossGameObject;

    [Header("보스 등장")]
    [SerializeField] GameObject bossWarning;
    [SerializeField] PlayableDirector bossCutScene;

    public int nNowSurviveMonster { get; set; }

    public WaveManager()
    {
        instance = this;
    }

    void Start()
    {
        wavePlayMode = PlayingMode.Wait;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        nNowSurviveMonster = 0;
        WaveStart(1);
    }

    void Update()
    {
        WaveProgress();
    }

    private void StartBossStage()
    {
        uiMain.SetActive(false);
        wavePlayMode = PlayingMode.Boss;
        GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;
        GameObject.Find("EnemyManager").GetComponent<EnemyManger>().IsEnemySpawnByTime = false;
        BossOnlyCam.transform.position = Vector3.zero;
        NormalGameMap.SetActive(false);
        BossGamemap.SetActive(true);
        NormalCam.SetActive(false);
        BossOnlyCam.SetActive(true);

        ClearAllEnemy();

        //BossGameObject.SetActive(true);
    }

    public void ClearAllEnemy()
    {
        GameObject[] clone = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < clone.Length; i++)
        {
            if(clone[i].TryGetComponent<EnemyController>(out EnemyController con)) con.HitDamage(9999);

        }
    }
    private void WaveStart(int num)
    {
        GameObject.FindObjectOfType<PlayerAttack>().WeaponRenew();

        upgradePanel.gameObject.SetActive(false);

        playTime = 0;
        boxCreateTime = Random.Range(15f, 20f);
        boxCreateTime2 = Random.Range(30f, 35f);

        currentWave = num;
        if (currentWave == 2)
        {
            ChallengeManager.instance.PlayerAttackedCheckFunction();
        }
        
        if(currentWave%3 == 0 )//매 3스테이지 마다
        {
            GameObject.FindObjectOfType<EnemyManger>().EliteEnemySpawn();
        }
        if (currentWave == bossWave)
        {
            StartBossStage();
            StartCoroutine("BossStageStandby");
        }
        else
        {                     
            wavePlayMode = PlayingMode.Play;

            WaveStartText.SetActive(true);

            //ws.GetComponent<TextMeshProUGUI>().text = $"STAGE {currentWave} START";


            waveText.text = $"스테이지 진행 (웨이브 {currentWave})";

            GameObject.FindObjectOfType<EnemyManger>().EnemySpawnSet(true);
        }
    }

    private IEnumerator BossStageStandby()
    {
        bossWarning.SetActive(true);

        if (SystemSoundPlayer.instance != null) SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.warning);

        yield return new WaitForSeconds(1.5f);

        bossCutScene.Play();
    }

    private void WaveProgress()
    {
        if(wavePlayMode == PlayingMode.Play || wavePlayMode == PlayingMode.Boss)
        {
            boxCreateCurrentTime += Time.deltaTime;
            boxCreateCurrentTime2 += Time.deltaTime;

            if (boxCreateCurrentTime > boxCreateTime)
            {
                boxCreateCurrentTime = 0;
                boxCreateTime = Random.Range(15f, 20f);
                //boxCreateTime = 2;
                BoxCreate();
            }
            if (boxCreateCurrentTime2 > boxCreateTime2)
            {
                boxCreateCurrentTime2 = 0;
                boxCreateTime2 = Random.Range(30f, 35f);
                // boxCreateTime2 = 2;

                BoxCreate2();
            }
        }

        if(wavePlayMode == PlayingMode.Play)
        {
            playTime += Time.deltaTime;
            playTotalTime += Time.deltaTime;

            
            if (waveSlider != null) waveSlider.value = playTime / waveTime;

            if(playTime > waveTime)
            {
                playTime = 0;

                GameObject le = Instantiate(levelupObject, playerStatus.transform.position, Quaternion.identity);
                le.transform.SetParent(playerStatus.transform);

                if (SystemSoundPlayer.instance != null) SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.levelup);
                Invoke("Gogogo", 1f);
                
            }
        }
    }

    public void Gogogo()
    {
        wavePlayMode = PlayingMode.Wait;
        WaveEnd();
    }

    private void BoxCreate()
    {
        float ranX = Random.Range(5f, 13f);
        float ranY = Random.Range(5f, 13f);

        Vector3 spawnLocation = Random.value <= 0.5f ? new Vector2(ranX, ranY) : new Vector2(-ranX, ranY);

        if (player == null) return;
        Vector3 spawnpoisition = player.transform.position + spawnLocation;

        if (spawnpoisition.x <= -36.0f)
        {
            spawnpoisition = new Vector3(-33.0f + Random.Range(2.0f, 7.0f), spawnpoisition.y, spawnpoisition.z);
        }
        else if (spawnpoisition.x >= 60.0f)
        {
            spawnpoisition = new Vector3(55.0f - Random.Range(2.0f, 7.0f), spawnpoisition.y, spawnpoisition.z);
        }
        if (spawnpoisition.y >= 33.3f)
        {
            spawnpoisition = new Vector3(spawnpoisition.x, 30.0f - Random.Range(2.0f, 7.0f), spawnpoisition.z);
        }
        else if (spawnpoisition.y <= -35.2f)
        {
            spawnpoisition = new Vector3(spawnpoisition.x, -30.0f + Random.Range(2.0f, 7.0f), spawnpoisition.z);
        }
        GameObject tb = Instantiate(tresureBox, spawnpoisition, Quaternion.identity);
    }

    private void BoxCreate2()
    {
        float ranX = Random.Range(5f, 13f);
        float ranY = Random.Range(5f, 13f);

        Vector3 spawnLocation = Random.value <= 0.5f ? new Vector2(ranX, ranY) : new Vector2(-ranX, ranY);

        if (player == null) return;
        Vector3 spawnpoisition = player.transform.position + spawnLocation;

     
        if (spawnpoisition.x <= -36.0f)
        {
            spawnpoisition = new Vector3(-33.0f + Random.Range(2.0f, 7.0f), spawnpoisition.y, spawnpoisition.z);
        }
        else if (spawnpoisition.x >= 60.0f)
        {
            spawnpoisition = new Vector3(55.0f - Random.Range(2.0f, 7.0f), spawnpoisition.y, spawnpoisition.z);
        }
        if (spawnpoisition.y >= 33.3f)
        {
            spawnpoisition = new Vector3(spawnpoisition.x, 30.0f - Random.Range(2.0f, 7.0f), spawnpoisition.z);
        }
        else if (spawnpoisition.y <= -35.2f)
        {
            spawnpoisition = new Vector3(spawnpoisition.x, -30.0f + Random.Range(2.0f, 7.0f), spawnpoisition.z);
        }
        GameObject tb = Instantiate(tresureBox2, spawnpoisition, Quaternion.identity);

    }

    private void WaveEnd()
    {
        Debug.Log("ssss");
       // GameObject.FindObjectOfType<EnemyManger>().EnemySpawnSet(false);
        upgradePanel.gameObject.SetActive(true);
        upgradePanel.CardSetup();
    }

    public void LevelUp()
    {
        WaveStart(currentWave + 1);
    }

    public void GameSet(int a)
    {
        wavePlayMode = PlayingMode.End;
        if (playerStatus != null) playerStatus.isSuperInvincible = true;
        StartCoroutine("CameraSetup", a);  
    }

    public IEnumerator CameraSetup(int arg)
    {
        playerCam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
        int a = arg;

        CinemachineFramingTransposer transposer = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        float current = 0;
        float percent = 0;

        transposer.m_TrackedObjectOffset = new Vector3(1, 0.5f);
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 2f;

            playerCam.m_Lens.OrthographicSize = Mathf.Lerp(playerCam.m_Lens.OrthographicSize, 2, percent);

            yield return null;
        }
        yield return null;

        waveResultPanel.gameObject.SetActive(true);
        waveResultPanel.PanelSetup(a);

    }
}
