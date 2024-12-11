using UnityEngine;

public class CameraSwicth : MonoBehaviour
{
    public GameObject[] cameras = new GameObject[4];
    int i = 1;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            i++;
            if (i == cameras.Length) { i = 0; }

            ChangeCamera(i);
        }


    }

    void ChangeCamera(int i)
    {
        for (int cam = 0; cam < cameras.Length; cam++)
        {
            if (cam == i)
            {
                cameras[cam].SetActive(true);

            }
            else
            {
                cameras[cam].SetActive(false);
            }
        }
    }
}
