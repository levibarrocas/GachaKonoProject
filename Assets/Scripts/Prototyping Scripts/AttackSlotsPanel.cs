using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSlotsPanel : MonoBehaviour {

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
    void Update()
    {
        
    }
}
