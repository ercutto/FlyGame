using RageRunGames.EasyFlyingSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour
{
    public bool isTesting = false;
   
    
    public Wind wind=null;

    public GameObject drone=null;
    public GameObject drone2 = null;
    void Awake()
    {
        if (isTesting)
        {
            SceneManager.LoadScene("DisplayScene", LoadSceneMode.Additive);
            
            
            
        }


    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            drone.SetActive(false);
            drone2.SetActive(true);
        }
    }
}
