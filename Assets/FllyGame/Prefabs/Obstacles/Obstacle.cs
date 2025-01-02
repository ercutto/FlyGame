using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Range(0.0f, 60f)]public float rotationX_Speed;
    [Range(0.0f, 90f)]public float rotationX_Length;
    [Range(0.0f, 60f)]public float rotationY_Speed;
    [Range(0.0f, 90f)]public float rotationY_Length;
    
    public GameObject rotator=null;

   
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotator != null) { Petroll(); }
    }

  
    void Petroll()
    {
        rotator.transform.localEulerAngles= new Vector3(Mathf.PingPong(Time.time*rotationX_Speed,rotationX_Length), Mathf.PingPong(Time.time*rotationY_Speed ,rotationY_Length),0);
    }

  
}
