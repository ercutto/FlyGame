using RageRunGames.EasyFlyingSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour
{
    public bool isTesting = false;
    public int stage = 0;
    void Awake()
    {
        if (isTesting)
            SceneManager.LoadScene("DisplayScene",LoadSceneMode.Additive);

    }

  
}
