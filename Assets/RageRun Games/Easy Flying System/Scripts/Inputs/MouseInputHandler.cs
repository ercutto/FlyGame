using UnityEngine;
using UnityEngine.Serialization;


namespace RageRunGames.EasyFlyingSystem
{
    public class MouseInputHandler : BaseInputHandler, IInputHandler
    {
        [SerializeField] private float yawSpeed = 5f;
        [SerializeField] private float scrollLiftSpeed = 25f;
        
        [FormerlySerializedAs("lerpSpeed")] [SerializeField] private float inputLerpSpeed = 5f;
        [SerializeField] private float releaseLerpSpeed = 10f;

        [SerializeField] private KeyInputs keyInputs;

        private float mouseX;
        private float targetScrollValue;

        public void HandleInputs()
        {
            if (Input.GetKey(keyInputs.rollRight))
            {
                Roll = Mathf.Lerp(Roll, 1f, Time.deltaTime * inputLerpSpeed);
            }
            else if (Input.GetKey(keyInputs.rollLeft))
            {
                Roll = Mathf.Lerp(Roll, -1f, Time.deltaTime * inputLerpSpeed);
            }
            else
            {
                Roll = Mathf.Lerp(Roll, 0f, Time.deltaTime * releaseLerpSpeed);
            }

            // Update cyclic.y based on input keys
            if (Input.GetKey(keyInputs.pitchForward))
            {
                Pitch = Mathf.Lerp(Pitch, 1f, Time.deltaTime * inputLerpSpeed);
            }
            else if (Input.GetKey(keyInputs.pitchBackward))
            {
                Pitch = Mathf.Lerp(Pitch, -1f, Time.deltaTime * inputLerpSpeed);
            }
            else
            {
                Pitch = Mathf.Lerp(Pitch, 0f, Time.deltaTime * releaseLerpSpeed);
            }

            float mouseX = Input.GetAxis("Mouse X");
            Yaw = Mathf.Lerp(Yaw, mouseX * yawSpeed, Time.deltaTime * inputLerpSpeed);

            
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

            EvaluateAnyKeyDown();
        }
    }
    
    public enum MouseLiftInputType
    {
        Scroll,
        Key
    }
}