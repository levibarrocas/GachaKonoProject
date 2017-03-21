using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelWindowManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] Panels;
    int SelectedPanel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchScreen()
    {
        SelectedPanel++;
        if (SelectedPanel < Panels.Length)
        {
            Panels[SelectedPanel].SetActive(true);
            for (int i = 0; i < Panels.Length; i++)
            {
                if (i != SelectedPanel)
                {
                    Panels[i].SetActive(false);
                }
            }
        }
        else if (SelectedPanel >= Panels.Length)
        {
            SelectedPanel = 0; Panels[SelectedPanel].SetActive(true);
            for (int i = 0; i < Panels.Length; i++)
            {
                if (i != SelectedPanel)
                {
                    Panels[i].SetActive(false);
                }
            }
        }
    }
}
    
