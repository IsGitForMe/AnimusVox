using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField]
    private float TimeExplosion = 3;
    [SerializeField]
    private float RadiusExplosion = 5;    
    private bool explode = false;
    private int bombDamage = 6;
    int tickBegin = 0;
    [SerializeField]
    AudioClip bomb;
    // Use this for initialization
    void Start ()
    {
        tickBegin = Environment.TickCount;
	}
    public void SetBombDamage(int bombInputDamage)
    {
        bombDamage = bombInputDamage;
    }
	// Update is called once per frame
	void Update ()
    {
        int tickUpdate = Environment.TickCount;
        if (tickUpdate - tickBegin > TimeExplosion * 1000)
        {
            explode = true;
            tickBegin = tickUpdate;
        }
        if (explode)
        {
            AudioSource.PlayClipAtPoint(bomb, transform.position);
            Collider2D[] players;
            players = Physics2D.OverlapCircleAll(transform.position, RadiusExplosion);
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].tag == "Enemy")
                {
                    players[i].GetComponent<EnemyMove>().HitPointsChange(bombDamage);
                }
                Debug.Log(players[i].name);
            }
            explode = false;
            Destroy(gameObject);
        }
    }
}
