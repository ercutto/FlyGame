using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStatManager : MonoBehaviour
{

    public static MainStatManager instance;
    public float _score = 0;
    public float postmanscore = 0;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
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

            default:
                break;
            case 1:
                _score = score;
                Debug.Log("Score " + _score);
                break;
            case 2:
                postmanscore = score;
                Debug.Log("postmanscore " + postmanscore);
                break;

        }

    }
}
