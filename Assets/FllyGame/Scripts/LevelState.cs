using UnityEngine;
using RageRunGames.EasyFlyingSystem;
using UnityEngine.UI;

public class LevelState : MonoBehaviour
{
    public int levelInt = 0;
    public Image image;
    public bool isOpen=false;
    public GameObject keypadOpen=null;
    public GameObject keypadClose=null;
    public Text scoretext = null;


    public void Unlock()
    {
        
        GetComponent<Button>().enabled = true;
        keypadClose.gameObject.SetActive(false);
        keypadOpen.gameObject.SetActive(true);
        Color color = Color.white;
        image.color = color;
        isOpen = true;
        
    }
    public void Lock()
    {
        //DeActivate();
        GetComponent<Button>().enabled = false;
        keypadClose.gameObject.SetActive(true);
        keypadOpen.gameObject.SetActive(false);
        Color color = Color.gray;
        image.color = color;
        isOpen = false;
        


    }
    public void InvokeDeActivate()
    {
        DeActivate();
    }

    void DeActivate()
    {
        gameObject.SetActive(false);
    }
   
    public void InvokeActivate()
    {
        Invoke(nameof(Activate), 0.5f);
    }
    void Activate()
    {
        gameObject.SetActive(true);
    }
}
