using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player=null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Invoke()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;


    }
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
