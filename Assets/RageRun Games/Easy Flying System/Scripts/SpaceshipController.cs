using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class SpaceshipController : BaseFlyController
    {
        public enum FlightMethod { Velocity, Force, SideWaysForce }
        
        [Header("Flight Settings")]
        [SerializeField] FlightMethod flightMethod = FlightMethod.Velocity;
        
        [SerializeField] Transform bodyTransform;
        
        
        protected override void HandleRotations()
        {
            base.HandleRotations();
            
            Quaternion currentRotation = Quaternion.Euler(currentPitch, currentYaw, currentRoll);
            rb.MoveRotation(currentRotation);
            
            if (Input.GetKey(KeyCode.E))
            {
                bodyTransform.Rotate(new Vector3(bodyTransform.localEulerAngles.x, bodyTransform.localEulerAngles.y, -360f * Time.deltaTime), Space.Self);
            } 
            else if (Input.GetKey(KeyCode.Q))
            {
                bodyTransform.Rotate(new Vector3(bodyTransform.localEulerAngles.x, bodyTransform.localEulerAngles.y, 360f * Time.deltaTime), Space.Self);
            } 
            else
            {
                bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, transform.rotation, Time.deltaTime * 5);
            }
        }

        protected override void UpdateMovement(IInputHandler inputHandler)
        {
            if (autoForwardMovement)
            {                
                Rb.AddForce(maxSpeed * transform.forward, ForceMode.Force);
                return;
            }
            
            var forwardForce = inputHandler.Lift * maxSpeed * transform.forward;
            
            // apply boost with shift
            if (Input.GetKey(KeyCode.Space))
            {
                forwardForce *= 10f;
            }
            
            switch (flightMethod)
            {
                case FlightMethod.Velocity:
                    Vector3 forwardVelocity = Vector3.Project(Rb.linearVelocity, transform.forward);
                    Rb.linearVelocity = forwardVelocity + forwardForce * Time.deltaTime;
                    break;
                case FlightMethod.Force:
                    Rb.AddForce(forwardForce, ForceMode.Force);
                    break;
                case FlightMethod.SideWaysForce:
                    Rb.AddForce(forwardForce, ForceMode.Force);
                    Vector3 sideForce = inputHandler.Roll * maxSpeed * Vector3.right;
                    Rb.AddForce(sideForce, ForceMode.Force);
                    break;
            }
            
            Rb.linearVelocity = Vector3.ClampMagnitude(Rb.linearVelocity, maxSpeed);
        }
    }

   
}