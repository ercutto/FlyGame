using RageRunGames.EasyFlyingSystem;
using System.Collections;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    
    Vector3 dir = Vector3.zero;
    float a = 0;
    float b = -90;
    float c = 180;
    float d = 90;
    int i = 0;
    private float[] direct=new float[4];
    void Start()
    {
        direct[0] = a;
        direct[1] = b;
        direct[2] = c;
        direct[3] = d;

        InvokeRepeating(nameof(CurrentDir), 4, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CurrentDir()
    {
        
        ChangeDirection( direct[i]);
        i++;
        if (i == direct.Length) { i = 0; }

    }
    void  ChangeDirection(float direction)
    {
        
        
        transform.localEulerAngles = new Vector3(0, direction, 0);
    }
}
