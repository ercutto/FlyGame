using System;
using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class PlayerNavigation : MonoBehaviour
    {
        public Transform drone=null;
        private Vector3 dronePosition=Vector3.zero  ;
        [Range(0,20f)]public float miniMapCameraDistance;
        float droneX = 0;
        float droneY = 0;
        float droneheigth;
        float droneRotation = 0;
    
        void Start()
        {
            
            droneX = drone.position.x;
            droneY = drone.position.z;
            droneRotation = drone.eulerAngles.y;
            transform.position = new Vector3(droneX, miniMapCameraDistance, droneY);
            
        }

        void LateUpdate()
        {
            

            droneX = drone.position.x;
            droneY = drone.position.z;
            droneheigth = drone.position.y;
            droneRotation = drone.eulerAngles.y;
            transform.position = new Vector3(droneX,droneheigth+ miniMapCameraDistance, droneY);
            transform.rotation = Quaternion.Euler(90, droneRotation, 0f);
           
        }
    }
}
