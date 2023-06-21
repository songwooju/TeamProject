using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern4 : MonoBehaviour
{
    public Transform[] tiles;
    Transform targetTile;

    int xPos = 0;
    int yPos = 0;
    int randomTileIndex;

    public Sprite[] tileSprite;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("MakeAnCanNotBeMovedTile", 2f);
    }
    void MakeAnCanNotBeMovedTile()
    {
        ChooseRandomTile();
        StartCoroutine(ToOriginal(targetTile));

        Invoke("MakeAnCanNotBeMovedTile", 5f);
    }

    private IEnumerator ToOriginal(Transform tile)
    {
        yield return new WaitForSeconds(3f);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        if (tileRenderer != null)
        {
            tileRenderer.sprite = tileSprite[randomTileIndex];
        }
        Manager.instance.posArray[yPos, xPos] = 0;
    }

    void ChooseRandomTile()
    {
        randomTileIndex = Random.Range(0, 9);
        CheckPosArray(randomTileIndex);

        while (Manager.instance.posArray[yPos, xPos] == 1)
        {
            randomTileIndex = Random.Range(0, 9);
            CheckPosArray(randomTileIndex);
        }

        targetTile = tiles[randomTileIndex];
        SpriteRenderer tileRenderer = targetTile.GetComponent<SpriteRenderer>();
        tileRenderer.sprite = tileSprite[9];
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
