using RageRunGames.EasyFlyingSystem;
using UnityEngine;
using UnityEngine.EventSystems;


public class MiniMenu : MonoBehaviour
{
    
    public GameObject miniMenuObject;

    public void OnPause()
    {
        miniMenuObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void OnPlay()
    {
        miniMenuObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void ReturnMainMenu()
    {
        Time.timeScale = 1.0f;
        
        GameManager.instance.BackToMainMenu();
       
    }
}
