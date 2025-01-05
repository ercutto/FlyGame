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
            raycasterLeft.dir=-windArea.transform.forward;

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

       

        
    }
}
