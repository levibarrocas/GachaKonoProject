using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePositionsManager : MonoBehaviour {

    BattleAvatar[] Positions;
    BattleCharacterBar[] BattleCharacterBar;


    public bool Enemy;

    

	// Use this for initialization
	void Start () {
        Positions = GetComponentsInChildren<BattleAvatar>();
    }

    public int Ammount()
    {
        Positions = GetComponentsInChildren<BattleAvatar>();
        return Positions.Length;
    }
	
    public bool SetupBattleAvatars(Party PA)
    {
        gameObject.SetActive(true);
        if (PA.PartyCharacters.Count != Positions.Length)
        {
            return false;
        } else
        {
            for (int i = 0; i < Positions.Length; i++)
            {
                Positions[i].ActivateBattleAvatar(PA.PartyCharacters[i]);
                
            }
            return true;
        }



    }
}
