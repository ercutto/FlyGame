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
            }
            else
            {
                SceneManager.LoadScene(Stage);
            }
        }

    }
}
