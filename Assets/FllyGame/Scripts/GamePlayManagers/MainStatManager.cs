using RageRunGames.EasyFlyingSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainStatManager : MonoBehaviour
{

    public static MainStatManager instance;
    public float _score = 0;
    public float postmanscore = 0;
    public Text scoreText = null;
    public GameObject canvas = null;
    public Button finishButton = null;
    public Text gameName = null;
    public void Awake()
    {
        //instance = this;
        //DontDestroyOnLoad(instance);

        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }



    public enum Stats
    {
        None,
        Mission,
        Post,
    }
    [SerializeField]
    public Stats stats;

    public void Score(float score, byte gameMode)
    {
        switch (gameMode)
        {
            case 0:
                _score = score;
                gameName.text = "Checkpoint game";
                scoreText.text = _score.ToString();
           
                break;
            case 1:

                gameName.text = "Deliver Game";
                postmanscore = score;
                scoreText.text = postmanscore.ToString();

                break;
            case 2:
               
               
                break;

        }

    }

    public void OpenCanvas()
    {
        
        canvas.SetActive(true);
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(finishButton.gameObject);
    }
    public void CloseCanvas()
    {
        Invoke(nameof(CanvasClosing),1.5f);
    }
    void CanvasClosing()
    {
        scoreText.text = "";
        canvas.SetActive(false);
    }

    public void BacktoMainMenu()
    {
        GameManager.instance.BackToMainMenu();
    }

    
}
