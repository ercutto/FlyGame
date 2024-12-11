using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject defaultPosition;
    public GameObject cameraTarget;
    public GameObject[] cameraLocations;
    public Transform targetLocation;
    float currentSmoothTime;
    float smoothTime = 1f;
    float smoothTimeHurry = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public bool canSeePlayer;



    // Use this for initialization
    public void Start()
    {

        targetLocation = defaultPosition.transform;

    }


    void LateUpdate()
    {

        transform.LookAt(cameraTarget.transform);

        RaycastHit defaultHit;
        Vector3 defaultDirection = defaultPosition.transform.position - cameraTarget.transform.position;
        Debug.DrawRay(cameraTarget.transform.position, defaultDirection, Color.red, 1);
        if (Physics.Raycast(cameraTarget.transform.position, defaultDirection, out defaultHit))
        {
            if (defaultHit.transform.gameObject.tag == "Player")
            {

                Debug.Log("Default näkee");
                targetLocation = defaultPosition.transform;
            }
        }

        if (gameObject.transform.position != targetLocation.position)
        {
            if (canSeePlayer == false)
            {
                currentSmoothTime = smoothTimeHurry;
            }
            else
            {
                currentSmoothTime = smoothTime;
            }
            transform.position = Vector3.SmoothDamp(transform.position, targetLocation.position, ref velocity, currentSmoothTime);
        }
        else
        {
            gameObject.transform.position = targetLocation.position;

        }

        RaycastHit hit;
        Vector3 direction = transform.position - cameraTarget.transform.position;
        Debug.DrawRay(transform.position, -direction, Color.green, 10);
        if (Physics.Raycast(cameraTarget.transform.position, -direction, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                canSeePlayer = true;
                Debug.Log("Can see");
            }
            else
            {
                canSeePlayer = false;
                Debug.Log("Cannot see");
                FindNewTarget();
            }

        }
    }

    void FindNewTarget()
    {
        foreach (GameObject possibleLocation in cameraLocations)
        {
            RaycastHit hit;
            Vector3 direction = possibleLocation.transform.position - cameraTarget.transform.position;
            if (Physics.Raycast(cameraTarget.transform.position, direction, out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    RaycastHit option;
                    Vector3 clearDirection = hit.transform.position - transform.position;
                    if (Physics.Raycast(transform.position, clearDirection, out option))
                    {
                        if (option.transform.gameObject.tag == "Player")
                        {
                            Debug.Log("suora linja");
                            targetLocation = possibleLocation.transform;
                            break;
                        }
                    }


                }
            }
            else
            {
                Debug.Log("Ei sädettä");
            }
        }
    }
}