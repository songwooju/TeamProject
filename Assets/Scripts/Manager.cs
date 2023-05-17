using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    private GameObject target;
    private RaycastHit2D mHit;

    private PlayerMovement player;
    private Vector3 currentPos; // �÷��̾��� ���� ��ġ

    int[,] posArray = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // 3x3 �迭�� ���� Ÿ�� ��ġ�� �÷��̾ �ִ��� Ȯ���ϴ� �뵵
    private int cPos_h; // �迭���� �÷��̾��� ���� ��ġ
    private int cPos_w; // �迭���� �÷��̾��� ���� ��ġ

    SpriteRenderer spriteColor;
    bool isBossPattern; 
    public GameObject[] Floors; // �ٴ��� �迭�� ����
    int randomTilePos = 0; // �������϶� �������� ������ Ÿ�� ��ġ

    public int randomItemPos = 0; // ������ ������ �������� ������ ��ġ
    public int randomItem = 0; // �������� ������ ������
    Vector2 itemPos; // �����ۻ��� ��ġ
    public GameObject[] Items = new GameObject[2]; // ������ �迭�� ���� 

    float objectSpwanTime = 0.0f; // ������Ʈ ���� ���� �ð� (������Ʈ�� �������� 5�� �Ŀ� �����)
    int objectSpwanCount = 1; // ������Ʈ ���� ���� ����
    public int currentObjectCount = 0; // ���� ������ ������Ʈ ��, Player�� �������� ������ 0���� ����

    public bool itemBuff = false; // ������ ȿ�� ����
    float itemBuffTime = 5.0f; // ������ ȿ�� ���� �ð�


    private void Awake() // �̱���
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        isBossPattern = false;
        Floors = GameObject.FindGameObjectsWithTag("Floor"); // Floor�±׸� ���� �ִ� ������Ʈ�� �迭�� �޾ƿ�.
    }

    // Update is called once per frame
    void Update()
    {
        objectSpwanTime += Time.deltaTime;

        Movement();
        MouseClick();
        BossPattern();
        StartCoroutine(BossPatternRoutine());
        ItemRandomPos();
        ItemSqwanTime();
        ItemBuffTime();
    }

    void Movement() // MouseClick() ���� ���� player�� �̵�, Ÿ�� ���� ����� ���ϰ� ����
    {
        if (currentPos.y < -1 && Input.GetKeyDown(KeyCode.W))
        {
            if (posArray[cPos_h - 1, cPos_w] == 1) // �÷��̾ �̵��� ���� 0���� 1���� �Ǵ�, �� ���� 1�̸� �̵����� ����
                return;
            p_characterPos();
            player.transform.Translate(0, 1.1f, 0);
            currentPos = player.gameObject.transform.position;
            c_characterPos();
        }
        else if (currentPos.y > -2 && Input.GetKeyDown(KeyCode.S))
        {
            if (posArray[cPos_h + 1, cPos_w] == 1)
                return;
            p_characterPos();
            player.transform.Translate(0, -1.1f, 0);
            currentPos = player.gameObject.transform.position;
            c_characterPos();

        }
        else if (currentPos.x > -1 && Input.GetKeyDown(KeyCode.A))
        {
            if (posArray[cPos_h, cPos_w - 1] == 1)
                return;
            p_characterPos();
            player.transform.Translate(-1.1f, 0, 0);
            currentPos = player.gameObject.transform.position;
            c_characterPos();
        }
        else if (currentPos.x < 0.5 && Input.GetKeyDown(KeyCode.D))
        {
            if (posArray[cPos_h, cPos_w + 1] == 1)
                return;
            p_characterPos();
            player.transform.Translate(1.1f, 0, 0);
            currentPos = player.gameObject.transform.position;
            c_characterPos();
        }
    }

    void c_characterPos() // �÷��̾ �ִ� ��ġ�� 3x3 �迭�� ǥ�������� 1�� �ٲ���, 0�� �⺻, 1�� ���� ��ġ�� �ִٴ°��� �ǹ�.
    {
        characterArray();
        if (currentPos.y > -1)
        {
            if (currentPos.x < -1)
                posArray[0, 0] = 1;
            else if (-1 < currentPos.x && currentPos.x < 0)
                posArray[0, 1] = 1;
            else if (currentPos.x > 0)
                posArray[0, 2] = 1;
        }
        else if( -2 < currentPos.y && currentPos.y < -1)
        {
            if (currentPos.x < -1)
                posArray[1, 0] = 1;
            else if (-1 < currentPos.x && currentPos.x < 0)
                posArray[1, 1] = 1;
            else if (currentPos.x > 0)
                posArray[1, 2] = 1;
        }
        else
        {
            if (currentPos.x < -1)
                posArray[2, 0] = 1;
            else if (-1 < currentPos.x && currentPos.x < 0)
                posArray[2, 1] = 1;
            else if (currentPos.x > 0)
                posArray[2, 2] = 1;
        }
    }

    void p_characterPos() // ĳ���Ͱ� ������ġ�� ����� �� �迭�� 1���� 0���� �ٲ��ֱ� ���� �Լ�
    {
        characterArray();
        if (currentPos.y > -1)
        {
            if (currentPos.x < -1)
                posArray[0, 0] = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                posArray[0, 1] = 0;
            else if (currentPos.x > 0)
                posArray[0, 2] = 0;
        }
        else if (-2 < currentPos.y && currentPos.y < -1)
        {
            if (currentPos.x < -1)
                posArray[1, 0] = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                posArray[1, 1] = 0;
            else if (currentPos.x > 0)
                posArray[1, 2] = 0;
        }
        else
        {
            if (currentPos.x < -1)
                posArray[2, 0] = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                posArray[2, 1] = 0;
            else if (currentPos.x > 0)
                posArray[2, 2] = 0;
        }
    }

    void characterArray() // ó�� ĳ������ ��ġ�� �迭�� ǥ���ϱ� ���� �Լ�, Initialize�Լ��� ����� �� �ȿ� �־����.
    {
        cPos_h = 0;
        cPos_w = 0;
        if (currentPos.y > -1)
        {
            cPos_h = 0;
            if (currentPos.x < -1)
                cPos_w = 0;
            else if ( -1 < currentPos.x && currentPos.x < 0)
                cPos_w = 1;
            else if (currentPos.x > 0)
                cPos_w = 2;
        }
        else if (-2 < currentPos.y && currentPos.y < -1)
        {
            cPos_h = 1;
            if (currentPos.x < -1)
                cPos_w = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                cPos_w = 1;
            else if (currentPos.x > 0)
                cPos_w = 2;
        }
        else
        {
            cPos_h = 2;
            if (currentPos.x < -1)
                cPos_w = 0;
            else if (-1 < currentPos.x && currentPos.x < 0)
                cPos_w = 1;
            else if (currentPos.x > 0)
                cPos_w = 2;
        }
    }

    private GameObject GetClickedObject()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mHit = Physics2D.Raycast(worldPoint, Vector2.zero, 10.0f);

        if (mHit.collider != null)
        {
            target = mHit.collider.gameObject;
        }
        return target;
    }
    private void MouseClick() // PlayerMovement�� �ִ� �÷��̾ Ŭ���ϸ� player�� �޾ƿ�
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            if (target.CompareTag("Player"))
            {
                player = target.GetComponent<PlayerMovement>();
                currentPos = player.gameObject.transform.position;
                characterArray();
            }
        }
    }

    void BossPattern()
    {
        spriteColor = Floors[randomTilePos].gameObject.GetComponent<SpriteRenderer>();

        if (!isBossPattern) // ���������� �ƴ϶�� 
        {
            randomTilePos = Random.Range(0, 9);
            spriteColor.material.color = Color.white;
        }
        else if (isBossPattern) // ���������̶��
        {
            spriteColor.material.color = Color.red;
        }
    }
    IEnumerator BossPatternRoutine() // 5�� �������� ������������
    {
        if (isBossPattern)
        {
            yield return new WaitForSeconds(5.0f);
            isBossPattern = false;
        }
        else
        {
            yield return new WaitForSeconds(5.0f);
            isBossPattern = true;
        }
    }

    void ItemRandomPos() // �������� ������ ��ġ�� ����
    {
        if (currentObjectCount + 1 > objectSpwanCount) return;

        if (objectSpwanTime > 5.0f)
        {
            randomItem = Random.Range(0, 2);
            randomItemPos = Random.Range(0, 9);
            itemPos = Floors[randomItemPos].transform.position;
            Instantiate(Items[randomItem], itemPos, Quaternion.identity);

            currentObjectCount += 1;
        }
    }

    void ItemSqwanTime() // ������ ���� �ð�
    {
        if (currentObjectCount + 1 > objectSpwanCount)
        {
            objectSpwanTime = 0.0f;
        }
    }

    void ItemBuffTime()
    {
        if (itemBuff)
        {
            itemBuffTime -= Time.deltaTime;
            if (itemBuffTime <= 0.0f)
            {
                itemBuff = false;
                itemBuffTime = 5.0f;
            }
        }
    }
}
