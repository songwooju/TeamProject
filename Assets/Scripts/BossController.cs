using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public int health = 100; // ������ ü��

    public Slider bossHpBar;

    private void Update()
    {
        BossHpBarUpdate();
    }

    // ������ ü���� ���ҽ�Ű�� �Լ�
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
