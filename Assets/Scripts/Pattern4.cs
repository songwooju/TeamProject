using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern4 : MonoBehaviour
{
    public GameObject[] tilesGameObject;
    GameObject targerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("MakeAnCanNotBeMovedTile", 2f);
    }
    void MakeAnCanNotBeMovedTile()
    {
        int index = ChooseRandomTile();
        StartCoroutine(ToOriginal(index));

        Invoke("MakeAnCanNotBeMovedTile", 5f);
    }

    private IEnumerator ToOriginal(int index)
    {
        yield return new WaitForSeconds(3f);
        targerGameObject = tilesGameObject[index];
        targerGameObject.GetComponent<Animator>().SetTrigger("canGo");

        Manager.instance.CheckPosArray(index);
        Manager.instance.ArrayPosTo0();
    }

    int ChooseRandomTile() // 캐릭터가 가지 못하는 타일을 랜덤으로 정하는 함수
    {
        int randomTileIndex = Random.Range(0, tilesGameObject.Length); // 랜덤한 수를 정함
        Manager.instance.CheckPosArray(randomTileIndex);
        while (Manager.instance.IsNotPos0())
        {
            randomTileIndex = Random.Range(0, tilesGameObject.Length);
            Manager.instance.CheckPosArray(randomTileIndex);
        } 

        targerGameObject = tilesGameObject[randomTileIndex]; // 정해진 수에 해당하는 타일을 타겟으로 설정
        targerGameObject.GetComponent<Animator>().SetTrigger("can'tGo");

        Manager.instance.ArrayPosTo1();
        return randomTileIndex;
    }
}
