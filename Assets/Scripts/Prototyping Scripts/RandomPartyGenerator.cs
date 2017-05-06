using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPartyGenerator : MonoBehaviour {

    // THIS SCRIPT IS PURELY FOR DEBUGGING PORPUSES AND SHOULD NOT BE IN THE FINAL VERSION

    // THIS SCRIPT FILLS THE PARTY UP EACH SLOT BY USING THE CHARACTER RANDOMIZER;

    PlayerParty PP;
    CharacterRandomizer CR;
    LogText LT;
    [SerializeField]
    int NumberTimes;
    [SerializeField]
    int DebugNCommons;
    [SerializeField]
    int DebugNRares;
    [SerializeField]
    int DebugNEpics;
    [SerializeField]
    int DebugNLegendaries;



    void Start()
    {
        PP = GetComponent<PlayerParty>();
        CR = GetComponent<CharacterRandomizer>();



    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {
            
            for (int i = 0; i < PP.Party.Length; i++)
            {
                NumberTimes++;
                PP.Party[i] = CR.BasicRandomCharacter();
                //LogText.LT.addToLogText("Adicionei " + PP.Party[i].Nome + " para o slot " + i + "da party");
                if(PP.Party[i].Rarity == 0)
                {
                    DebugNCommons++;
                }
                if (PP.Party[i].Rarity == 1)
                {
                    DebugNRares++;
                }
                if (PP.Party[i].Rarity == 2)
                {
                    DebugNEpics++;
                }
                if (PP.Party[i].Rarity == 3)
                {
                    DebugNLegendaries++;
                }
            }
        }

    }
}
