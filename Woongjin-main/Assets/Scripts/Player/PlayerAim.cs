using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerAim : MonoBehaviour
{
    PlayerAttack playerAttack;
    pAttack_functions attackFunction;

    //public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    [SerializeField] bool isCollision = false;
    public bool IsCollision => isCollision;
    [SerializeField] RadiusIndicator radiusIndicator;
    [SerializeField] GameObject triangleIndicator;
    [SerializeField] Transform shootPosition;

    

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        attackFunction = GetComponent<pAttack_functions>();
    }
    void Update()
    {
        //EnemyChecker();
        DrawIndicator();
        DrawIndicator2();

    }

    public bool EnemyChecker(Transform target)
    {
        Vector3 dir = (target.position + new Vector3(0,0.5f)) - transform.position;
        
        if (dir.magnitude <= radius)
        {
            // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
            float dot = Vector3.Dot(dir.normalized, playerAttack.AimDir);
            // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
            float theta = Mathf.Acos(dot);
            // angleRange�� ���ϱ� ���� degree�� ��ȯ
            float degree = Mathf.Rad2Deg * theta;

            // �þ߰� �Ǻ�
            if (degree <= angleRange / 2f)
            {
                isCollision = true;
                return true;
            }
                
            else
            {
                isCollision = false;
                
                return false;
            }
        }
        else
        {
            isCollision = false;

            return false;
        }
    }

    private void DrawIndicator()
    {
        if (radiusIndicator == null) return;

        radiusIndicator.transform.position = transform.position;

        Vector3 rDir = new Vector3(playerAttack.AimDir.x * transform.localScale.x, playerAttack.AimDir.y, 0);

        radiusIndicator.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(rDir.y, rDir.x) * Mathf.Rad2Deg);

        radiusIndicator.arcPoint2 = 360 - angleRange;
        radiusIndicator.angle = angleRange / 2f;

        radiusIndicator.transform.localScale = new Vector3(radius, radius, radius);
    }

    private void DrawIndicator2()
    {
        if (triangleIndicator == null) return;

        triangleIndicator.transform.position = transform.position;

        Vector3 rDir = new Vector3(playerAttack.AimDir.x * transform.localScale.x, playerAttack.AimDir.y, 0);

        triangleIndicator.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(rDir.y, rDir.x) * Mathf.Rad2Deg);
    }

    private void OnDrawGizmos()
    {
        if (playerAttack == null) return;
        //Handles.color = new Color(1, 0, 0, 0.3f);
        //Handles.DrawSolidArc(transform.position, Vector3.forward, playerAttack.AimDir, angleRange / 2f, radius);
        //Handles.DrawSolidArc(transform.position, Vector3.forward, playerAttack.AimDir, -(angleRange / 2f), radius);

    }
}
