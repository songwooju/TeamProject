using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    private GameObject target;
    private RaycastHit2D mHit;

    private PlayerMovement player;
    private Vector3 currentPos; // 플레이어의 현재 위치

    int[,] posArray = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // 3x3 배열을 만들어서 타일 위치에 플레이어가 있는지 확인하는 용도
    private int cPos_h; // 배열에서 플레이어의 세로 위치
    private int cPos_w; // 배열에서 플레이어의 가로 위치

    SpriteRenderer spriteColor;
    bool isBossPattern; 
    public GameObject[] Floors; // 바닥을 배열로 받음
    int randomTilePos = 0; // 보스패턴때 랜덤으로 정해질 타일 위치

    public int randomItemPos = 0; // 아이템 생성시 랜덤으로 정해질 위치
    public int randomItem = 0; // 랜덤으로 정해질 아이템
    Vector2 itemPos; // 아이템생성 위치
    public GameObject[] Items = new GameObject[2]; // 아이템 배열로 설정 

    float objectSpwanTime = 0.0f; // 오브젝트 생성 제한 시간 (오브젝트가 없어지고 5초 후에 재생성)
    int objectSpwanCount = 1; // 오브젝트 생성 갯수 제한
    public int currentObjectCount = 0; // 현재 생성된 오브젝트 수, Player가 아이템을 먹으면 0으로 만듦

    public bool itemBuff = false; // 아이템 효과 적용
    float itemBuffTime = 5.0f; // 아이템 효과 적용 시간


    private void Awake() // 싱글톤
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
        Floors = GameObject.FindGameObjectsWithTag("Floor"); // Floor태그를 갖고 있는 오브젝트를 배열로 받아옴.
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

    void Movement() // MouseClick() 에서 받은 player를 이동, 타일 밖을 벗어나지 못하게 제한
    {
        if (currentPos.y < -1 && Input.GetKeyDown(KeyCode.W))
        {
            if (posArray[cPos_h - 1, cPos_w] == 1) // 플레이어가 이동할 곳이 0인지 1인지 판단, 갈 곳이 1이면 이동하지 못함
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

    void c_characterPos() // 플레이어가 있는 위치를 3x3 배열로 표현했을때 1로 바꿔줌, 0이 기본, 1이 현재 위치해 있다는것을 의미.
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

    void p_characterPos() // 캐릭터가 현재위치를 벗어났을 때 배열을 1에서 0으로 바꿔주기 위한 함수
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

    void characterArray() // 처음 캐릭터의 위치를 배열에 표현하기 위한 함수, Initialize함수가 생기면 그 안에 넣어야함.
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
    private void MouseClick() // PlayerMovement가 있는 플레이어를 클릭하면 player로 받아옴
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

        if (!isBossPattern) // 보스패턴이 아니라면 
        {
            randomTilePos = Random.Range(0, 9);
            spriteColor.material.color = Color.white;
        }
        else if (isBossPattern) // 보스패턴이라면
        {
            spriteColor.material.color = Color.red;
        }
    }
    IEnumerator BossPatternRoutine() // 5초 간격으로 보스패턴형성
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

    void ItemRandomPos() // 아이템이 생성될 위치를 정함
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
}
