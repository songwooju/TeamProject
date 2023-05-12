using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public GameObject projectilePrefab; // 발사할 총알 프리팹
    public float speed = 10f; // 총알 속도
    public float fireRate = 1f; // 발사 속도
    public float delayBetweenShots = 0.5f; // 발사 사이 딜레이

    private bool canShoot = true; // 발사 가능 여부
    private Vector2[] spawnPoints; // 총알이 나오는 위치 배열

    private void Start()
    {
        spawnPoints = new Vector2[3];
        // 직사각형을 삼등분하여 총알이 나오는 위치 계산
        float leftX = transform.position.x - transform.localScale.x / 2f;
        float rightX = transform.position.x + transform.localScale.x / 2f;
        float bottomY = transform.position.y - transform.localScale.y / 2f;
        float middleY = transform.position.y;
        float topY = transform.position.y + transform.localScale.y / 2f;

        spawnPoints[0] = new Vector2(leftX + transform.localScale.x / 6f, middleY); // 좌측 중앙
        spawnPoints[1] = new Vector2(transform.position.x, middleY); // 중앙
        spawnPoints[2] = new Vector2(rightX - transform.localScale.x / 6f, middleY); // 우측 중앙

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            // 발사 가능할 때까지 대기
            while (!canShoot)
            {
                yield return null;
            }

            // 총알 발사
            for (int i = 0; i < 2; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, spawnPoints[Random.Range(0, 3)], Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(0f, -speed);
            }

            yield return new WaitForSeconds(1f / fireRate);

            // 발사 가능 상태로 변경
            canShoot = true;
        }
    }

    // 총알 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
