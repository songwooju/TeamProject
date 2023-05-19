using UnityEngine;

public class Pattern2 : MonoBehaviour
{
    public Transform[] tiles; // 타일 배열 (3x3)
    public GameObject bulletPrefab; // 총알 프리팹

    private float bulletSpeed = 5f; // 총알 속도
    private float tileChangeDuration = 1.0f; // 타일 색상 변경 지속 시간
    private Color originalColor = Color.white; // 원래 타일 색상
    private float distanceThreshold = 0.1f; // 타겟 도착 거리 임계값

    private void Start()
    {
        // 총알 발사를 시작합니다.
        ShootBullet();
    }

    private void ShootBullet()
    {
        // 랜덤한 타일 선택
        int randomTileIndex = Random.Range(0, tiles.Length);
        Transform targetTile = tiles[randomTileIndex];

        // 선택된 타일을 2초 동안 빨간색으로 변하게 합니다.
        StartCoroutine(ChangeTileColor(targetTile, Color.red));

        // 타일의 중앙 위치 계산
        Vector3 targetPosition = targetTile.position;

        // 총알 발사
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = (targetPosition - transform.position).normalized * bulletSpeed;

        // 다음 총알 발사 대기 및 총알 도착 체크 코루틴 시작
        StartCoroutine(WaitForBulletArrival(bullet, targetPosition, targetTile));

        // 총알이 도착한 뒤에 타일을 원래 색상으로 변경합니다.
        StartCoroutine(ChangeTileColor(targetTile, originalColor, tileChangeDuration + 1.0f));

        // 다음 총알 발사 대기
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