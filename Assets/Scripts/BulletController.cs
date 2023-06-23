using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Transform bossTransform;

    private int damage = 1; // �Ѿ��� ������

    private Rigidbody2D bulletRigidbody;

    void Start()
    {
        speed = 10.0f;
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
        if (bossObject != null)
        {
            bossTransform = bossObject.transform;
        }

        bulletRigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
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
            angle -= 75f; // ȸ�� ������ 45���� �� ������ ����
            bulletRigidbody.rotation = angle; // �Ѿ��� ȸ���� ���⿡ ���� ����
            bulletRigidbody.velocity = direction * speed; // Rigidbody2D�� velocity �Ӽ��� �̿��Ͽ� �̵�
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
            BossController boss = collision.GetComponent<BossController>(); // ������ BossController ��ũ��Ʈ�� ������
            if (boss != null) // BossController ��ũ��Ʈ�� ������ ���
            {
                boss.TakeDamage(damage); // ������ ü���� ���ҽ�Ŵ
                if (boss.health <= 0) // ������ ü���� 0 ������ ���
                {
                    boss.Die(); // ������ �ı���
                }
            }
            //Destroy(gameObject); // �Ѿ��� �ı���
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
