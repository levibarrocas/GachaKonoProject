using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VisualizerInventory : MonoBehaviour
{
    [SerializeField]
    Button[] Slots;
    [SerializeField]
    Text[] SlotText;
    InventoryPanel IP;

    CharacterManager CM;

    // Use this for initialization
    void Start()
    {
        IP = GetComponentInParent<InventoryPanel>();
        CM = GameObject.Find("GameManager").GetComponent<CharacterManager>();

        Slots = GetComponentsInChildren<Button>();
        SlotText = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < Slots.Length; i++)
        {
            if(i == IP.SelectedSlot)
            {
                Slots[i].image.color = Color.cyan;
            } else
            {
                Slots[i].image.color = Color.white;
            }

        }

        for (int i = 0; i < SlotText.Length; i++)
        {
            if (i < CM.CharacterInventory.Count)
            {
                SlotText[i].text = CM.CharacterInventory[i].Nome();
                Slots[i].gameObject.SetActive(true);
            }

            if (i >= CM.CharacterInventory.Count)
            {
                Slots[i].gameObject.SetActive(false);
            }

        }
    }
}
