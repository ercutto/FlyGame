using UnityEngine;
using UnityEngine.UI;
namespace RageRunGames.EasyFlyingSystem
{
    public class Target : MonoBehaviour
    {
        public string player = "Player";
        public Text IndexText;
        public int index = 0;
        public bool destinationReached = false;
        public float scoreAmount = 100;
        public GameObject canvas;


        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(player))
            {

                TargetManager.instance.CheckDestination(index, scoreAmount);

            }

        }

    }
}
