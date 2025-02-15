using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    const float moveStep = 1.1f;

    float time;
    public GameObject prefabBullet;
    public GameObject GameOverUI;

    public AudioSource gameSound;

    private GameManager gameManager;
    private Manager manager;
    private Animator animator;

    bool isMoving; // 움직이고 있는지
    Vector3 originPos, targetPos; // 원래 위치, 목표 위치
    float timeToMove = 0.2f; // 이동시간


    AudioSource audioSrc;
    private void Start()
    {
        gameManager = GameManager.instance;
        manager = Manager.instance;
        animator = GetComponent<Animator>();
        GameOverUI.SetActive(false);

        if (gameManager != null)
        {
            gameManager.sharedCurrentHealth = gameManager.sharedMaxHealth;
        }

        time = 0;

        manager.CheckCharacterArray(this.transform.position);
        manager.ArrayPosTo1();

        audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
    }


    void Update()
    {
        FireBullet();
        CheckHealth();
    }

    public void FireBullet()
    {
        time += Time.deltaTime;

        if (time > 0.3f)
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity);
            time -= 0.5f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (gameManager != null && !manager.itemBuffShield)
        {
            gameManager.sharedCurrentHealth -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (manager != null)
            {
                manager.currentObjectCount = 0;
                manager.itemBuff = true;
                audioSrc.Play();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Item2"))
        {
            if (manager != null)
            {
                manager.currentObjectCount = 0;
                manager.itemBuffHP = true;
                audioSrc.Play();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Item3"))
        {
            if (manager != null)
            {
                manager.currentObjectCount = 0;
                manager.itemBuffShield = true;
                audioSrc.Play();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Item4"))
        {
            if (manager != null)
            {
                manager.currentObjectCount = 0;
                manager.itemBuffMP = true;
                audioSrc.Play();
            }
            Destroy(collision.gameObject);
        }
    }

    private void CheckHealth()
    {
        if (gameManager != null && gameManager.sharedCurrentHealth <= 0)
        {
            animator.SetBool("IsDie", true);
            gameSound.volume = 0.0f;
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;
        manager.CheckCharacterArray(transform.position);
        manager.ArrayPosTo0();

        float escapeTime = -0.1f;

        originPos = transform.position;
        targetPos = targetPosition;

        while (escapeTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (escapeTime / timeToMove));
            escapeTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        manager.CheckCharacterArray(transform.position);
        manager.ArrayPosTo1();
        isMoving = false;
    }

    void PlayerMove(int deltaX, int deltaY)
    {
        Vector3 currentPos = this.transform.position;
        manager.CheckCharacterArray(currentPos);

        int arrayTargetX = manager.XPos + deltaX;
        int arrayTargetY = manager.YPos + deltaY;

        if (manager.IsNotPos0(arrayTargetX, arrayTargetY)) return; // arrayTarget 이 0이 아니라면 return

        float targetX = currentPos.x + (deltaX * moveStep); // x 는 int 형인데 이렇게 해도 되는지 모르겠음. 확실하게 하는게 좋음
        float targetY = currentPos.y + (deltaY * moveStep); // currentPos y 는 아래로 내려갈 때 - 를 해줘야함. 근데 deltaY 는 위로 올라갈 때 - 를 해줌. 위에서부터 0 1 2 라서

        if (targetX < -2 || targetX > 1.5f || targetY < -3 || targetY > 1) return;
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        StartCoroutine(Move(targetPosition));
    }

    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.OnSwipe += HandleSwipe;
        }

    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
            TouchManager.Instance.OnSwipe -= HandleSwipe;
    }

    void HandleSwipe(Player touchedCharacter, Vector2 swipeDirection)
    {
        if (touchedCharacter == this)
        {
            int deltaX = 0;
            int deltaY = 0;
   
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y)) // 수평, 수직 스와이프 중 어느 쪽의 크기가 큰지에 따라 이동 방향 결정
            {
                deltaX = swipeDirection.x > 0 ? 1 : -1;  // 수평 이동: 오른쪽이면 +1, 왼쪽이면 -1
            }
            else
            {
                deltaY = swipeDirection.y > 0 ? 1 : -1;  // 수직 이동: 위이면 1, 아래이면 1, 위에서부터 2 1 0
            }

            Debug.Log(deltaX + ", " + deltaY);
            if (!isMoving) PlayerMove(deltaX, deltaY); // 해당 캐릭터가 이동하지 않을 때만 이동하게
        }
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.8f);

        Time.timeScale = 0f;
        if (!GameOverUI.activeSelf) GameOverUI.SetActive(true);
    }

    public void OnGameOverButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

