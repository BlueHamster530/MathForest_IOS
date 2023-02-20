using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyController Controller;
    Rigidbody2D rigid2D;
    // Start is called before the first frame update
    public void Init(EnemyController _controller)
    {
        Controller = _controller;
        Controller.IsCanMove = true;
        rigid2D = GetComponent<Rigidbody2D>();
    }
    private void MovementFunction()
    {
        if (Controller.IsCanMove == false || Controller.GetIsDead())
        {
            if (rigid2D.velocity != Vector2.zero)
                rigid2D.velocity = Vector2.zero;

            return;
        }
        SetAngle();
        Vector3 vDirection = Controller.PlayerObject.transform.position - transform.position;
        float fDistance = Vector3.Distance(transform.position, Controller.PlayerObject.transform.position);
        if (Controller.GetInfo().fAttackDistance <= fDistance)
        {
            //Controller.GetAnimation().SetAnimation("walk", true);
            rigid2D.velocity = vDirection.normalized * Controller.GetInfo().fMoveSpeed * Time.deltaTime * 10.0f;
        }
        else
        {
            //Controller.GetAnimation().SetAnimation("idle", true);
            rigid2D.velocity = Vector3.zero;
        }

    }
    private void SetAngle()
    {
        if (Controller.PlayerObject.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        MovementFunction();
    }
}
