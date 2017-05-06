using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons2 : MonoBehaviour {

    public InventorySlotButton[] ISB;
    CharacterManager CM;
	
	void Start () {
       ISB = GetComponentsInChildren<InventorySlotButton>();
        CM = GameObject.Find("GameManager").GetComponent<CharacterManager>();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < ISB.Length; i++)
        {
            if (i < CM.CharacterInventory.Count)
            {
                ISB[i].SetInfo(CM.CharacterInventory[i]);
                ISB[i].Slot = i;
                ISB[i].gameObject.SetActive(true);
            }

            if (i >= CM.CharacterInventory.Count)
            {
                ISB[i].gameObject.SetActive(false);
            }

        }
    }
}
