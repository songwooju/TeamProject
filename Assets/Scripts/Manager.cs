using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private GameObject target;
    private RaycastHit2D mHit;

    private PlayerMovement player;
    private Vector3 currentPos; // 플레이어의 현재 위치

    int[,] posArray = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // 3x3 배열을 만들어서 타일 위치에 플레이어가 있는지 확인하는 용도
    private int cPos_h; // 배열에서 플레이어의 세로 위치
    private int cPos_w; // 배열에서 플레이어의 가로 위치

    private void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        MouseClick();
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
}
