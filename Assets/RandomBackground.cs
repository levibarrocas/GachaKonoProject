using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomBackground : MonoBehaviour {
    [SerializeField]
    Sprite[] Backgrounds;
    [SerializeField]
    Image Img;
    [SerializeField]
    Image Img2;
    bool WhichChange = true;
    [SerializeField]
    float TimeToChange;

    CanvasRenderer ImgC1;
    CanvasRenderer ImgC2;

    float Cooldown;
	// Use this for initialization
	void Start () {
        int R = Random.Range(0, Backgrounds.Length-1);
        Img.sprite = Backgrounds[R];
        ImgC1 = Img.GetComponent<CanvasRenderer>();
        ImgC2 = Img2.GetComponent<CanvasRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Img1 Alpha =" + ImgC1.GetAlpha().ToString() + "Img2 Alpha =" + ImgC2.GetAlpha().ToString());

        //if (Cooldown == 0)
        //{
        //    if (WhichChange)
        //    {
        //        Img2.CrossFadeAlpha(0, TimeToChange, false);
        //        Img.CrossFadeAlpha(1, TimeToChange, false);
        //    }
        //    else
        //    {
        //        Img.CrossFadeAlpha(0, TimeToChange, false);
        //        Img2.CrossFadeAlpha(1, TimeToChange, false);
        //    }
        //}

        //Cooldown += Time.deltaTime;
        if (Cooldown > (TimeToChange / 4) * 3)
        {
            if (ImgC1.GetAlpha() == 1)
            {
                Cooldown = 0;
                int R = Random.Range(0, Backgrounds.Length - 1);
                Img2.sprite = Backgrounds[R];
                Img.CrossFadeAlpha(0, TimeToChange / 4, false);
                Img2.CrossFadeAlpha(1, TimeToChange / 4, false);
                WhichChange = true;
            }
        } else
        {
            Cooldown += Time.deltaTime;
        }
        if (Cooldown > (TimeToChange / 4) * 3)
        {
            if (WhichChange)
            {
                if (ImgC2.GetAlpha() == 1)
                {
                    Cooldown = 0;
                    int R = Random.Range(0, Backgrounds.Length - 1);
                    Img.sprite = Backgrounds[R];
                    Img2.CrossFadeAlpha(0, TimeToChange / 4, false);
                    Img.CrossFadeAlpha(1, TimeToChange / 4, false);
                }
            }
        }

        //if(Cooldown > TimeToChange)
        //{
            
        //    if (WhichChange)
        //    {
        //        Img.sprite = Backgrounds[R];
        //        WhichChange = true;
        //    } else
        //    {
               
        //        WhichChange = true;
        //    }
        //    Cooldown = 0;
        //}
	}
}
