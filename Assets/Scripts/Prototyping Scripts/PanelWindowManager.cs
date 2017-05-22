using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelWindowManager : MonoBehaviour
{

    // THIS CODE IS A GENERAL PORPUSE CODE,AND COULD BE USED FOR THE FINAL GAME UI
    [SerializeField]
    GameObject[] Panels;
    [SerializeField]
    public int SelectedPanel;

    [SerializeField]
    GameObject BackToMain;

    public static PanelWindowManager PWM;


    private void Awake()
    {
        if (PWM == null)
        {
            PWM = this;
        }
        else if (PWM != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        JumpTo(0);
    } 

    public void JumpTo(int N)
    {
        if (N != 0)
        {
            BackToMain.SetActive(true);
        }else
        {
            BackToMain.SetActive(false);
        }
        SelectedPanel = N;
        Panels[N].SetActive(true);
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i != N)
            {
                Panels[i].SetActive(false);
            }
        }
    }

	public void SwitchScreen(bool Direction)
    {
		if (Direction) {
			SelectedPanel++;

			if (SelectedPanel < Panels.Length) {
				Panels [SelectedPanel].SetActive (true);
				for (int i = 0; i < Panels.Length; i++) {
					if (i != SelectedPanel) {
						Panels [i].SetActive (false);
					}
				}
			} else if (SelectedPanel >= Panels.Length) {
				SelectedPanel = 0;
				Panels [SelectedPanel].SetActive (true);
				for (int i = 0; i < Panels.Length; i++) {
					if (i != SelectedPanel) {
						Panels [i].SetActive (false);
					}
				}
			}
		} else {
			SelectedPanel--;

			if (SelectedPanel >= 0)
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
			else if (SelectedPanel < 0)
			{
				SelectedPanel = Panels.Length - 1; Panels[SelectedPanel].SetActive(true);
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
}
    
