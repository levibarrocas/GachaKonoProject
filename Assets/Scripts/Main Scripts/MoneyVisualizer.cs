using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyVisualizer : MonoBehaviour {

    Text Visualizer;
    [SerializeField]
    int CreditToShow;

    private void Start()
    {
        Visualizer = GetComponent<Text>();
    }

 

	void Update () {
        if (CreditToShow == 0)
        {
           Visualizer.text = "Credits: " + MoneyManager.MM.Credits.ToString();
        }
        else if (CreditToShow == 1)
        {
           Visualizer.text = "Dust: " + MoneyManager.MM.Dust.ToString();
        }
        else if (CreditToShow == 2)
        {
            Visualizer.text = "Power: " + MoneyManager.MM.Power.ToString();
        }

    }
}
