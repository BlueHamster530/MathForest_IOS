using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : MonoBehaviour
{
    Rigidbody2D rigid2D;
    [SerializeField]
    GameObject PlayerObject;
    PlayerStatus playerStatus;

    [SerializeField]
    [Header("공격사정거리")]
    float fAttackDistance=1.0f;
    [SerializeField]
    [Header("공격데미지")]
    int nAttackDamage=20;
    [SerializeField]
    [Header("공격쿨타임")]
    float fAttackCoolTime=1.0f;
    float fAttackCurrentCoolTime;
    [SerializeField]
    [Header("이동속도")]
    float fMoveSpeed;

    bool bIsAttacking;


    [SerializeField]
    [Header("테스트이동패턴")]
    bool bIsTestingMovingPatten = false;
    float fTestMovingChectTime;



    Vector3 vDirection;
    public void init(GameObject _Playerobject)
    {
        rigid2D = GetComponent<Rigidbody2D>();
        PlayerObject = _Playerobject;
        playerStatus = PlayerObject.GetComponent<PlayerStatus>();
        bIsAttacking = false;
        fAttackCurrentCoolTime = fAttackCoolTime;
        fTestMovingChectTime = 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerObject == null)
        {
            init(GameObject.FindGameObjectWithTag("Player"));
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        MovementFunction();
    }
    private void AttackFunction()
    {
        if (bIsAttacking == false) return;

        if (fAttackCurrentCoolTime >= 0)
        {
            fAttackCurrentCoolTime -= Time.deltaTime;
        }
        else
        {
            fAttackCurrentCoolTime = fAttackCoolTime;
            playerStatus.Damage(nAttackDamage);
        }

    }
    // Update is called once per frame
    void Update()
    {
        AttackFunction();
    }
    private void MovementFunction()
    {
        SetAngle();
        float fDistance = Vector3.Distance(transform.position, PlayerObject.transform.position);
        if (bIsTestingMovingPatten == false)
        {
             vDirection = PlayerObject.transform.position - transform.position;
            if (fAttackDistance <= fDistance)
            {
                //Controller.GetAnimation().SetAnimation("walk", true);
                rigid2D.velocity = vDirection.normalized * fMoveSpeed * Time.deltaTime * 10.0f;
            }
            else
            {
                //Controller.GetAnimation().SetAnimation("idle", true);
                rigid2D.velocity = Vector3.zero;
            }
        }
        else
        {
            if (fAttackDistance <= fDistance)
            {
                if (fTestMovingChectTime <= 0)
                {
                    vDirection = PlayerObject.transform.position - transform.position;
                }
                else
                {
                    fTestMovingChectTime -= Time.deltaTime;
                }
                //Controller.GetAnimation().SetAnimation("walk", true);
                rigid2D.velocity = vDirection.normalized * fMoveSpeed * Time.deltaTime * 10.0f;
            }
            else
            {
                rigid2D.velocity = vDirection.normalized * fMoveSpeed * Time.deltaTime * 10.0f;
                fTestMovingChectTime = 1.0f;
            }
        }

    }
    private void SetAngle()
    {
        if (PlayerObject.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bIsAttacking == false)
            {
                bIsAttacking = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (bIsAttacking == true)
        {
            if (collision.CompareTag("Player"))
            {
                bIsAttacking = false;
            }
        }
    }
}
