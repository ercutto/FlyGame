using UnityEngine;
using UnityEngine.SceneManagement;
namespace RageRunGames.EasyFlyingSystem
{
    public class ScenesManager : MonoBehaviour
    {
        public static ScenesManager instance;
        public enum _states
        {
            menu,
            game,
        }
        [SerializeField]
        public enum _gameType
        {
            CheckPointGame,
            PackagageDelivery,
            None,
           
        }
        public _gameType gameType;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            DontDestroyOnLoad(instance);
        }
   
       
        public void SelectLevel(int Stage)
        {
            if (GameManager.instance)
            {
                GameManager.instance.state= (GameManager.states)_states.game;
                SceneManager.LoadScene(Stage);
                SceneManager.LoadScene("DisplayScene", LoadSceneMode.Additive);
                GameManager.instance.state = (GameManager.states)_states.game;

                if(Stage == 2|| Stage == 7|| Stage == 8) gameType= _gameType.CheckPointGame;

                if(Stage == 3|| Stage == 5|| Stage == 6) gameType = _gameType.PackagageDelivery;

                
            }
            else
            {
                SceneManager.LoadScene(Stage);
            }
        }

    }
}
