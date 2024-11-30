using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public abstract class BaseInputHandler : MonoBehaviour
    {
        public float Pitch { get; set; }
        
        public float Roll { get; set; }
        
        public float Yaw { get; set; }
        public float Lift { get; set; }
        
        public bool checkInputs { get; set; }

        public void EvaluateAnyKeyDown()
        {
            checkInputs = Mathf.Abs(Pitch) <= 0.05f && Mathf.Abs(Lift) <= 0.05f && Mathf.Abs(Yaw) <= 0.05f && Mathf.Abs(Roll) <= 0.05f;
        }
    }
}