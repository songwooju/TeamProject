using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Transform bossTransform;

    private int damage = 10; // �Ѿ��� ������

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
            BossController boss = collision.GetComponent<BossController>(); // ������ BossController ��ũ��Ʈ�� ������
            if (boss != null) // BossController ��ũ��Ʈ�� ������ ���
            {
                boss.TakeDamage(damage); // ������ ü���� ���ҽ�Ŵ
                if (boss.health <= 0) // ������ ü���� 0 ������ ���
                {
                    Destroy(collision.gameObject); // ������ �ı���
                }
            }
            Destroy(gameObject); // �Ѿ��� �ı���
        }
    }
}
