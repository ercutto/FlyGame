using RageRunGames.EasyFlyingSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour
{
    public bool isTesting = false;
   
    
    public Wind wind=null;
    void Awake()
    {
        if (isTesting)
        {
            SceneManager.LoadScene("DisplayScene", LoadSceneMode.Additive);
            
            
            
        }


    }

  
}
