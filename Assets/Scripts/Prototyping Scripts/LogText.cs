using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class LogText : MonoBehaviour {
    public static LogText LT;
    public string LogString;

    private int Counter;
    [SerializeField]
    GameObject WarningPanel;
    [SerializeField]
    Text WarningText;

	// Use this for initialization
	void Start () {
        SetInitialReferences();
	}

    void SetInitialReferences()
    {
        LT = GetComponent<LogText>();
    }

    public void addToLogText(string Log)
    {
        LogString += Counter + ".  " + Log + "\n";
        Counter++;
        Debug.Log(Log);

    }

    public void LogWarning(string Log)
    {
        addToLogText(Log);
        WarningText.text = Log;
        WarningPanel.SetActive(true);
    }

    public void ClearLog()
    {
        LogString = "";
        Counter = 0;

    }
}
