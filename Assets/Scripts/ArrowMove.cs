using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowMove : MonoBehaviour {
    [SerializeField]
    float speed = 50;    
	// Use this for initialization
	void Start ()
    {
        //Debug.Log("GG");        
    }
	
	// Update is called once per frame
	void Update ()
    {
        float f = 0.001f;

        gameObject.transform.position += new Vector3(f * speed, 0,0);
        
        
	}

    void OnTriggerEnter ()
    {
        Debug.Log("Crush");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("strike");

        if (collision.gameObject.tag == "Stone")
        {
            Debug.Log("Ground strike");
            Destroy(this.gameObject);
        }
                
    }

}
//void OnCollisionEnter(Collision collision)
//{
//    Debug.Log("strike");

//    if (collision.gameObject.tag == "Ground")
//    {
//        Debug.Log("Ground");
//    }

//    foreach (ContactPoint contact in collision.contacts)
//    {
//        Debug.DrawRay(contact.point, contact.normal, Color.white);            
//    }


//}