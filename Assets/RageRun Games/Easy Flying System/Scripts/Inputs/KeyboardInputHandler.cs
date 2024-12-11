using System;
using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    [DisallowMultipleComponent]
    public class KeyboardInputHandler : BaseInputHandler, IInputHandler
    {
        [SerializeField] private float lerpSpeed = 5f;
        [SerializeField] private float releaseLerpSpeed = 10f;

        [SerializeField] private KeyInputs keyInputs;

        public void HandleInputs()
        {
            
            if (Input.GetKey(keyInputs.rollRight))
            {
                Roll = Mathf.Lerp(Roll, 1f, Time.deltaTime * lerpSpeed);
            }
            else if (Input.GetKey(keyInputs.rollLeft))
            {
                Roll = Mathf.Lerp(Roll, -1f, Time.deltaTime * lerpSpeed);
            }
            else
            {
                Roll = Mathf.Lerp(Roll, 0f, Time.deltaTime * releaseLerpSpeed);
            }

            // Update cyclic.y based on input keys
            if (Input.GetKey(keyInputs.pitchForward))
            {
                Pitch = Mathf.Lerp(Pitch, 1f, Time.deltaTime * lerpSpeed);
            }
            else if (Input.GetKey(keyInputs.pitchBackward))
            {
                Pitch = Mathf.Lerp(Pitch, -1f, Time.deltaTime * lerpSpeed);
            }
            else
            {
                Pitch = Mathf.Lerp(Pitch, 0f, Time.deltaTime * releaseLerpSpeed);
            }

            // Update pedal based on input keys
            if (Input.GetKey(keyInputs.yawRight))
            {
                Yaw = Mathf.Lerp(Yaw, 1f, Time.deltaTime * lerpSpeed);
            }
            else if (Input.GetKey(keyInputs.yawLeft))
            {
                Yaw = Mathf.Lerp(Yaw, -1f, Time.deltaTime * lerpSpeed);
            }
            else
            {
                Yaw = Mathf.Lerp(Yaw, 0f, Time.deltaTime * releaseLerpSpeed);
            }

            // Update throttle based on input keys
            if (Input.GetKey(keyInputs.liftUp))
            {
                Lift = Mathf.Lerp(Lift, 1f, Time.deltaTime * lerpSpeed);
            }
            else if (Input.GetKey(keyInputs.liftDown))
            {
                Lift = Mathf.Lerp(Lift, -1f, Time.deltaTime * lerpSpeed);
            }
            else
            {
                Lift = Mathf.Lerp(Lift, 0f, Time.deltaTime * releaseLerpSpeed);
                //tuslamayinca sabitleme
                //Lift = 0;
            }
            
            EvaluateAnyKeyDown();
        }
    }
    
    [Serializable]
    public class KeyInputs
    {
        public KeyCode rollLeft = KeyCode.A;
        public KeyCode rollRight = KeyCode.D;
        public KeyCode pitchForward = KeyCode.W;
        public KeyCode pitchBackward = KeyCode.S;

        public KeyCode yawLeft = KeyCode.LeftArrow;
        public KeyCode yawRight = KeyCode.RightArrow;

        public KeyCode liftUp = KeyCode.UpArrow;
        public KeyCode liftDown = KeyCode.DownArrow;
    }
}

