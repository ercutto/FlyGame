using UnityEngine;

public class CamPosSwitch : MonoBehaviour
{
    LayerMask layerMask;

    [Range(0,20f)] public float distance = 0;
    public Transform orginalPos;
    public Transform pos1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask=LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, layerMask))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.yellow);
            Debug.Log("Did Hit");
            
            //transform.position=orginalPos.position;
            //transform.rotation=orginalPos.rotation;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.white);
            Debug.Log("Did not Hit");
            transform.position = pos1.position;
            transform.rotation = pos1.rotation;
        }
    }
}
