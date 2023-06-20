using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    private GameObject target;
    private RaycastHit2D mHit;

    public int[,] posArray = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // 3x3 �迭�� ���� Ÿ�� ��ġ�� �÷��̾ �ִ��� Ȯ���ϴ� �뵵

    public GameObject[] Floors; // �ٴ��� �迭�� ����

    public int randomItemPos = 0; // ������ ������ �������� ������ ��ġ
    public int randomItem = 0; // �������� ������ ������
    Vector2 itemPos; // �����ۻ��� ��ġ
    public GameObject[] Items = new GameObject[2]; // ������ �迭�� ���� 

    float objectSpwanTime = 0.0f; // ������Ʈ ���� ���� �ð� (������Ʈ�� �������� 5�� �Ŀ� �����)
    int objectSpwanCount = 1; // ������Ʈ ���� ���� ����
    public int currentObjectCount = 0; // ���� ������ ������Ʈ ��, Player�� �������� ������ 0���� ����

    public bool itemBuff = false; // ������ ȿ�� ����
    public bool itemBuffHP = false; // ������ ȿ��, ������ ü�� ����
    float itemBuffTime = 5.0f; // ������ ȿ�� ���� �ð�

    private void Awake() // �̱���
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this) Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        Floors = GameObject.FindGameObjectsWithTag("Floor"); // Floor�±׸� ���� �ִ� ������Ʈ�� �迭�� �޾ƿ�.
    }

    // Update is called once per frame
    void Update()
    {
        objectSpwanTime += Time.deltaTime;

      
        ItemRandomPos();
        ItemSqwanTime();
        ItemBuffTime();
        ItemBuffHP();
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

    void ItemBuffHP() // 2�� �������� �԰� ü���� 2���϶�� ü�� 1 ����
    {
        if (itemBuffHP)
        {
            if (GameManager.instance.sharedCurrentHealth <= 2)
            {
                GameManager.instance.sharedCurrentHealth += 1;
                itemBuffHP = false;
                return;
            }
            else return;
        }
    }
}
