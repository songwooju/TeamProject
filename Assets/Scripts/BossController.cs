using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int health = 100; // ������ ü��
  
    // ������ ü���� ���ҽ�Ű�� �Լ�
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
    }
}
