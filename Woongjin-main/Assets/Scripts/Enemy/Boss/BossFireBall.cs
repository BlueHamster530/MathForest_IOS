using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    [SerializeField]
    float fSpeed;

    Vector3 target;

    [SerializeField]
    GameObject ExplosionEffect;
    [SerializeField]
    GameObject ExplosionSound;

    int damage;
    private void Start()
    {
    }
    public void Init(Vector3 _target,int _damage)
    {
        target = _target;
        damage = _damage;
        Vector3 vDir = transform.position - target;
        float angle = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = angleAxis;
    }
    private void FixedUpdate()
    {
        transform.position += -transform.right * fSpeed * Time.deltaTime;
    }
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target);
        if (distance <= 0.1f)
        {
            ExplosionEvent();
        }
    }

    private void ExplosionEvent()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Instantiate(ExplosionSound, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerAttackTrigger(collision.gameObject.GetComponent<PlayerStatus>());
        }
    }
    private void PlayerAttackTrigger(PlayerStatus _player)
    {
        if (_player == null)
        {
            Debug.LogError("파이어볼 - 플레이어 정보 없음");
        }
        else
        {
            _player.Damage(damage);
        }
        ExplosionEvent();
    }

}
