using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float time;
    public GameObject prefabBullet;

    private GameManager gameManager;
    private Manager manager;

    bool isMoving; // �����̰� �ִ���
    Vector3 originPos, targetPos; // ���� ��ġ, ��ǥ ��ġ
    float timeToMove = 0.2f; // �̵��ð�

    Vector3 upDirection = new Vector3(0f, 1.1f, 0.0f);
    Vector3 leftDirection = new Vector3(-1.1f, 0f, 0.0f);
    Vector3 downDirection = new Vector3(0f, -1.1f, 0.0f);
    Vector3 rightDirection = new Vector3(1.1f, 0f, 0.0f);

    private Vector3 currentPos; // �÷��̾��� ���� ��ġ
    private int cPos_y; // �迭���� �÷��̾��� ���� ��ġ
    private int cPos_x; // �迭���� �÷��̾��� ���� ��ġ


    private void Start()
    {
        gameManager = GameManager.instance;
        manager = Manager.instance;

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
    }

    private void CheckHealth()
    {
        if (gameManager != null && gameManager.sharedCurrentHealth <= 0)
        {
            Destroy(gameObject);
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

    void cPosTo_1() // �÷��̾ �ִ� ��ġ�� 3x3 �迭�� ǥ�������� 1�� �ٲ���, 0�� �⺻, 1�� ���� ��ġ�� �ִٴ°��� �ǹ�.
    {
        characterArray();
        manager.posArray[cPos_y, cPos_x] = 1;
    }

    void cPosTo_0() // ĳ���Ͱ� ������ġ�� ����� �� �迭�� 1���� 0���� �ٲ��ֱ� ���� �Լ�
    {
        characterArray();
        manager.posArray[cPos_y, cPos_x] = 0;
    }
    void characterArray() // ó�� ĳ������ ��ġ�� �迭�� ǥ���ϱ� ���� �Լ�, Initialize�Լ��� ����� �� �ȿ� �־����.
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
}
