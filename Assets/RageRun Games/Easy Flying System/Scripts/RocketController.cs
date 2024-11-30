using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class RocketController : BaseFlyController
    {
        [SerializeField] private RocketEffectsController rocketEffectsController;
        [SerializeField] private float upwardAngleThreshold = 30f; 
        
        bool isRocketEffectActive;
        
        protected override void HandleRotations()
        {
            currentPitch += InputHandler.Pitch; 
            currentRoll += InputHandler.Roll; 
            
            Quaternion targetRotation = Quaternion.Euler(currentPitch * pitchAmount, 0f, currentRoll * -rollAmount);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed));
        }

        protected override void UpdateMovement(IInputHandler inputHandler)
        {
            base.UpdateMovement(inputHandler);
            
            float lift = inputHandler.Lift;

            if (lift > 0.1f)
            {
                var upwardForce = inputHandler.Lift * maxSpeed * transform.up;
                Vector3 upwardProjectedVel = Vector3.Project(rb.linearVelocity, transform.up);
                rb.linearVelocity = upwardProjectedVel + upwardForce * Time.deltaTime;
                
                rocketEffectsController.StartAllEffects();
                isRocketEffectActive = true;
            }
            else
            {
                
                bool isPointingUpward = Vector3.Dot(transform.up, Vector3.up) > Mathf.Cos(upwardAngleThreshold * Mathf.Deg2Rad);
                
                if(isPointingUpward)
                {
                    var upwardForce = inputHandler.Lift * maxSpeed * 0.25f * transform.up;
                    Vector3 upwardProjectedVel = Vector3.Project(rb.linearVelocity, transform.up);
                    rb.linearVelocity = upwardProjectedVel + upwardForce * Time.deltaTime;
                }
                
                if (!isRocketEffectActive) return;
                rocketEffectsController.StopAllEffects();
                isRocketEffectActive = false;
            }
        }
    }
}