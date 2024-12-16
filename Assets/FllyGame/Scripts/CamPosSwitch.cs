using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class CamPosSwitch : MonoBehaviour
    {
        LayerMask layerMask;

        [Range(0, 20f)] private float distance = 0;
        private Transform orginalPos;
        private Transform pos1;

        public LayerMask LayerMask { get => layerMask; set => layerMask = value; }
        public float Distance { get => distance; set => distance = value; }
        public Transform OrginalPos { get => orginalPos; set => orginalPos = value; }
        public Transform Pos1 { get => pos1; set => pos1 = value; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            LayerMask = LayerMask.GetMask("Player");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Distance, LayerMask))

            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Distance, Color.yellow);
                Debug.Log("Did Hit");

                //transform.position=orginalPos.position;
                //transform.rotation=orginalPos.rotation;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Distance, Color.white);
                Debug.Log("Did not Hit");
                transform.position = Pos1.position;
                transform.rotation = Pos1.rotation;
            }
        }
    }
}
