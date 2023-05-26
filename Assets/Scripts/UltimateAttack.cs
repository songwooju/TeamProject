using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UltimateAttack : MonoBehaviour
{
    public Animator playerAnimator;
    public Image animationImage;
    public Button pauseButton;
    public BossController bossController;

    private bool isPaused = false;
    private float pauseDuration = 3f;
    private int damageAmount = 500;

    private void Start()
    {
        Time.timeScale = 1f;
        animationImage.enabled = false;

        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void Update()
    {
        if (isPaused)
        {
            playerAnimator.SetBool("IsUsingUltimate", true);
        }
        else
        {
            playerAnimator.SetBool("IsUsingUltimate", false);
        }
    }

    private void OnPauseButtonClicked()
    {
        if (!isPaused)
        {
            StartCoroutine(PauseGame());
        }
    }

    private IEnumerator PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        animationImage.enabled = true;

        yield return new WaitForSecondsRealtime(pauseDuration);

        Time.timeScale = 1f;
        isPaused = false;
        animationImage.enabled = false;

        if (bossController != null)
        {
            bossController.ApplyDamage(damageAmount);
        }
    }
}
