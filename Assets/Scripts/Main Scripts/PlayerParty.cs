using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour {
// THIS CODE IS DEPRECATED IT'S FUNCTION WILL BE DONE IN THE CHARACTER MANAGER

    [SerializeField]
    public Character[] Party = new Character[4];
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddCharacterToParty(Character CHA,int TargetSlot)
    {
        CHA = Party[TargetSlot];

    }
}
