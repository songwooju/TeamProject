using UnityEngine;
using System.Collections;

public class Pattern2 : MonoBehaviour
{
    public Transform[] tiles;
    public GameObject bulletPrefab;

    private float bulletSpeed = 5f;
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
    }

    private IEnumerator ChangeTileColor(Transform tile, Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        if (tileRenderer != null)
        {
            tileRenderer.color = color;
        }
    }

    private IEnumerator WaitForBulletArrival(GameObject bullet, Vector3 targetPosition, Transform targetTile)
    {
        while (bullet != null && Vector3.Distance(bullet.transform.position, targetPosition) > distanceThreshold)
        {
            yield return null;
        }

        if (bullet != null)
        {
            GameManager.instance.isAvoidPattern = true;
            Destroy(bullet);
            StartCoroutine(ChangeTileColor(targetTile, originalColor));
        }
    }
}