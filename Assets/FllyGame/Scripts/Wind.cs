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
            if (NatureManager.instance)
            {
                windArea = NatureManager.instance.windZoneObject;
            }
           
            //Invoke(nameof(SetWindDirectionImage),1);
            
        }
        private void FixedUpdate()
        {
            
            if (!DroneIsUnderCower())
            {
                droneController.windforce = windArea.transform.forward;
                droneController.windSpeed = windArea.GetComponent<WindZone>().windMain;
                
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

        public void SetWindDirectionImage()
        {
           // StatsManager.instance.windDirectionUi.transform.eulerAngles = Vector3.zero;
            if (windDirection == WindDirection.none)
            {
                windDirection = WindDirection.none;
                //StatsManager.instance.windDirectionUi.transform.eulerAngles = new Vector3(0, 0, 0);
            }


            if (windDirection == WindDirection.left)
            {
                raycasterLeft.dir = Vector3.left;
                //StatsManager.instance.windDirectionUi.transform.eulerAngles = new Vector3(0, 0, -90);
            }


            if (windDirection == WindDirection.right)
            {
                raycasterRight.dir = Vector3.right;
               // StatsManager.instance.windDirectionUi.transform.eulerAngles = new Vector3(0, 0, 90);
            }


            if (windDirection == WindDirection.forward)
            {
                raycasterForward.dir = Vector3.forward;
                //StatsManager.instance.windDirectionUi.transform.eulerAngles = new Vector3(0, 0, 0);
            }


            if (windDirection == WindDirection.back)
            {
                raycasterBack.dir = Vector3.back;
               // StatsManager.instance.windDirectionUi.transform.eulerAngles = new Vector3(0, 0, 180);
            }

        }
    }
}
