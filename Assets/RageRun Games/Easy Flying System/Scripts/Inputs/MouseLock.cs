using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class MouseLock : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}