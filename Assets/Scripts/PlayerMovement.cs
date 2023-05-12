using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int maxHealth = 3; // �ִ� ü��
    public int currentHealth; // ���� ü��

    float time;
    public GameObject prefabBullet;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
    }

    public void FireBullet()
    {
        time += Time.deltaTime;
        //Debug.Log("Fire" + time);
        if (time > 0.3f)
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity);
            time -= 0.5f;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
