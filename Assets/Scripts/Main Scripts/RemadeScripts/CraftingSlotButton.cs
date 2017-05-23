using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlotButton : MonoBehaviour
{

    public int Slot;

    public Text ButtonText;
    public Button ThisButton;
    public CraftingPanel IP;
    public Image AvatarImage;



    private void Start()
    {
        ThisButton = GetComponent<Button>();
        ButtonText = GetComponentInChildren<Text>();
        ThisButton.onClick.AddListener(TaskOnClick);
        IP = GetComponentInParent<CraftingPanel>();
        AvatarImage = GetComponentsInChildren<Image>()[1];
    }

    public void SetInfo(Character CHA)
    {

        ThisButton.colors = CHA.Colors;
        ButtonText.text = CHA.Nome(1);
        AvatarImage.sprite = CHA.Image;

    }

    public void TaskOnClick()
    {
        IP.SelectSlot(Slot);
    }


}
