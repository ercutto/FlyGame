using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class MouseMovementInputHandler : BaseInputHandler, IInputHandler
    {
        [SerializeField] private MouseLiftInputType liftInputType;
        [SerializeField] float _deadZoneRadius = 0.1f;

        [Header("Scroll Lift Settings - Requires MouseLiftInputType.Scroll to be selected")]
        [SerializeField] private float scrollLiftSpeed = 25f;
        [SerializeField] private float inputLerpSpeed = 5f;
        private float targetScrollValue;

        
        [SerializeField] private KeyInputs keyInputs;

        Vector2 ScreenCenter => new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        
        public void HandleInputs()
        {
            Vector2 mousePosition = Input.mousePosition;
            
            float calculatedPitch = (mousePosition.y - ScreenCenter.y) / ScreenCenter.y;
            float calculatedRoll = (mousePosition.x - ScreenCenter.x) / ScreenCenter.x;
            
            Pitch = Mathf.Abs(calculatedPitch) > _deadZoneRadius ? calculatedPitch : 0f;
            Roll = Mathf.Abs(calculatedRoll) > _deadZoneRadius ? calculatedRoll : 0f;
            
            switch (liftInputType)
            {
                case MouseLiftInputType.Scroll:
                    if (Input.mouseScrollDelta.y > 0)
                    {
                        targetScrollValue = 1f * scrollLiftSpeed;
                    }
                    else if (Input.mouseScrollDelta.y < 0)
                    {
                        // Lift = Mathf.Lerp(Lift, scrollInput * scrollLiftSpeed, Time.deltaTime * lerpSpeed);
                        targetScrollValue = -1f * scrollLiftSpeed;
                    }
                    else
                    {
                        targetScrollValue = 0f;
                    }
            
                    Lift = Mathf.Lerp(Lift, targetScrollValue, Time.deltaTime * inputLerpSpeed);
                    break;
                case MouseLiftInputType.Key:
                    Lift = Input.GetKey(keyInputs.liftUp) ? 1f : (Input.GetKey(keyInputs.liftDown) ? -1f : 0f);
                    break;
            }
            
            
            Yaw = Input.GetKey(keyInputs.yawRight) ? 1f : (Input.GetKey(keyInputs.yawLeft) ? -1f : 0f);
        }
    }
}