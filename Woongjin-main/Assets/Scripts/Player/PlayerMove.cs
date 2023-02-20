using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]    StageManager    cStageManager;
    VirtualPad                          cMyPad;

    [SerializeField]    float           fSpeed;
    public float fPlusSpeed;
    [SerializeField]    Vector2         fPadInput;
    public              Vector2         FPadInput => fPadInput;

    PlayerAnimation playerAnim;
    PlayerStatus charStatus;

    Rigidbody2D rigid;

    public bool isBoost;

    [Header("illusion")]
    [SerializeField] int skinIndex;
    [SerializeField] IllusionSpritePack[] illusionPacks;
    int sNum;
    float sCoolTime;
    

    private void Awake()
    {
        charStatus = GetComponent<PlayerStatus>();
        rigid = GetComponent<Rigidbody2D>();
        if (cStageManager == null)
        {
            cStageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        }
        else cMyPad = cStageManager.gPadMove;

        if (PlayerPrefs.HasKey("PlayerSkinIndex"))
            skinIndex = PlayerPrefs.GetInt("PlayerSkinIndex");
        else skinIndex = 0;
    }

    private void FixedUpdate()
    {
        if (charStatus != null && (charStatus.isDeath || charStatus.isSuperInvincible)) return;
        Move();
    }

    private void Move()
    {
        //fPadInput = InputCorrection(cMyPad.vPadOutput);
        rigid.velocity = Vector2.zero;
        fPadInput = cMyPad.vPadOutput;
        transform.position += (Vector3)fPadInput * (fSpeed + fPlusSpeed) * Time.deltaTime;

        if (fPadInput != Vector2.zero && isBoost && sCoolTime < 0)
        {
            CreateIllusion();
        }
        else sCoolTime -= Time.deltaTime;
    }

    private void CreateIllusion()
    {
        IllusionSpritePack kitasan = Instantiate(illusionPacks[skinIndex], transform.position + new Vector3(0.2f * -transform.localScale.x, 0), Quaternion.identity);
        kitasan.Setup(sNum);
        kitasan.transform.localScale = transform.localScale * 0.3f;
        sNum++;
        sNum %= 4;

        sCoolTime = 0.1f;
    }


}
