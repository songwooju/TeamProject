using UnityEngine;
using System.Collections;

public class Pattern2 : MonoBehaviour
{
    public Transform[] tiles;
    public GameObject bulletPrefab;

    private float bulletSpeed = 5f;
    private float tileChangeDuration = 1.0f;
    private Color originalColor = Color.white;
    private float distanceThreshold = 0.1f;

    Transform targetTile;
    Vector3 targetPosition;

    public void ShootManager2()
    {
        int randomTileIndex = Random.Range(0, 9);
        targetTile = tiles[randomTileIndex];
        targetPosition = targetTile.position;

        StartCoroutine(ChangeTileColor(targetTile, Color.red));

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = (targetPosition - transform.position).normalized * bulletSpeed;

        StartCoroutine(WaitForBulletArrival(bullet, targetPosition, targetTile));

        StartCoroutine(ChangeTileColor(targetTile, Color.white, tileChangeDuration + 1.0f));
    }

    private IEnumerator ChangeTileColor(Transform tile, Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        tileRenderer.color = color;
    }

    private IEnumerator WaitForBulletArrival(GameObject bullet, Vector3 targetPosition, Transform targetTile)
    {
        while (Vector3.Distance(bullet.transform.position, targetPosition) > distanceThreshold)
        {
            yield return null;
        }

        Destroy(bullet);
        StartCoroutine(ChangeTileColor(targetTile, originalColor));
    }
}