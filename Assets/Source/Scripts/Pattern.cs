using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public GameObject projectilePrefab; // �߻��� �Ѿ� ������
    public float speed = 10f; // �Ѿ� �ӵ�
    public float fireRate = 1f; // �߻� �ӵ�
    public float delayBetweenShots = 0.5f; // �߻� ���� ������

    private bool canShoot = true; // �߻� ���� ����
    private Vector2[] spawnPoints; // �Ѿ��� ������ ��ġ �迭

    private void Start()
    {
        spawnPoints = new Vector2[3];
        // ���簢���� �����Ͽ� �Ѿ��� ������ ��ġ ���
        float leftX = transform.position.x - transform.localScale.x / 2f;
        float rightX = transform.position.x + transform.localScale.x / 2f;
        float bottomY = transform.position.y - transform.localScale.y / 2f;
        float middleY = transform.position.y;
        float topY = transform.position.y + transform.localScale.y / 2f;

        spawnPoints[0] = new Vector2(leftX + transform.localScale.x / 6f, middleY); // ���� �߾�
        spawnPoints[1] = new Vector2(transform.position.x, middleY); // �߾�
        spawnPoints[2] = new Vector2(rightX - transform.localScale.x / 6f, middleY); // ���� �߾�

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            // �߻� ������ ������ ���
            while (!canShoot)
            {
                yield return null;
            }

            // �Ѿ� �߻�
            for (int i = 0; i < 2; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, spawnPoints[Random.Range(0, 3)], Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(0f, -speed);
            }

            yield return new WaitForSeconds(1f / fireRate);

            // �߻� ���� ���·� ����
            canShoot = true;
        }
    }

    // �Ѿ� �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
