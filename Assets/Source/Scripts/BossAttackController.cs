using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public int damage = 1; // ������ ���ݷ�
    public float destroyTime = 3f; // ���� ������Ʈ ���� �ð�

    private void Start()
    {
        // ���� �ð� �Ŀ� �ڵ����� ����
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾ �浹�ϸ� �������� ����
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage);
                if (player.currentHealth <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}

