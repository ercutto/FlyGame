using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class Raycaster : MonoBehaviour
    {
        RaycastHit[] m_Results = new RaycastHit[5];

        [Range(0.0f, 30.0f)] public float raycastDistance;
        LayerMask currentLayer;

        public string layerTocheck;
        public bool isSecurityCamera = false;
        public bool cameraSeesPlayer = false;
        public bool isUnderCower = false;
        public bool isForWind = false;
       
        public Vector3 dir= Vector3.zero;   

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentLayer = LayerMask.GetMask(layerTocheck);


        }
 
        void FixedUpdate()
        {
            if (isSecurityCamera)
            {
                RaycastCamera();
            }
            else
            {
                WindCheck(dir);
            }
                

        }
        void RaycastCamera()
        {

            int hits = Physics.RaycastNonAlloc(transform.position, transform.forward, m_Results, raycastDistance, currentLayer);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastDistance, Color.yellow);



            for (int i = 0; i < hits; i++)
            {



                cameraSeesPlayer = true;
                SecurityCameraCheck();



            }

            if (hits == 0)
                cameraSeesPlayer = false;

            SecurityCameraCheck();




        }

        public void WindCheck(Vector3 dir)
        {

            int hits = Physics.RaycastNonAlloc(transform.position, dir, m_Results, raycastDistance, currentLayer);
            Debug.DrawRay(transform.position, dir * raycastDistance, Color.yellow);




            for (int i = 0; i < hits; i++)
            {

                isUnderCower = true;

                Debug.Log("Ruzgar Yok!!");

            }


            if (hits == 0)
            {
                isUnderCower = false;
                Debug.Log("Ruzgar var!!");
            }



            
        }

        void SecurityCameraCheck()
        {
            Debug.Log("camera  " + cameraSeesPlayer);
        }
    }
}
