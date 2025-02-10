using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController2 : MonoBehaviour
{
    public int damageAmount = 1; // 보스 공격력

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Player2 player2 = collision.gameObject.GetComponent<Player2>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
            else if (player2 != null)
            {
                player2.TakeDamage(damageAmount);
            }
        }
    }
}
