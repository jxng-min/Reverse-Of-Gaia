using UnityEngine;


namespace Junyoung
{
    public class CameraMoveCtrl : MonoBehaviour
    {
        private Transform m_player_transform;
        private float m_camera_move_speed = 1.0f;

        public Vector2 m_camera_limit_center;
        public Vector2 m_camera_limit_size;

        private float m_camera_height;
        private float m_camera_width;

        void Start()
        {
            m_camera_height = Camera.main.orthographicSize; // 카메라가 화면의 보이는 절반 높이 값
            m_camera_width = m_camera_height * Screen.width / Screen.height; // 카메라의 너비를 구함
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_camera_limit_center, m_camera_limit_size);
        }



        private void Update()
        {
            m_player_transform = GameObject.Find("Player").transform;
        }
    
        private void LateUpdate()
        {
            // 선형보간으로 카메라와 플레이어 사이의 벡터값이 프레임과 일정하게 차이나게끔 하여 천천히 따라오는 효과를 만듬
            transform.position = Vector3.Lerp(transform.position,m_player_transform.position, Time.deltaTime*m_camera_move_speed); 

            float lx = m_camera_limit_size.x * 0.5f - m_camera_width;
            float clampX = Mathf.Clamp(transform.position.x , -lx + m_camera_limit_center.x , lx + m_camera_limit_center.x);

            float ly = m_camera_limit_size.y * 0.5f - m_camera_height;
            float clampY = Mathf.Clamp(transform.position.y, -lx + m_camera_limit_center.y, lx + m_camera_limit_center.y);

            transform.position = new Vector3(clampX, clampY, -10f); // 카메라의 z값을 고정시켜 화면을 이탈하지 않도록

        }
    }


}
