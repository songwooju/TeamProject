using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController2 : MonoBehaviour
{
    public int damageAmount = 1; // ÇÇÇØ·®

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                if (GameManager.instance.sharedCurrentHealth <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
