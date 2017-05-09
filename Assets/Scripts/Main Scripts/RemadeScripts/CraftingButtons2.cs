using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButtons2 : MonoBehaviour
{

    public CraftingSlotButton[] CSB;
    CharacterManager CM;

    void Start()
    {
        CSB = GetComponentsInChildren<CraftingSlotButton>();
        CM = GameObject.Find("GameManager").GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < CSB.Length; i++)
        {
            if (i < CM.CharacterLibrary.Length)
            {
                CSB[i].SetInfo(CM.CharacterLibrary[i]);
                CSB[i].Slot = i;
                CSB[i].gameObject.SetActive(true);
            }

            if (i >= CM.CharacterLibrary.Length)
            {
                CSB[i].gameObject.SetActive(false);
            }

        }
    }
}
