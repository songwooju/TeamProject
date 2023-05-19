using UnityEngine;

public class Pattern2 : MonoBehaviour
{
    public Transform[] tiles; // Ÿ�� �迭 (3x3)
    public GameObject bulletPrefab; // �Ѿ� ������

    private float bulletSpeed = 5f; // �Ѿ� �ӵ�
    private float tileChangeDuration = 2f; // Ÿ�� ���� ���� ���� �ð�
    private Color originalColor = Color.white; // ���� Ÿ�� ����

    private void Start()
    {
        // �Ѿ� �߻縦 �����մϴ�.
        ShootBullet();
    }

    private void ShootBullet()
    {
        // ������ Ÿ�� ����
        int randomTileIndex = Random.Range(0, tiles.Length);
        Transform targetTile = tiles[randomTileIndex];

        // ���õ� Ÿ���� 2�� ���� ���������� ���ϰ� �մϴ�.
        StartCoroutine(ChangeTileColor(targetTile, Color.red));

        // Ÿ���� �߾� ��ġ ���
        Vector3 targetPosition = targetTile.position + new Vector3(0.5f, 0.5f, 0f);

        // �Ѿ� �߻�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = (targetPosition - transform.position).normalized * bulletSpeed;

        // �Ѿ��� ������ �ڿ� Ÿ���� ���� �������� �����մϴ�.
        StartCoroutine(ChangeTileColor(targetTile, originalColor, tileChangeDuration + 2f));

        // ���� �Ѿ� �߻� ���
        Invoke("ShootBullet", tileChangeDuration + 2f);
    }

    private System.Collections.IEnumerator ChangeTileColor(Transform tile, Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        tileRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ˰� �浹�� ��� �Ѿ� ����
        if (collision.CompareTag("Floor"))
        {
            Destroy(collision.gameObject);
        }
    }
}