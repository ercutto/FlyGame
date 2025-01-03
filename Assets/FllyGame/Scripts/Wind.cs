using System;
using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class Wind : MonoBehaviour
    {
        public Rigidbody rb;
        public DroneController droneController;
        public bool isOnWIndZone = false;
        public GameObject windArea = null;
        public Raycaster raycasterLeft=null;
        public Raycaster raycasterRight=null;
        public Raycaster raycasterForward=null;
        public Raycaster raycasterBack=null;
        [Serializable]
        public enum WindDirection
        {
            none,
            left,
            right,
            back,
            forward,

        }
        
        public WindDirection windDirection;
        
        
        public void Start()
        {
            if(windDirection == WindDirection.none)
                windDirection = WindDirection.none;
            
            if(windDirection == WindDirection.left)
                raycasterLeft.dir=Vector3.left;

            if(windDirection == WindDirection.right)
                raycasterRight.dir=Vector3.right;

            if(windDirection == WindDirection.forward)
                raycasterForward.dir=Vector3.forward;

            if(windDirection == WindDirection.back)
                raycasterBack.dir=Vector3.back;
        }
        private void FixedUpdate()
        {
            
            if (!DroneIsUnderCower())
            {
                droneController.windforce = windArea.transform.forward;
                droneController.windSpeed = windArea.GetComponent<WindZone>().windMain;
                //rb.AddForce(windArea.transform.forward*windArea.GetComponent<WindZone>().windMain, ForceMode.Force);
            }
            else
            {
                droneController.windforce=Vector3.zero;
                droneController.windSpeed = 0;
            }
        }

        public bool DroneIsUnderCower()
        {
            if (raycasterLeft.isUnderCower ) { return true; }
            else
            {
                return false;
            }
           
        }
    }
}
