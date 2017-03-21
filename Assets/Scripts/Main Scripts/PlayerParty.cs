using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour {
    CharacterManager CM;
    [SerializeField]
    Character[] Party = new Character[4];
	// Use this for initialization
	void Start () {
        CM = GetComponent<CharacterManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddCharacterToParty(Character CHA,int TargetSlot)
    {
        CHA = Party[TargetSlot];

    }
}
