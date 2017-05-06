using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelWindowManager : MonoBehaviour
{

    // THIS CODE IS A GENERAL PORPUSE CODE,AND COULD BE USED FOR THE FINAL GAME UI
    [SerializeField]
    GameObject[] Panels;
    [SerializeField]
    int SelectedPanel;

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
    
