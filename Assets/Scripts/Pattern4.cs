using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern4 : MonoBehaviour
{
    int xPos = 0;
    int yPos = 0;
    int randomTileIndex;

    public GameObject[] tilesGameObject;
    GameObject targerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("MakeAnCanNotBeMovedTile", 2f);
    }
    void MakeAnCanNotBeMovedTile()
    {
        ChooseRandomTile();
        StartCoroutine(ToOriginal());

        Invoke("MakeAnCanNotBeMovedTile", 5f);
    }

    private IEnumerator ToOriginal()
    {
        yield return new WaitForSeconds(3f);
        targerGameObject = tilesGameObject[randomTileIndex];
        targerGameObject.GetComponent<Animator>().SetTrigger("canGo");
        Manager.instance.posArray[yPos, xPos] = 0;
    }

    void ChooseRandomTile() // 캐릭터가 가지 못하는 타일을 랜덤으로 정하는 함수
    {
        randomTileIndex = Random.Range(0, 9); // 랜덤한 수를 정함
        CheckPosArray(randomTileIndex); // 정해진 수에 해당하는 3X3 배열을 확인 (플레이어가 겹치지 않게 하기 위해 만든 배열 활용)

        while (Manager.instance.posArray[yPos, xPos] == 1) // 해당 배열의 값이 1이라면 다시 정하도록 함
        {
            randomTileIndex = Random.Range(0, 9);
            CheckPosArray(randomTileIndex);
        }

        targerGameObject = tilesGameObject[randomTileIndex]; // 정해진 수에 해당하는 타일을 
        targerGameObject.GetComponent<Animator>().SetTrigger("can'tGo");
        Manager.instance.posArray[yPos, xPos] = 2;
    }

    void CheckPosArray(int randomTileIndex)
    {
        switch (randomTileIndex)
        {
            case 0:
                xPos = 0;
                yPos = 0;
                break;
            case 1:
                xPos = 0;
                yPos = 1;
                break;
            case 2:
                xPos = 0;
                yPos = 2;
                break;
            case 3:
                xPos = 1;
                yPos = 0;
                break;
            case 4:
                xPos = 1;
                yPos = 1;
                break;
            case 5:
                xPos = 1;
                yPos = 2;
                break;
            case 6:
                xPos = 2;
                yPos = 0;
                break;
            case 7:
                xPos = 2;
                yPos = 1;
                break;
            case 8:
                xPos = 2;
                yPos = 2;
                break;
        }
    }
}
