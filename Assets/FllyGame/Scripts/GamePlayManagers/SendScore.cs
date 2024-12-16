using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class SendScore : MonoBehaviour
    {
        public BoxCollider col = null;
        private Rigidbody rb = null;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        public void SendScoreToScoreManager(float score)
        {
            StatsManager.instance.AddScore(score);
        }


    }
}
