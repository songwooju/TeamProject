using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float time;
    public GameObject prefabBullet;
    public GameObject GameOverUI;

    //public bool isGamePaused = false;


    private GameManager gameManager;
    private Manager manager;
    private Animator animator;

    bool isMoving; // 움직이고 있는지
    Vector3 originPos, targetPos; // 원래 위치, 목표 위치
    float timeToMove = 0.2f; // 이동시간

    Vector3 upDirection = new Vector3(0f, 1.1f, 0.0f);
    Vector3 leftDirection = new Vector3(-1.1f, 0f, 0.0f);
    Vector3 downDirection = new Vector3(0f, -1.1f, 0.0f);
    Vector3 rightDirection = new Vector3(1.1f, 0f, 0.0f);

    private Vector3 currentPos; // 플레이어의 현재 위치
    private int cPos_y; // 배열에서 플레이어의 세로 위치
    private int cPos_x; // 배열에서 플레이어의 가로 위치


    private void Start()
    {
        gameManager = GameManager.instance;
        manager = Manager.instance;
        animator = GetComponent<Animator>();

        if (gameManager != null)
        {
            gameManager.sharedCurrentHealth = gameManager.sharedMaxHealth;
        }

        time = 0;
        characterArray();
        cPosTo_1();
    }


    void Update()
    {
        FireBullet();
        CheckHealth();
        PlayerMove();
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
        if (gameManager != null)
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
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Item2"))
        {
            if (manager != null)
            {
                manager.currentObjectCount = 0;
                manager.itemBuffHP = true;
            }
            Destroy(collision.gameObject);
        }
    }

    private void CheckHealth()
    {
        if (gameManager != null && gameManager.sharedCurrentHealth <= 0)
        {
            animator.SetBool("IsDie", true);
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        cPosTo_0();

        float escapeTime = -0.1f;

        originPos = transform.position;
        targetPos = originPos + direction;

        while (escapeTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (escapeTime / timeToMove));
            escapeTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        cPosTo_1();
        isMoving = false;
    }

    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isMoving && currentPos.y < -1)
        {
            if (manager.posArray[cPos_y - 1, cPos_x] >= 1) return;
            StartCoroutine(Move(upDirection));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving && currentPos.x > -1)
        {
            if (manager.posArray[cPos_y, cPos_x - 1] >= 1) return;
            StartCoroutine(Move(leftDirection));
        }
        if (Input.GetKey(KeyCode.DownArrow) && !isMoving && currentPos.y > -2)
        {
            if (manager.posArray[cPos_y + 1, cPos_x] >= 1) return;
            StartCoroutine(Move(downDirection));
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isMoving && currentPos.x < 0.5)
        {
            if (manager.posArray[cPos_y, cPos_x + 1] >= 1) return;
            StartCoroutine(Move(rightDirection));
        }
    }

    void cPosTo_1() // 플레이어가 있는 위치를 3x3 배열로 표현했을때 1로 바꿔줌, 0이 기본, 1이 현재 위치해 있다는것을 의미.
    {
        characterArray();
        manager.posArray[cPos_y, cPos_x] = 1;
    }

    void cPosTo_0() // 캐릭터가 현재위치를 벗어났을 때 배열을 1에서 0으로 바꿔주기 위한 함수
    {
        characterArray();
        manager.posArray[cPos_y, cPos_x] = 0;
    }
    void characterArray() // 처음 캐릭터의 위치를 배열에 표현하기 위한 함수, Initialize함수가 생기면 그 안에 넣어야함.
    {
        currentPos = this.gameObject.transform.position;
        if (currentPos.y > -1)
        {
            cPos_y = 0;
            if (currentPos.x < -1)
                cPos_x = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                cPos_x = 1;
            else if (currentPos.x > 0)
                cPos_x = 2;
        }
        else if (-2 < currentPos.y && currentPos.y < -1)
        {
            cPos_y = 1;
            if (currentPos.x < -1)
                cPos_x = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                cPos_x = 1;
            else if (currentPos.x > 0)
                cPos_x = 2;
        }
        else
        {
            cPos_y = 2;
            if (currentPos.x < -1)
                cPos_x = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                cPos_x = 1;
            else if (currentPos.x > 0)
                cPos_x = 2;
        }
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.8f);

        Time.timeScale = 0f;
        GameOverUI.SetActive(true);
    }

    public void OnGameOverButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FirstScene");
    }
}

