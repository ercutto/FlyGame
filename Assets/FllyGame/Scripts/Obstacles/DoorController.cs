using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class DoorController : MonoBehaviour
    {
        public Animator animator;



        public void OpenDoor()
        {
            animator.SetTrigger("open");

        }
        public void CloseDoor()
        {
            animator.SetTrigger("close");
        }

    }
}
