using UnityEngine;
using System.Collections;

public class Pattern3 : MonoBehaviour
{
    public Transform[] tiles;
    public GameObject bulletPrefab;

    private float bulletSpeed = 5f;
    private float tileChangeDuration = 1.0f;
    private Color originalColor = Color.white;
    private Color bulletArrivalColor = Color.red;
    private float distanceThreshold = 0.1f;

    Transform[] targetTiles;
    Vector3 targetPosition;

    public void ShootManager3()
    {
        int randomTileIndex = Random.Range(0, 6);

        bool isSquare = (randomTileIndex % 3 != 2) && (randomTileIndex < 6);

        if (isSquare)
        {
            targetTiles = new Transform[4];

            targetTiles[0] = tiles[randomTileIndex];
            targetTiles[1] = tiles[randomTileIndex + 1];
            targetTiles[2] = tiles[randomTileIndex + 3];
            targetTiles[3] = tiles[randomTileIndex + 4];

            targetPosition = (targetTiles[0].position + targetTiles[1].position + targetTiles[2].position + targetTiles[3].position) / 4f;

            StartCoroutine(ChangeTileColor(targetTiles, bulletArrivalColor));

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = (targetPosition - transform.position).normalized * bulletSpeed;

            StartCoroutine(WaitForBulletArrival(bullet, targetPosition, targetTiles));
        }
    }

    private IEnumerator ChangeTileColor(Transform[] tiles, Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        foreach (Transform tile in tiles)
        {
            SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
            if (tileRenderer != null)
            {
                tileRenderer.color = color;
            }
        }
    }

    private IEnumerator WaitForBulletArrival(GameObject bullet, Vector3 targetPosition, Transform[] targetTiles)
    {
        while (bullet != null && Vector3.Distance(bullet.transform.position, targetPosition) > distanceThreshold)
        {
            yield return null;
        }

        if (bullet != null)
        {
            Destroy(bullet);
            StartCoroutine(ChangeTileColor(targetTiles, originalColor));
        }
    }
}
