using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public interface IInputHandler
    {
        public float Pitch { get; set; }
        
        public float Roll { get; set; }
        
        public float Yaw{ get; set; }
        public float Lift{ get; set; }
        public bool checkInputs { get; set; }

        void HandleInputs();
    }
}