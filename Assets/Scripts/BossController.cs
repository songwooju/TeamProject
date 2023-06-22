using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public int health = 10000; // 보스의 체력

    public Slider bossHpBar;
    public GameObject stageClearUI;
    private Animator animator;

    public bool isGamePaused = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isGamePaused)
        {
            BossHpBarUpdate();
        }

        if (Manager.instance.isBossAttack)
        {
            animator.SetTrigger("attack");
            Manager.instance.isBossAttack = false;
        }
        // animator.SetBool("attack", false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void BossHpBarUpdate()
    {
        bossHpBar.value = health / 10000f;
        if (bossHpBar.value <= 0)
        {
            GameObject bossHpFillArea = GameObject.Find("Boss_HP").transform.Find("Fill Area").gameObject;
            if (bossHpFillArea != null)
            {
                bossHpFillArea.SetActive(false);
            }
        }
        else
        {
            GameObject bossHpFillArea = GameObject.Find("Boss_HP").transform.Find("Fill Area").gameObject;
            if (bossHpFillArea != null)
            {
                bossHpFillArea.SetActive(true);
            }
        }
    }

    public void Die()
    {
        stageClearUI.SetActive(true);
        PauseGame();
    }

    public void OnStageClearButtonClick()
    {
        ResumeGame();
        SceneManager.LoadScene("StageChange");
    }


    public void IsGameClear()
    {
        ResumeGame();
        SceneManager.LoadScene("ClearScene");
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
