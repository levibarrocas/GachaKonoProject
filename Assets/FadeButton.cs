using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeButton : MonoBehaviour {

    Button BTN;
    [SerializeField]
    GameObject CanvasMain;
    [SerializeField]
    GameObject StartingCanvas;

    [SerializeField]
    CanvasGroup CG;
    bool ActivateTransition;

	// Use this for initialization
	void Start () {
        BTN = GetComponent<Button>();
        BTN.onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
        ////if(CR.GetAlpha() < 0.05)
        ////    {
        ////        StartingCanvas.SetActive(false);
        ////    }    

        if (ActivateTransition)
        {
            //Color CO = IMG.color;
            //CO.a -= Time.deltaTime;
            //IMG.color = CO;
            CG.alpha -= Time.deltaTime;
            if(CG.alpha <= 0.05)
            {
                StartingCanvas.SetActive(false);
            }
        }
    }
    void OnClick()
    {
        CanvasMain.SetActive(true);
        ActivateTransition = true;
        //IMG.CrossFadeAlpha(0, 2, true);
    }

}
