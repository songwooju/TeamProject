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

    void ChooseRandomTile() // ĳ���Ͱ� ���� ���ϴ� Ÿ���� �������� ���ϴ� �Լ�
    {
        randomTileIndex = Random.Range(0, 9); // ������ ���� ����
        CheckPosArray(randomTileIndex); // ������ ���� �ش��ϴ� 3X3 �迭�� Ȯ�� (�÷��̾ ��ġ�� �ʰ� �ϱ� ���� ���� �迭 Ȱ��)

        while (Manager.instance.posArray[yPos, xPos] == 1) // �ش� �迭�� ���� 1�̶�� �ٽ� ���ϵ��� ��
        {
            randomTileIndex = Random.Range(0, 9);
            CheckPosArray(randomTileIndex);
        }

        targerGameObject = tilesGameObject[randomTileIndex]; // ������ ���� �ش��ϴ� Ÿ���� 
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
