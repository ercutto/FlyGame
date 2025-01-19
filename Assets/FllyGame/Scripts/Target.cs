using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.MaterialProperty;
namespace RageRunGames.EasyFlyingSystem
{
    public class Target : MonoBehaviour
    {
        public string player = "Player";
        public Text IndexText;
        public int index = 0;
        public bool destinationReached = false;
        public float scoreAmount = 100;
        public bool isFinalLandingPosition = false;
        public bool isStartingPosition = false;
        public GameObject canvas;
        [Space(5)]
        [Header("PACKET DELIVERY")]
        public bool isPacket = false;
        public GameObject playerObject;
        public GameObject mapIndicator = null;
        public float deliveryTime = 60f;
        
        public bool isDeliveryDestianation = false;
        [TextArea(5,10)]
        public string Adress = "";
        [SerializeField]
        public enum TargetType//görev tipi
        {
            none,
            package,
            packetPikupPosition,
            packetDeliveryPosition,
            destinations,
            startPosition,
            endPosition,
        }
        public TargetType targetType;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(player))
            {
                playerObject = other.gameObject;
                ObjectType((int)targetType,playerObject);

            }


        }


        public void ObjectType(int type,GameObject player)
        {
            switch (type)
            {
                case 0:
                    TargetManager.instance.CheckDestination(index, scoreAmount);
                    break;
                case 1:
                    PackedLifted(playerObject);
                    break;
                case 2:
                    GetComponent<BoxCollider>().enabled = false;
                    Invoke(nameof(CallLiftingStage), 2);
                    break;
                case 3:
                    PackedDelivered();
                    break;
                case 4:
                    break;
                case 5:
                    GetComponent<BoxCollider>().enabled = false;
                    Invoke(nameof(CallLiftingStage), 2);
                    break;
                case 6:
                    GetComponent<BoxCollider>().enabled = false;
                    Invoke(nameof(CallStageWin), 2);
                    break;

            }
        }


        void CallLiftingStage()//drone kalkis
        {
            
            TargetManager.instance.PlayerCouldLiftDrone();

        }
        void CallStageWin()//bölum gecme
        {
            TargetManager.instance.StageWin();
        }

        void PackedLifted(GameObject player)//paket alma
        {
            if (TargetManager.instance.packed == null)
            {
                GetComponent<BoxCollider>().enabled = false;
                mapIndicator.SetActive(false);
                TargetManager.instance.packed = this.gameObject;
                transform.position = player.GetComponent<DroneController>().packedHolder.transform.position;
                transform.SetParent(player.transform.GetComponent<DroneController>().packedHolder.transform);
                //Adress icin gönderilen Mesaj
                TargetManager.instance.PackageDeliveryAddress(index, "");
               
            }

        }

        public void PackedDelivered()//paket teslim
        {
            if (TargetManager.instance.packed != null&& TargetManager.instance.packed.GetComponent<Target>().index==index)
            {
                GetComponent<BoxCollider>().enabled = false;
                mapIndicator.SetActive(false);
                Debug.Log("packed Delivered");
                GameObject packed = TargetManager.instance.packed;
                packed.transform.SetParent(null);
                packed.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                packed.transform.SetParent(this.transform);
                TargetManager.instance.packed = null;
                //StatsManager.instance.MesageText.text = "Message:" + Adress;
                //Adress icin gönderilen Mesaj
                string msg = "paket teslim edilmistir!";
                TargetManager.instance.PackageDeliveredMessage(msg);
            }
        }


    }
}
