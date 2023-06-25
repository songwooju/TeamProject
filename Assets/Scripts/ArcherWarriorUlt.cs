using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArcherWarriorUlt : MonoBehaviour
{
    public GameObject popupPanel;
    public float popupDuration = 3f;

    public bool isPopupShowing = false;
    private bool isGamePaused = false;

    private GameManager gameManager;
    private BossController bossController;

    AudioSource audioSrc;
    private void Start()
    {
        popupPanel.SetActive(false);

        gameManager = GameManager.instance;
        bossController = FindObjectOfType<BossController>();

        audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
    }

    private void Update()
    {
        if (gameManager != null && gameManager.testMp >= 100)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isPopupShowing)
                {
                    audioSrc.Play();
                    StartCoroutine(ShowPopup());
                }
            }
        }
    }

    private IEnumerator ShowPopup()
    {
        if (popupPanel == null)
        {
            yield break;
        }


        Time.timeScale = 0f;
        isGamePaused = true;

        popupPanel.SetActive(true);
        isPopupShowing = true;

        yield return new WaitForSecondsRealtime(popupDuration);

        popupPanel.SetActive(false);
        isPopupShowing = false;

        if (bossController != null)
        {
            bossController.TakeDamage(50);
        }

        Time.timeScale = 1f;
        isGamePaused = false;

        gameManager.testMp = 0;
    }
}
