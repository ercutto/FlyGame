using System;
using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class TargetManager : MonoBehaviour
    {

        public static TargetManager instance;
        [Header("Flying And Landing positions")]
        public GameObject startPosition;
        public GameObject endPosition;
        [Header("__________________________________________")]

        [Space(4)]
        [Header("CheckPoint Game")]
        public GameObject CheckPointsGameObject = null;
        public Target[] Targets = new Target[10];

        [Header("__________________________________________")]

        [HideInInspector]
        public Camera cam;

        

        private GameObject player;
        [Header("Package Delivery")]
        public GameObject packageGameObject = null;
        public Target[] packegeDeliveryPosition=new Target[2];
        [Header("__________________________________________")]

        [Header("Packages")]
        public Target[] packages=new Target[2];
        [HideInInspector]
        public GameObject packed=null;
        [HideInInspector]
        public int packageIndex = 0;
        public int sceneIndex = 0;

        [Header("__________________________________________")]

        //sonra sill
        public PlayerNavigation playerNavigation;

       

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
        
        [Header("__________________________________________")]
        [Header("Select target mode")]
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
            StartGamTargetMode((int)ScenesManager.instance.gameType);
        }

        void LateUpdate()
        {
            if (cam)
                RotateTergetsUIToCamera();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StageWin();
            }
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

                        CheckPointGameWin("win!!");

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
            { //GameManager.instance.NextStage(3);
                GameManager.instance.SceneInt=sceneIndex+1;
                MainStatManager.instance.OpenCanvas();
                //GameManager.instance.BackToMainMenu();
            }
            else
            {
                Debug.Log("StageWin!");
                ScenesManager.instance.SelectLevel(3);
            }
        }

        void CheckPointGameWin(string msg)
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                Targets[i].IndexText.text = msg;

            }

            StatsManager.instance.AddScore(1000);

            if (GameManager.instance)
            { GameManager.instance.NextStage(2); }
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
            StatsManager statManager= StatsManager.instance;
            statManager.isPackageDelivered = false;
            statManager.WriteMessage("paket kaldirildi!", 0);
            statManager.WriteMessage(_message + msg, 1);
            statManager.TimeCount(deliveryTime);
           
        }
        public void PackageDeliveredMessage(string msg)
        {
            StatsManager.instance.isPackageDelivered = true;
            StatsManager.instance.WriteMessage(msg,1);
        }


        public void StartGamTargetMode(int gamemode)
        {
            switch (gamemode)
            {
                case 0:
                    CheckPointGameStart();
                    break;
                case 1:
                    CheckPointsGameObject.SetActive(false);
                    packageGameObject.SetActive(true);
                    Invoke(nameof(PackageDeliveryGameStart),2);
                    break;


                default:
                    break;
            }
        }

        void CheckPointGameStart()
        {
            CheckPointsGameObject.SetActive(true);
            packageGameObject.SetActive(false);
        }

        public void PackageDeliveryGameStart()
        {

            if (packageIndex==packages.Length)
            {
                StatsManager.instance.WriteMessage("Tebrikler bugunku siparisler bitti!...", 0);
                
                StageWin();
                return;
            }
            else
            {
              
                Invoke(nameof(NewPackageOrdered),2);

            }
           
        }

        public void NewPackageOrdered()
        {
            StatsManager.instance.WriteMessage("yeni siparis geldi!...", 0);
            packages[packageIndex].gameObject.SetActive(true);
        }
    }
}
