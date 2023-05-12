using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Transform bossTransform;

    private int damage = 10; // 총알의 데미지

    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
        bossTransform = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
        DestroyBullet();
    }

    void FireBullet()
    {
        Vector3 direction = (bossTransform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
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
            if (boss != null) // BossController 스크립트가 존재할 경우
            {
                boss.TakeDamage(damage); // 보스의 체력을 감소시킴
                if (boss.health <= 0) // 보스의 체력이 0 이하일 경우
                {
                    Destroy(collision.gameObject); // 보스를 파괴함
                }
            }
            Destroy(gameObject); // 총알을 파괴함
        }
    }
}
