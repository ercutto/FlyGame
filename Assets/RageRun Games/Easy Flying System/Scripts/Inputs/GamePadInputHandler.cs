using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class GamePadInputHandler : BaseInputHandler, IInputHandler
    {
        
        public void HandleInputs()
        {
            //Input.GetAxisRaw("Vertical");
            Pitch = Input.GetAxis("Vertical");

            //Input.GetAxisRaw("Horizontal");
            Roll = Input.GetAxis("Horizontal");

            // -Input.GetAxisRaw("Yaw");
            Yaw = -Input.GetAxis("Yaw");


            // Input.GetAxisRaw("Throttle");
            Lift = Input.GetAxis("Throttle");
            
            EvaluateAnyKeyDown();
        }
    }
}