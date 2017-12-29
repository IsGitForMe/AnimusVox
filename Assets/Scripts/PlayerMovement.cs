using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed = 50;

    [SerializeField]
    private float jumpSpeed = 2;
    Component rigidBody2D;
    [SerializeField]
    GameObject Lefthand;

    [SerializeField]
    private Transform groundCheck;
    private bool grounded = true;

    [SerializeField]
    GameObject Arrow;

    // Use this for initialization
    void Start ()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        
        Lefthand = GameObject.FindGameObjectWithTag("Hand");
        
        if (Lefthand == null)
        {
            Debug.Log("Hands is not finded");
        }

        groundCheck = transform.Find("floor");

        if (groundCheck == null)
        {
            Debug.Log("Fail");
        }
        
    }
	
    private bool IsOnTheGround()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void RotateHand()
    {

    }
    
	// Update is called once per frame
	void Update ()
    {
        bool hit = Input.GetKeyDown(KeyCode.Mouse0);

        if (hit)
        {
            Lefthand.gameObject.transform.Rotate(new Vector3(0, 0, 90));
        }

        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);

        grounded = IsOnTheGround();

        float h = Input.GetAxis("Horizontal");
        //if (grounded)
        //{
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (h * speed / 100), gameObject.transform.position.y);
        //}

        bool jump = Input.GetKeyDown(KeyCode.Space);
        
        if ((jump)&&grounded)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpSpeed);
        }

        bool shot = Input.GetKeyDown(KeyCode.T);

        if (shot)
        {
            Instantiate(Arrow,transform.position,Quaternion.identity);
        }
    }
}

//bool doEverything = false;

//if (!doEverything)
//{
//    gameObject.transform.rotation.SetLookRotation(Vector3 11);   //(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z, 4);
//}