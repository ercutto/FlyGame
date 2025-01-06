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
        public bool isFinalLandingPosition=false;
        public bool isStartingPosition=false;
        public GameObject canvas;


        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(player))
            {
                if (isStartingPosition)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    Invoke(nameof(CallLiftingStage),2);
                    
                }

                if (isFinalLandingPosition)
                {
                    GetComponent<BoxCollider>().enabled = false;
                   Invoke(nameof(CallStageWin),2);
                }

                if(!isStartingPosition&&!isFinalLandingPosition)
                {
                    TargetManager.instance.CheckDestination(index, scoreAmount);
                }
                

            }

        }

        void CallLiftingStage()
        {
            //Buraya pervanelerin durmasini beklemeyi eklemek gerekiyor
            TargetManager.instance.PlayerCouldLiftDrone();

        }
        void CallStageWin()
        {
            TargetManager.instance.StageWin();
        }
    }

       
}
