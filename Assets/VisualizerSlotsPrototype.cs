using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VisualizerSlotsPrototype : MonoBehaviour {
    [SerializeField]
    Button[] Slots;
    [SerializeField]
    Text[] SlotText;

    CharacterManager CM;

    // Use this for initialization
    void Start () {
        CM = GameObject.Find("GameManager").GetComponent<CharacterManager>();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0;i < SlotText.Length; i++)
        {
           if (i < CM.Characters.Length)
            {
                SlotText[i].text = CM.Characters[i].Nome;
            }

            if(i >= CM.Characters.Length)
            {
                Slots[i].gameObject.SetActive(false);
            }
        }
	}
}
