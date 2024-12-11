using UnityEngine;

public class SendScore : MonoBehaviour
{
    public BoxCollider col=null;
    private Rigidbody rb=null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
     
            
            rb = GetComponentInParent<Rigidbody>();
        
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            ScoreManager.instance.TakeDamage(-10);

        }
    }
}
