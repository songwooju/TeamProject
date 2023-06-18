using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public int health = 100; // 보스의 체력

    public Slider bossHpBar;

    private void Update()
    {
        BossHpBarUpdate();
    }

    // 보스의 체력을 감소시키는 함수
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void BossHpBarUpdate()
    {
        bossHpBar.value = health / 10000f;
        if (bossHpBar.value <= 0)
        {
            GameObject.Find("Canvas").transform.Find("Boss_HP").transform.Find("Fill Area").gameObject.SetActive(false);
        }
        else
            GameObject.Find("Canvas").transform.Find("Boss_HP").transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
