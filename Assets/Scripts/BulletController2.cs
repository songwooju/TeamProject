using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController2 : MonoBehaviour
{
    public int damage = 100;

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
        }
    }
}