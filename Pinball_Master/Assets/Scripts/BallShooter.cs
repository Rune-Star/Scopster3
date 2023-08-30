using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public float shootSpeed = 10f;
    public LineRenderer lineRenderer;
    Camera cam;
    Vector3 mousePos;
    Vector3 shootDir;
    [SerializeField] float lineWidth;

    bool isActive;

    void Start()
    {
        cam = Camera.main;

        lineRenderer.sharedMaterial.color = new Color(0.32f, 0.75f, 1f);
        lineRenderer.widthMultiplier = lineWidth;
    }

    void Update()
    {
        isActive = Input.GetMouseButton(0);

        if (!isActive) {
            lineRenderer.enabled = false;
            return;
        }


        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        shootDir = (mousePos - transform.position).normalized;

        lineRenderer.enabled = true;
        DrawLine();

        if (Input.GetMouseButtonDown(0)) ShootBall();
    }

    void DrawLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, mousePos);
    }

    // 공을 발사하는 메서드
    void ShootBall()
    {
        // 공의 프리팹을 생성한다
        var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // 공의 리지드바디 컴포넌트를 가져온다
        var rb = ball.GetComponent<Rigidbody2D>();

        // 공에 힘을 가한다
        rb.AddForce(shootDir * shootSpeed, ForceMode2D.Impulse);
    }
}