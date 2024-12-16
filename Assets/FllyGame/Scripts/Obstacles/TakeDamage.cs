using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class TakeDamage : MonoBehaviour
    {
        public bool player = false;


        public void Damage(float damageAmount)
        {
            if (player)
            {

                StatsManager.instance.TakeDamage(damageAmount, true);
            }

        }
        private void OnCollisionEnter(Collision collision)
        {
            Damage(10);

        }
    }
}
