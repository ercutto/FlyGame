using RageRunGames.EasyFlyingSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour
{
    public bool isTesting = false;
   
    
    public Wind wind=null;

   
    public GameObject[]drones=new GameObject[6];
    int i = 0;
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
            SwicthDrone();
        }
    }

    void SwicthDrone()
    {
        drones[i].SetActive(false);
        i++;
        if(i > 5)
        {
            i = 0;
            drones[i].SetActive(true);

        }
        else
        {
            drones[i].SetActive(true) ;
        }

       
    }
}
