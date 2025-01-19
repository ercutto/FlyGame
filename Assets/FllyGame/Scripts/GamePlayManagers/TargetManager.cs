using System;
using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager instance;
        public GameObject startPosition;
        public GameObject endPosition;
        public Target[] Targets = new Target[10];
        public Camera cam;

        
        private GameObject player;
        [Header("Package Delivery")]
        public Target[] packegeDeliveryPosition=new Target[2];
        public GameObject packed=null;
        [SerializeField]
        public enum TargetType
        {   none,
            package,
            packetPikupPosition,
            packetDeliveryPosition,
            destinations,
            startPosition,
            endPosition,
        }
        public TargetType targetType;
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
            if (!GameManager.instance)
            {
                cam = Camera.main;
            }
            else
            {
                cam = GameManager.instance.displayCamera;
            }

            for (int i = 0; i < Targets.Length; i++)
            {

                Targets[i].IndexText.text = (Targets[i].index + 1).ToString();


            }

            Targets[0].mapIndicator.SetActive(true);
            return;

        }
        public void CheckDestination(int currentDestination, float score)
        {
            if (currentDestination == 0)
            {
                Targets[1].mapIndicator.SetActive(true);

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

                        if(i < Targets.Length-1)
                        {
                            Targets[i+1].mapIndicator.SetActive(true);
                        }
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
            currentTarget.mapIndicator.SetActive(false);
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

        public void PlayerCouldLiftDrone()
        {
            StatsManager.instance.AddScore(100);
            Debug.Log("Player could Lift!");

        }

        public void StageWin()
        {
            StatsManager.instance.AddScore(10000);
            if (GameManager.instance)
            { GameManager.instance.NextStage(); }
            else
            {
                Debug.Log("StageWin!");
                ScenesManager.instance.SelectLevel(3);
            }
        }

        void WinStage(string msg)
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                Targets[i].IndexText.text = msg;

            }

            StatsManager.instance.AddScore(1000);

            if (GameManager.instance)
            { GameManager.instance.NextStage(); }
            else
            {
               
            }
        }

      
        public void PackageDeliveryAddress(int packageindex,string msg)
        {
            string _message;
            float deliveryTime;
            Target _target = packegeDeliveryPosition[packageindex].GetComponent<Target>();
            _message = _target.Adress;
            deliveryTime = _target.deliveryTime;
            _target.mapIndicator.SetActive(true);
           //_message = packegeDeliveryPosition[packageindex].GetComponent<Target>().Adress;
           // StatsManager.instance.isPackageDelivered = false;
           // deliveryTime = packegeDeliveryPosition[packageindex].GetComponent<Target>().deliveryTime;
            
            

            //StatsManager.instance.MesageText.text=_message;
            StatsManager.instance.WriteMessage(_message+msg);
            StatsManager.instance.TimeCount(deliveryTime);
        }
        public void PackageDeliveredMessage(string msg)
        {
            StatsManager.instance.isPackageDelivered = true;
            StatsManager.instance.WriteMessage(msg);
        }


    }
}
