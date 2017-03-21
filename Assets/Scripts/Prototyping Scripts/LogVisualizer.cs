using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogVisualizer : MonoBehaviour {

    LogText LT;

    Text LogWindow;
	// Use this for initialization
	void Start () {
        LT = GameObject.FindGameObjectWithTag("GameController").GetComponent<LogText>();
        LogWindow = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        LogWindow.text = LT.LogString;
	}
}
