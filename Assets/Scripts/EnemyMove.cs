using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    [SerializeField]
    private Vector3 StartPosition;
    [SerializeField]
    private Vector3 EndPosition;
    [SerializeField]
    private int Speed = 20;
    bool forward = true;
    [SerializeField]
    private int MaxHitPoints = 4;
    int realHitPoints = 0;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject Quiver;
    // Use this for initialization
    void Start ()
    {
        StartPosition = transform.position;
        EndPosition = transform.position + new Vector3(5,0,0);
        realHitPoints = MaxHitPoints;
    }
	
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
    public void HitPointsChange(int damage)
    {
        realHitPoints = realHitPoints - damage;
    }
    public void Death(int realHitPoints)
    {
        if (realHitPoints < 1)
        {
            if (explosion != null)
            {
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                Debug.Log("Death");
            }
            GameObject qiv = Instantiate(Quiver, transform.position, Quaternion.identity);
            qiv.tag = "Quiver";
            Debug.Log("LR " + qiv.transform.localEulerAngles);
            var locRotation = qiv.transform.localEulerAngles;
            locRotation.z = 180;
            qiv.transform.localEulerAngles = locRotation;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    private void TurnAndWalk()
    {
        if (forward)
        {
            if ((transform.position.x < EndPosition.x))
            {
                //    transform.position = transform.position + new Vector3(0.001f * Speed, 0, 0);
                transform.position = new Vector3(transform.position.x + 0.001f * Speed, transform.position.y, transform.position.z);
            }
            else
            {
                Flip();
                forward = false;
            }
        }
        else
        {
            if ((transform.position.x > StartPosition.x))
            {
                transform.position = new Vector3(transform.position.x - 0.001f * Speed, transform.position.y, transform.position.z);
            }
            else
            {
                Flip();
                forward = true;
            }
        }
    }
	// Update is called once per frame
	void Update ()
    {
        TurnAndWalk();
        Death(realHitPoints);
    }
    /// <summary>
    /// function of death
    /// </summary>
    /// <param name="collision"></param>
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Debug.Log("strike");
    //    if (collision.gameObject.tag == "Arrow")
    //    {
    //        Destroy(collision.gameObject);
    //        //Debug.Log("Ground strike");
    //    }
    //}
}
