using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public int damage = 1; // 보스의 공격력
    public float destroyTime = 3f; // 공격 오브젝트 삭제 시간

    private void Start()
    {
        // 일정 시간 후에 자동으로 삭제
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 충돌하면 데미지를 입힘
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

