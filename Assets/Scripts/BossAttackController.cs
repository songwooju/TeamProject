using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public int damageAmount = 1; // ÇÇÇØ·®

    private void Start()
    {
        Invoke("DestroyGameObject", 1.5f);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Player2 player2 = collision.gameObject.GetComponent<Player2>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                Destroy(gameObject);
            }
            else if (player2 != null)
            {
                player2.TakeDamage(damageAmount);
                Destroy(gameObject);
            }
        }
    }
}