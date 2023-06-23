using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    private GameObject target;
    private RaycastHit2D mHit;

    public int[,] posArray = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // 3x3 배열을 만들어서 타일 위치에 플레이어가 있는지 확인하는 용도

    public GameObject[] Floors; // 바닥을 배열로 받음

    public int randomItemPos = 0; // 아이템 생성시 랜덤으로 정해질 위치
    public int randomItem = 0; // 랜덤으로 정해질 아이템
    Vector2 itemPos; // 아이템생성 위치
    public GameObject[] Items = new GameObject[2]; // 아이템 배열로 설정 

    float objectSpwanTime = 0.0f; // 오브젝트 생성 제한 시간 (오브젝트가 없어지고 5초 후에 재생성)
    int objectSpwanCount = 1; // 오브젝트 생성 갯수 제한
    public int currentObjectCount = 0; // 현재 생성된 오브젝트 수, Player가 아이템을 먹으면 0으로 만듦

    public bool itemBuff = false; // 아이템 효과 적용
    public bool itemBuffHP = false; // 아이템 효과, 먹으면 체력 증가
    float itemBuffTime = 5.0f; // 아이템 효과 적용 시간
    public bool itemBuffShield = false; // 먹으면 5초간 무적
    public bool itemBuffMP = false; // 먹으면 마나 증가

    public Transform[] players;

    public bool isBossAttack; // 보스 애니메이션 적용을 위한 변수

    private void Awake() // 싱글톤
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
        Floors = GameObject.FindGameObjectsWithTag("Floor"); // Floor태그를 갖고 있는 오브젝트를 배열로 받아옴.
    }

    // Update is called once per frame
    void Update()
    {
        objectSpwanTime += Time.deltaTime;

      
        ItemRandomPos();
        ItemSqwanTime();
        ItemBuffTime();
        ItemBuffHP();
        ItemBuffShield();
        ItemBuffMP();
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

    void ItemRandomPos() // 아이템이 생성될 위치를 정함
    {
        if (currentObjectCount + 1 > objectSpwanCount) return;

        if (objectSpwanTime > 5.0f)
        {
            randomItem = Random.Range(0, 4);
            randomItemPos = Random.Range(0, 9);
            itemPos = Floors[randomItemPos].transform.position;
            Instantiate(Items[randomItem], itemPos, Quaternion.identity);

            currentObjectCount += 1;
        }
    }

    void ItemSqwanTime() // 아이템 생성 시간
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

    void ItemBuffHP() // 2번 아이템을 먹고 체력이 2이하라면 체력 1 증가
    {
        if (itemBuffHP)
        {
            if (GameManager.instance.sharedCurrentHealth == 3)
            {
                itemBuffHP = false;
                return;
            }
            else if (GameManager.instance.sharedCurrentHealth < 3)
            {
                GameManager.instance.sharedCurrentHealth += 1;
                itemBuffHP = false;
                return;
            }
        }
    }

    void ItemBuffShield()
    {
        if (itemBuffShield)
        {
            itemBuffTime -= Time.deltaTime;

            for (int i = 0; i < 2; i++)
            {
                StartCoroutine(ChangeColor(players[i], Color.yellow));
            }

            if (itemBuffTime <= 0.0f)
            {
                for (int i = 0; i < 2; i++)
                {
                    StartCoroutine(ChangeColor(players[i], Color.white));
                }
                itemBuffShield = false;
                itemBuffTime = 5.0f;
            }
        }
    }

    void ItemBuffMP()
    {
        if (itemBuffMP)
        {
            if (GameManager.instance.testMp <= 80)
            {
                GameManager.instance.testMp += 20;
                itemBuffMP = false;
                return;
            }
            else return;
        }
    }

    IEnumerator ChangeColor(Transform player, Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
        playerRenderer.color = color;
    }
}
