using UnityEngine;
using UnityEngine.SceneManagement;
namespace RageRunGames.EasyFlyingSystem
{
    public class ScenesManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneManager.LoadScene("DisplayScene", LoadSceneMode.Additive);
        }

       
    }
}
