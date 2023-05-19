using UnityEngine;

public class Pattern2 : MonoBehaviour
{
    public Transform[] tiles; // Ÿ�� �迭 (3x3)
    public GameObject bulletPrefab; // �Ѿ� ������

    private float bulletSpeed = 5f; // �Ѿ� �ӵ�
    private float tileChangeDuration = 1.0f; // Ÿ�� ���� ���� ���� �ð�
    private Color originalColor = Color.white; // ���� Ÿ�� ����
    private float distanceThreshold = 0.1f; // Ÿ�� ���� �Ÿ� �Ӱ谪

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
        Vector3 targetPosition = targetTile.position;

        // �Ѿ� �߻�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = (targetPosition - transform.position).normalized * bulletSpeed;

        // ���� �Ѿ� �߻� ��� �� �Ѿ� ���� üũ �ڷ�ƾ ����
        StartCoroutine(WaitForBulletArrival(bullet, targetPosition, targetTile));

        // �Ѿ��� ������ �ڿ� Ÿ���� ���� �������� �����մϴ�.
        StartCoroutine(ChangeTileColor(targetTile, originalColor, tileChangeDuration + 1.0f));

        // ���� �Ѿ� �߻� ���
        Invoke("ShootBullet", tileChangeDuration + 2f);
    }

    private System.Collections.IEnumerator ChangeTileColor(Transform tile, Color color, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        tileRenderer.color = color;
    }

    private System.Collections.IEnumerator WaitForBulletArrival(GameObject bullet, Vector3 targetPosition, Transform targetTile)
    {
        while (Vector3.Distance(bullet.transform.position, targetPosition) > distanceThreshold)
        {
            yield return null;
        }

        Destroy(bullet);
        StartCoroutine(ChangeTileColor(targetTile, originalColor));
    }
}