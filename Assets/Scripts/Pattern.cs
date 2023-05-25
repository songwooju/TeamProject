using System.Collections;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int damage = 1;
    public float speed = 10f;
    public float fireRate = 1f;
    public float delayBetweenShots = 0.5f;
    public float destroyTime = 1.0f;
    public float Duration;

    private bool canShoot = true;
    private Vector2[] spawnPoints;

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

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            while (!canShoot)
            {
                yield return null;
            }

            for (int i = 0; i < 2; i++)
            {
                if (projectilePrefab != null)
                {
                    GameObject projectile = Instantiate(projectilePrefab, spawnPoints[Random.Range(0, 3)], Quaternion.identity);
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(0f, -speed);

                    Destroy(projectile, destroyTime);
                }
            }

            yield return new WaitForSeconds(delayBetweenShots);

            canShoot = true;
        }
    }

    public void StartPattern()
    {
        canShoot = true;
        StartCoroutine(Shoot());
    }

    public void StopPattern()
    {
        canShoot = false;
    }
}
