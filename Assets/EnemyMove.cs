﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    [SerializeField]
    private Vector3 StartPozition = new Vector3(21.4f, 6.88f, 0);

    [SerializeField]
    private Vector3 EndPozition = new Vector3(27.8f, 6.88f, 0);

    [SerializeField]
    private int Speed = 20;

    bool forward = true;

    [SerializeField]
    private int MaxHitPoints = 4;

    int realHitPoints = 0;

    [SerializeField]
    GameObject explosion;

    // Use this for initialization
    void Start ()
    {
        transform.position = StartPozition;
        realHitPoints = MaxHitPoints;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (forward)
        {
            if ((transform.position.x < EndPozition.x))
            {
                transform.position = transform.position + new Vector3(0.001f * Speed, 0, 0);
            }
            else
            {
                forward = false;
            }
        }
        else
        {
            if ((transform.position.x > StartPozition.x))
            {
                transform.position = transform.position + new Vector3(-0.001f * Speed, 0, 0);
            }
            else
            {
                forward = true;
            }
        }
        

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("strike");

        if (collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            //Debug.Log("Ground strike");
            realHitPoints--;
            Debug.Log(realHitPoints);
            if (realHitPoints < 1)
            {
                if (explosion != null)
                {
                    Instantiate(explosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                    Debug.Log("Death");
                }                
                Destroy(gameObject);
            }
        }

    }
}
