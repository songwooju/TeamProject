using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float time;
    public GameObject prefabBullet;

    private GameManager gameManager;
    private Manager manager;


    private void Start()
    {
        gameManager = GameManager.instance;
        manager = Manager.instance;

        if (gameManager != null)
        {
            gameManager.sharedCurrentHealth = gameManager.sharedMaxHealth;
        }

        time = 0;
    }


    void Update()
    {
        FireBullet();
        CheckHealth();
    }

    public void FireBullet()
    {
        time += Time.deltaTime;

        if (time > 0.3f)
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity);
            time -= 0.5f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (gameManager != null)
        {
            gameManager.sharedCurrentHealth -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (manager != null)
            {
                manager.currentObjectCount = 0;
                manager.itemBuff = true;
            }

            Destroy(collision.gameObject);
        }
    }

    private void CheckHealth()
    {
        if (gameManager != null && gameManager.sharedCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
