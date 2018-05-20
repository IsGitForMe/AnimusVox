using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSctipt : MonoBehaviour {

    private bool _pause = false;

    enum WorkState: int
    {
        work = 1,
        rest = 2,
        vacation = 4
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    if (_pause == true)
        //    {
        //        _pause = false;
        //        Time.timeScale = 1;
        //        Debug.Log("Pause");
        //    }
        //    else
        //    {
        //        _pause = true;
        //        Time.timeScale = 0;
        //        Debug.Log("Pause");
        //    }           
        //}

        if (Input.GetKeyDown(KeyCode.P))
        {
            _pause = !_pause;
            Time.timeScale = _pause == true ? 1 : 0;
        }
    }
}
