using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityUtils.Projectile
{
    public class Crosshair : MonoBehaviour
    {
        #region Obs

        public Vector2 mousePos;
        public Camera _camera;
        public GameObject _obj;
        PointerEventData _eventData;
        Vector2 mousPos;

        // void Awake()
        // {
        //     _eventData = new PointerEventData(EventSystem.current);
        //     mousePos = Input.mousePosition;
        // }
        // void Update()
        // {
        //     Move();
        // }

        public void Move()
        {
            var dir = (Vector2) _obj.transform.position - mousePos;
            _obj.transform.position = dir;
            _obj.transform.rotation = Quaternion.Euler(dir);
            _obj.transform.rotation.Normalize();
        }

        // public void OnDrag(PointerEventData eventData)
        // {
        //     var dir = (Vector2) _obj.transform.position - eventData.position;
        //     _obj.transform.position = dir;
        //     _obj.transform.rotation = Quaternion.Euler(dir);
        //     _obj.transform.rotation.Normalize();
        // }

        public void GetMousePos(PointerEventData eventData) { }

        #endregion

        [SerializeField] Transform _transform;
        Camera m_cam;

        void Awake() { m_cam = Camera.main; }
        void LookAtMouse()
        {
            var mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mousePos - _transform.position;

            _transform.right = dir;
        }

        void Update()
        {
            LookAtMouse();

            if (Input.GetMouseButtonDown(0)) {
                var a = transform.rotation;
                var rt = Quaternion.Euler(new Vector3(a.x + 90, a.y + 90, a.z));

                var endPos = m_cam.ScreenToWorldPoint(Input.mousePosition);
                var dir = endPos - transform.position;

                var bullet
                    = Bullet.Create(GameManager.Instance.projectilePrefabs.group.transform,
                        m_cam.ScreenToWorldPoint(GetComponent<RectTransform>().position),
                        Quaternion.identity) as Bullet;
                bullet.Rigid.AddForce(dir.normalized * bullet.speed * Time.deltaTime);
            }
        }
    }
}