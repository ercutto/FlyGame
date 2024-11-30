using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class MobileInputHandler : BaseInputHandler, IInputHandler
    {
        [SerializeField] private MobileController pitchAndRollController;
        [SerializeField] private MobileController liftAndYawController;


        public void SetMobileInputControllers(MobileController[] controllers)
        {
            pitchAndRollController = controllers[0];
            liftAndYawController = controllers[1];
        }


        public void HandleInputs()
        {
            Pitch = pitchAndRollController.Vertical;
            Roll = pitchAndRollController.Horizontal;

            Yaw = liftAndYawController.Horizontal;
            Lift = liftAndYawController.Vertical;
            
            EvaluateAnyKeyDown();
        }


    }
}

