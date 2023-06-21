using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArcherWarriorUlt : MonoBehaviour
{
    public GameObject popupPanel;
    public float popupDuration = 3f;
    public GameObject bossObject;
    public int damageAmount = 100;

    private bool isPopupShowing = false;

    private void Start()
    {
        popupPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPopupShowing)
            {
                StartCoroutine(ShowPopup());
            }
        }
    }

    private IEnumerator ShowPopup()
    {
        popupPanel.SetActive(true);
        isPopupShowing = true;

        yield return new WaitForSecondsRealtime(popupDuration);

        popupPanel.SetActive(false);
        isPopupShowing = false;

        if (bossObject != null)
        {
            BossController bossHealth = bossObject.GetComponent<BossController>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damageAmount);
            }
        }
    }
}
