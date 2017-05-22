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
        if(PanelWindowManager.PWM.SelectedPanel == 2)
        {
            CreditToShow = 1;
        } else if (PanelWindowManager.PWM.SelectedPanel == 4)
        {
            CreditToShow = 2;
        } else
        {
            CreditToShow = 0;
        }

        if (CreditToShow == 0)
        {
           Visualizer.text = "Credits: " + MoneyManager.MM.Credits.Value.ToString();
        }
        else if (CreditToShow == 1)
        {
           Visualizer.text = "Dust: " + MoneyManager.MM.Dust.Value.ToString();
        }
        else if (CreditToShow == 2)
        {
            Visualizer.text = "Power: " + MoneyManager.MM.Power.Value.ToString();
        }

    }
}
