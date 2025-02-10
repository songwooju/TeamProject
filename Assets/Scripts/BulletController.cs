using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Transform bossTransform;

    private int damage = 1; // 총알의 데미지

    private Rigidbody2D bulletRigidbody;

    void Start()
    {
        speed = 10.0f;
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
        if (bossObject != null)
        {
            bossTransform = bossObject.transform;
        }

        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FireBullet();
        DestroyBullet();
        BulletDamageChange();
    }

    void FireBullet()
    {
        if (bossTransform != null)
        {
            Vector2 direction = (bossTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 75f; // 회전 각도에 45도를 뺀 값으로 수정
            bulletRigidbody.rotation = angle; // 총알의 회전을 방향에 따라 설정
            bulletRigidbody.velocity = direction * speed; // Rigidbody2D의 velocity 속성을 이용하여 이동
        }
    }

    void DestroyBullet()
    {
        if (transform.position.y > bossTransform.position.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            BossController boss = collision.GetComponent<BossController>(); // 보스의 BossController 스크립트를 가져옴
            if (boss != null) 
            {
                boss.TakeDamage(damage);
                if (boss.health <= 0)
                {
                    boss.Die();
                }
            }
        }
    }

    void BulletDamageChange()
    {
        if (Manager.instance != null && Manager.instance.itemBuff)
        {
            damage = 12;
        }
        else
        {
            damage = 10;
        }
    }
}
