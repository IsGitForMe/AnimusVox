using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiBombReloadText : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}    
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void BombReloadShowText(float BombReloadTime)
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Bomb reload " + BombReloadTime;
    }
    public void BombShowText(string text)
    {
        gameObject.GetComponent<Text>().text = text;
    }
}
