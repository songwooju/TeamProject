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

    private bool canShoot = true;

    private void Start()
    {
        StartPattern();
    }

    public void StartPattern()
    {
        canShoot = true;
        StartCoroutine(ShootPattern());
    }

    public void StopPattern()
    {
        canShoot = false;
        StopAllCoroutines();
    }

    private IEnumerator ShootPattern()
    {
        while (canShoot)
        {
            int randomTileIndex = Random.Range(0, tiles.Length);
            Transform targetTile = tiles[randomTileIndex];

            StartCoroutine(ChangeTileColor(targetTile, Color.red));

            Vector3 targetPosition = targetTile.position;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = (targetPosition - transform.position).normalized * bulletSpeed;

            StartCoroutine(WaitForBulletArrival(bullet, targetPosition, targetTile));

            yield return new WaitForSeconds(tileChangeDuration + 2f);

            StartCoroutine(ChangeTileColor(targetTile, originalColor));

            yield return new WaitForSeconds(tileChangeDuration);
        }
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