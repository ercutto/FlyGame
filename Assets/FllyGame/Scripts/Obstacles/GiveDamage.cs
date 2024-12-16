using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class GiveDamage : MonoBehaviour
    {
        public bool contiouslyDamage = false;
        public float damageAmount = 5f;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<TakeDamage>() != null)
            {

                other.gameObject.GetComponent<TakeDamage>().Damage(damageAmount);
            }
        }
    }
}
