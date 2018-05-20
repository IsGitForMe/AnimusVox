using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region SerializedVar
    [SerializeField]
    private float speed = 50;
    [SerializeField]
    private float jumpSpeed = 2;
    [SerializeField]
    GameObject Lefthand;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private uint arrowReload = 2;
    [SerializeField]
    private uint bombReload = 15;
    [SerializeField]
    private uint ArrowMaxCount = 10;
    [SerializeField]
    GameObject ArrowCountText;
    [SerializeField]
    GameObject BombReloadText;
    [SerializeField]
    GameObject Bomb;
    [SerializeField]
    int bombThrowSpeed = 5;
    [SerializeField]
    int bombDamage = 6;
    [SerializeField]
    int arrowdamage = 1;
    #endregion
    [SerializeField]
    GameObject Bullet;
    GameObject[] Arrows;
    Component rigidBody2D;
    private bool grounded = true;
    int tickArrowBegin = 0;
    int tickBombBegin = 0;
    uint arrowCount = 0;
    private bool _facing = true;
    int _arrowsPoolCount = 3;
    int _arrowPoolPosition = 0;
    // Use this for initialization
    void Start ()
    {
        Arrows = new GameObject[_arrowsPoolCount];
        for (int i = 0;i < Arrows.Length; i++)
        {
            Arrows[i] = Instantiate(Bullet, transform.position, Quaternion.identity);
            Arrows[i].SetActive(false);
        }
        arrowCount = ArrowMaxCount;
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
        tickArrowBegin = Environment.TickCount;
        tickBombBegin = Environment.TickCount;
    }
    private bool IsOnTheGround()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }
    private void RotateHand()
    {

    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
    private bool ViewDirection(float h)
    {
        bool lastFacing = _facing;
        //lastFacing = _facing;

        if (h > 0)
        {
            _facing = true;
        }
        else if (h == 0)
        {

        }
        else
        {
            _facing = false;
        }


        if (_facing != lastFacing)
        {
            Flip();            
        }
        
        if (_facing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void JumpHandler()
    {
        bool jump = Input.GetKeyDown(KeyCode.Space);

        if ((jump) && grounded)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpSpeed);
        }
    }
    private void ArrowPoolHandler(GameObject arr,bool viewDirection)
    {
        _arrowPoolPosition = ++_arrowPoolPosition % Arrows.Length;
        arr.transform.position = gameObject.transform.position;
        arr.GetComponent<ArrowMove>().speed = Bullet.GetComponent<ArrowMove>().speed;
        arr.SetActive(true);
        arr.GetComponent<ArrowMove>().speed = viewDirection ? arr.GetComponent<ArrowMove>().speed : -arr.GetComponent<ArrowMove>().speed;
        arr.GetComponent<ArrowMove>().SetArrowDamaage(arrowdamage);
    }
    private void ShotHandler(bool viewDirection)
    {
        bool shot = Input.GetKeyDown(KeyCode.T);        
        if ((shot) && (arrowCount > 0))
        {
            int tickUpdate = Environment.TickCount;
            if (tickUpdate - tickArrowBegin > arrowReload * 1000)
            {
                GameObject arr = Arrows[_arrowPoolPosition];
                ArrowPoolHandler(arr,viewDirection);
                Debug.Log("view direction is "+viewDirection);                
                tickArrowBegin = tickUpdate;
                arrowCount--;
                ArrowCountText.GetComponent<GuiArrowCount>().ShowArrowCountText(arrowCount);
                Debug.Log(arrowCount);
            }
        }
    }
    private void BombHandler(bool viewDirection)
    {
        bool bombSet = Input.GetKeyDown(KeyCode.Q);
        int viewDir = viewDirection == true ? 1 : -1;
        int tickUpdate = Environment.TickCount;
        if (bombSet)
        {
            if (tickUpdate - tickBombBegin > bombReload * 1000)
            {
                GameObject bomb = Instantiate(Bomb, transform.position, Quaternion.identity);
                bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(bombThrowSpeed * viewDir, bombThrowSpeed);
                bomb.GetComponent<Bomb>().SetBombDamage(bombDamage);
                tickBombBegin = tickUpdate;
            }
        }
        if (tickUpdate - tickBombBegin <= bombReload * 1000)
        {
            BombReloadText.GetComponent<GuiBombReloadText>().BombReloadShowText(bombReload - ((tickUpdate - tickBombBegin) / 1000));            
        }
        else
        {
            BombReloadText.GetComponent<GuiBombReloadText>().BombShowText("Bomb Ready");
        }
    }
    // Update is called once per frame
    void Update ()
    {
        bool hit = Input.GetKeyDown(KeyCode.Mouse0);
        bool viewDirection = true;
        if (hit)
        {
            Lefthand.gameObject.transform.Rotate(new Vector3(0, 0, 90));
        }
        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        grounded = IsOnTheGround();
        float h = Input.GetAxis("Horizontal");
        viewDirection = ViewDirection(h);
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + (h * speed / 100), gameObject.transform.position.y);
        JumpHandler();
        ShotHandler(viewDirection);
        BombHandler(viewDirection);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Quiver")
        {
            Debug.Log("Arrows picked");
            arrowCount = ArrowMaxCount;
            ArrowCountText.GetComponent<GuiArrowCount>().ShowArrowCountText(arrowCount);
            Destroy(collision.gameObject);
        }

    }
}

//bool doEverything = false;

//if (!doEverything)
//{
//    gameObject.transform.rotation.SetLookRotation(Vector3 11);   //(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z, 4);
//}//if (_arrowPoolPosition == Arrows.Length - 1) { _arrowPoolPosition = 0; } else { _arrowPoolPosition++; }//if (!viewDirection)
//{
//    arr.GetComponent<ArrowMove>().speed = -arr.GetComponent<ArrowMove>().speed;
//}
//else
//{
//    arr.GetComponent<ArrowMove>().speed = arr.GetComponent<ArrowMove>().speed;
//}