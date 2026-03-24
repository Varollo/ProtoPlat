using UnityEngine;

namespace ProtoPlat
{
    public class LimitCameraBounds : MonoBehaviour
    {
        [SerializeField] private Rect cameraLimit = new(-14.222f, -6f, 28.444f, 16f);

        private Camera _cam;

        private void LateUpdate()
        {
            if (!_cam)
                _cam = Camera.main;

            Vector3 camHalfSize = new(
                x: _cam.orthographicSize * _cam.aspect,
                y: _cam.orthographicSize);

            _cam.transform.position = new Vector3(
                x: Mathf.Clamp(_cam.transform.position.x, cameraLimit.xMin + camHalfSize.x, cameraLimit.xMax - camHalfSize.x), 
                y: Mathf.Clamp(_cam.transform.position.y, cameraLimit.yMin + camHalfSize.y, cameraLimit.yMax - camHalfSize.y), 
                z: _cam.transform.position.z);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            var cam = Camera.main;

            var camHalfSize = new Vector3(cam.orthographicSize * cam.aspect, cam.orthographicSize);
            var camPos = cam.transform.position + new Vector3(-camHalfSize.x, -camHalfSize.y);
            
            cameraLimit = new(camPos, camHalfSize * 2);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireCube(cameraLimit.center, cameraLimit.size);
        }
#endif
    }
}
