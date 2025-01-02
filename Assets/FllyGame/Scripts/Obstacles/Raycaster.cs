using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class Raycaster : MonoBehaviour
    {
        RaycastHit[] m_Results = new RaycastHit[5];

        [Range(0.0f, 10.0f)] public float raycastDistance;
        LayerMask player;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            player = LayerMask.GetMask("Player");
        }
        public void Update()
        {
            
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            RaycastsOne();

        }
        void RaycastsOne()
        {
            int hits = Physics.RaycastNonAlloc(transform.position, transform.forward, m_Results, raycastDistance, player);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastDistance, Color.yellow);
            for (int i = 0; i < hits; i++)
            {
               Debug.Log("Camera sees player");
            }

        }

    }
}
