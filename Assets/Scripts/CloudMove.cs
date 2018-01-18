using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    [SerializeField]
    private Vector3 StartPozition = new Vector3(-8,7,-1);

    [SerializeField]
    private Vector3 EndPozition = new Vector3(66, 7,-1);

    [SerializeField]
    private int Speed = 20;

	// Use this for initialization
	void Start ()
    {
        transform.position = StartPozition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x < EndPozition.x)
        {
            transform.position = transform.position + new Vector3(0.001f * Speed,0,0);
        }
        else
        {
            transform.position = StartPozition;
        }
    }
}
