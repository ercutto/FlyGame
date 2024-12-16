using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        //public GameObject player=null;
        public Camera displayCamera = null;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            DontDestroyOnLoad(gameObject);


        }
        public void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
