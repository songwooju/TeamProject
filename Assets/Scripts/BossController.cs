using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int health = 100; // 보스의 체력
  
    // 보스의 체력을 감소시키는 함수
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
    }
}
