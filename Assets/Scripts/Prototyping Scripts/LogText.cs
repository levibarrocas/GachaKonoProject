using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LogText : MonoBehaviour {
    public static LogText LT;
    public string LogString;

    private int Counter;

	// Use this for initialization
	void Start () {
        LT = GetComponent<LogText>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addToLogText(string Log)
    {
        LogString += Counter + "." + Log;
        Counter++;
        Debug.Log(Log);

    }

    public void ClearLog()
    {
        LogString = "";
        Counter = 0;

    }
}
