using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour {

    [SerializeField]
    GameObject EndPoint;

    public int CountFrame = 0;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CountFrame++;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //int tickUpdate = Environment.TickCount;
        if (CountFrame < 120 || EndPoint.GetComponent<PortalTeleport>().CountFrame < 120)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = EndPoint.transform.position + new Vector3(0.5f,0,0);
            Debug.Log("Tp");
            CountFrame = 0;
            EndPoint.GetComponent<PortalTeleport>().CountFrame = 0;
        }

    }
}
