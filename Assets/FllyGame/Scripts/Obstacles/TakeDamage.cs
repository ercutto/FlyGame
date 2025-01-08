using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class TakeDamage : MonoBehaviour
    {
        public bool player = false;
        public bool isGround=false;
        public DroneController droneController = null;


        public void Damage(float damageAmount)
        {
            if (player)
            {
                if (!droneController.IsGrounded) { 
                    StatsManager.instance.TakeDamage(damageAmount, true);
                }
            }

        }
        private void OnCollisionEnter(Collision collision)
        {
            Damage(10);

        }
    }
}
