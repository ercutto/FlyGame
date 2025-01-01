using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
      
        public Camera displayCamera = null;
        
        public void Awake()
        {
            if (instance)
            {
                Destroy(instance.gameObject);
                return;
            }
            instance = this;

            DontDestroyOnLoad(instance.gameObject);

            
            
        }
        public void NextStage()
        {
            ScenesManager.instance.SelectLevel(3);
        }

    }
}
