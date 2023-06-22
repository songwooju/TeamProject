using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArcherWarriorUlt : MonoBehaviour
{
    public GameObject popupPanel;
    public float popupDuration = 3f;

    private bool isPopupShowing = false;
    private bool isGamePaused = false;

    private GameManager gameManager; // GameManager 참조를 저장할 변수
    private BossController bossController; // BossController 참조를 저장할 변수

    private void Start()
    {
        popupPanel.SetActive(false);

        gameManager = GameManager.instance;
        bossController = FindObjectOfType<BossController>();
    }

    private void Update()
    {
        if (gameManager != null && gameManager.testMp >= 100)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isPopupShowing)
                {
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

        // 게임을 일시정지합니다.
        Time.timeScale = 0f;
        isGamePaused = true;

        popupPanel.SetActive(true);
        isPopupShowing = true;

        yield return new WaitForSecondsRealtime(popupDuration);

        popupPanel.SetActive(false);
        isPopupShowing = false;

        // 보스의 체력을 100 감소시킵니다.
        if (bossController != null)
        {
            bossController.TakeDamage(500);
        }

        // 게임을 다시 재개합니다.
        Time.timeScale = 1f;
        isGamePaused = false;

        gameManager.testMp = 0;
    }
}
