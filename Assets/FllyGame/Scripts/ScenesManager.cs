using UnityEngine;
using UnityEngine.SceneManagement;
namespace RageRunGames.EasyFlyingSystem
{
    public class ScenesManager : MonoBehaviour
    {
        public static ScenesManager instance;

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
