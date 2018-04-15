using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExitButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    public void ButtonClickExit()
    {
        Debug.Log("something");
        //Application.Quit();
        //EditorApplication.isPlaying = false;   
        Application.LoadLevel("OpeningScene");
    }
}
