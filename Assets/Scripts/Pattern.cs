using System.Collections;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public GameObject projectilePrefab;
    float speed = 1f;

    private Vector2[] spawnPoints;

    public Transform[] tiles;
    private float tileChangeDuration = 1.5f;

    int index;
    int tileIndex;

    private void Start()
    {
        spawnPoints = new Vector2[3];

        float leftX = transform.position.x - transform.localScale.x / 2f;
        float rightX = transform.position.x + transform.localScale.x / 2f;
        float bottomY = transform.position.y - transform.localScale.y / 2f;
        float middleY = transform.position.y;
        float topY = transform.position.y + transform.localScale.y / 2f;

        spawnPoints[0] = new Vector2(leftX + transform.localScale.x / 6f, middleY);
        spawnPoints[1] = new Vector2(transform.position.x, middleY);
        spawnPoints[2] = new Vector2(rightX - transform.localScale.x / 6f, middleY);
    }

    public void ShootManager()
    {
        index = Random.Range(0, 3);
        Transform[] targetTile = new Transform[3];

        SwitchUpdate();

        for (int i = 0; i < 3; i++)
        {
            targetTile[i] = tiles[tileIndex + i];
            StartCoroutine(ChangeTileColor(targetTile[i], Color.red));
        }

        GameObject projectile = Instantiate(projectilePrefab, spawnPoints[index], Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, -speed);


        for (int i = 0; i < 3; i++)
        {
            targetTile[i] = tiles[tileIndex + i];
            StartCoroutine(ChangeTileColor(targetTile[i], Color.white, tileChangeDuration));
        }
    }

    IEnumerator ChangeTileColor(Transform tile, Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        tileRenderer.color = color;
    }

    void SwitchUpdate()
    {
        switch (index)
        {
            case 0:
                tileIndex = 0;
                break;
            case 1:
                tileIndex = 3;
                break;
            case 2:
                tileIndex = 6;
                break;
        }
    }
}
