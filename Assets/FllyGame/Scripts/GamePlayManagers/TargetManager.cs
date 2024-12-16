using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager instance;
        public Target[] Targets = new Target[10];
        public Camera cam;
        private GameObject player;

        void Start()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            Invoke(nameof(WriteToTargetsUI), 2);

            player = GameObject.FindWithTag("Player");
        }

        void LateUpdate()
        {
            if (cam)
                RotateTergetsUIToCamera();
        }

        public void WriteToTargetsUI()
        {
            cam = GameManager.instance.displayCamera;
            for (int i = 0; i < Targets.Length; i++)
            {

                Targets[i].IndexText.text = (Targets[i].index + 1).ToString();


            }

            return;

        }
        public void CheckDestination(int currentDestination, float score)
        {
            if (currentDestination == 0)
            {
                DestinationAction(Targets[currentDestination], score);

            }
            else
            {
                for (int i = 0; i < Targets.Length; i++)
                {

                    if (!Targets[currentDestination - 1].destinationReached)
                    {

                        return;
                    }
                    else if (Targets[i].index == currentDestination)
                    {
                        DestinationAction(Targets[i], score);

                    }

                    if (i == Targets.Length - 1 && Targets[i].destinationReached)
                    {
                        WinStage("win!!");

                    }
                }
            }

        }
        void DestinationAction(Target currentTarget, float score)
        {
            currentTarget.GetComponent<Collider>().enabled = false;
            StatsManager.instance.AddScore(score);
            currentTarget.destinationReached = true;
            currentTarget.IndexText.text = "Pass";
        }
        void RotateTergetsUIToCamera()
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                GameObject obj = Targets[i].canvas;

                Transform pTransform = player.transform;

                obj.transform.forward = player.transform.forward;

            }
        }

        void WinStage(string msg)
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                Targets[i].IndexText.text = msg;

            }

            StatsManager.instance.AddScore(1000);
        }
    }
}
