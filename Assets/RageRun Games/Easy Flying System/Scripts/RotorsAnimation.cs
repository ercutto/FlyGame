using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RageRunGames.EasyFlyingSystem
{
    public class RotorsAnimation : MonoBehaviour
    {
        [FormerlySerializedAs("flyController")] [SerializeField] private DroneController droneController;

        [Header("Rotors Settings")] [SerializeField]
        private RotorsInfo[] rotors;

        private void Awake()
        {
            if (droneController == null)
            {
                droneController = GetComponent<DroneController>();

                if (droneController == null)
                {
                    Debug.LogWarning("Fly controller missing on this object.");
                }
            }
        }

        private void Update()
        {
            HandleRotors();
        }

        private void HandleRotors()
        {
            if (rotors.Length == 0) return;

            for (int i = 0; i < rotors.Length; i++)
            {
                UpdateRotation(rotors[i].rotor);
            }
        }

        private void UpdateRotation(Rotor currentRotor)
        {
            Transform rotorTransform = currentRotor.rotorTransform;

            bool executeRotation = CanRotateOnInput();

            if (executeRotation || !droneController.IsGrounded)
            {
                rotorTransform.Rotate(
                    (currentRotor.inverseRotation ? currentRotor.rotationSpeed : -currentRotor.rotationSpeed) *
                    Time.deltaTime * currentRotor.rotationAxis);
                currentRotor.speed = currentRotor.rotationSpeed;
            }
            else
            {
                float currentSpeed = currentRotor.speed;
                float decelerationRate = 720f; // Adjust as needed

                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decelerationRate * Time.deltaTime);
                
                currentRotor.speed = currentSpeed;
                
                rotorTransform.Rotate((currentRotor.inverseRotation ? currentSpeed : -currentSpeed) * Time.deltaTime * currentRotor.rotationAxis);
            }
        }

        private bool CanRotateOnInput()
        {
            var input = droneController.InputHandler;
            return Mathf.Abs(input.Lift + input.Yaw + input.Pitch + input.Roll) > 0.5f;
        }
    }


    [Serializable]
    public class RotorsInfo
    {
        public Rotor rotor;
    }


    [Serializable]
    public class Rotor
    {
        public Transform rotorTransform;
        public Vector3 rotationAxis;
        [HideInInspector] public float speed;
        public float rotationSpeed = 1500f;
        public bool inverseRotation;
    }
}