using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySlotButton : MonoBehaviour {
    [SerializeField]
    public Image CharacterImage;
    [SerializeField]
    public Text ButtonText;

    void Start()
    {
        
    }

    public void SetButtonInfo(Character CHA)
    {
        gameObject.SetActive(true);
        CharacterImage.sprite = CHA.Wallpaper;
        ButtonText.text = CHA.Nome(3);
    }

    public void DisableButton()
    {
        gameObject.SetActive(false);
    }

}

