using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingVisualizer : MonoBehaviour
{
    [SerializeField]
    Button[] Slots;
    [SerializeField]
    Text[] SlotText;

    CharacterManager CM;
    CraftingPanel PP;

    // Use this for initialization
    void Start()
    {
        PP = GetComponentInParent<CraftingPanel>();
        CM = StaticReferences.CharacterManager;
        Slots = GetComponentsInChildren<Button>();
        SlotText = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i == PP.SelectedSlot)
            {
                Slots[i].image.color = Color.cyan;
            }
            else
            {
                Slots[i].image.color = Color.white;
            }

        }

        for (int i = 0; i < SlotText.Length; i++)
        {
            if (i < CM.CharacterLibrary.Length)
            {
                SlotText[i].text = CM.CharacterLibrary[i].Nome(1);
                Slots[i].gameObject.SetActive(true);
            }

            if (i >= CM.CharacterLibrary.Length)
            {
                Slots[i].gameObject.SetActive(false);
            }

        }
    }
}
