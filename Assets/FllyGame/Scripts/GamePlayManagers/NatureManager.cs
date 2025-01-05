using UnityEngine;

public class NatureManager : MonoBehaviour
{
    public static NatureManager instance;
    public GameObject windZonePrefab = null;
    public GameObject windZoneObject = null;
    public GameObject windDirectionImage = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
       windZoneObject= Instantiate(windZonePrefab,Vector3.zero,Quaternion.identity);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
            windDirectionImage.transform.localEulerAngles = new Vector3(0, 0, windZoneObject.transform.localEulerAngles.y);
        

        //if (windZonePrefab.activeInHierarchy)
        //{
        //    Debug.Log("is active");
        //    //if (!windDirectionImage.activeInHierarchy) {/* windDirectionImage.gameObject.SetActive(true); */}
        //    windDirectionImage.transform.localEulerAngles=new Vector3(0,0,windZonePrefab.transform.localEulerAngles.z);
        //}
        //else
        //{
        //    //windDirectionImage.gameObject.SetActive(false);
        //}
    }

}
