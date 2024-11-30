using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    [DisallowMultipleComponent]
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset = new Vector3(0f, 2f, -5f);
        private Vector3 currentVel;

        public bool ignoreLookAt;

        private void Awake()
        {
            if (target == null)
            {
                target = FindFirstObjectByType<DroneController>().transform;

                if (target == null)
                {
                    Debug.LogWarning(
                        "Fly controller missing in the scene. Please add it in scene and assign target reference on MainCamera");
                }
            }
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = Vector3.zero;

            if (ignoreLookAt)
            {
                desiredPosition = target.position + offset;
            }
            else
            {
                desiredPosition = target.position + Quaternion.Euler(0f, target.eulerAngles.y, 0f) * offset;
            }

            Vector3 smoothedPosition =
                Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVel, smoothSpeed);

            transform.position = smoothedPosition;

            if (ignoreLookAt) return;
            transform.LookAt(target);
        }

        public CameraController SetTarget(Transform target)
        {
            this.target = target;
            return this;
        }

        public void SetOffset(Vector3 offset)
        {
            this.offset = offset;
        }
    }
}