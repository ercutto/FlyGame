using UnityEngine;

public class Target :MonoBehaviour
{
    public GameObject destination=null;
    public BoxCollider coll=null;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == destination)
        {

            Debug.Log("Destination has been reached!");
            destination.GetComponent<Collider>().enabled = false;
            ScoreManager.instance.AddScore(100);
        }
        
    }

}
