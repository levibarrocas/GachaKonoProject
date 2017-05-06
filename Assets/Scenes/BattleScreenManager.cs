using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenManager : MonoBehaviour {
    [SerializeField]
    BattlePositionsManager[] BPM;
    [SerializeField]
    BattleCharacterBar[] BCB;

    Party PlayerParty;
    Party EnemyParty;




    public void StartBattle (Party PlayerP,Party EnemyP)
    {
        PlayerParty = PlayerP;
        EnemyParty = EnemyP;
        gameObject.SetActive(true);
        for (int i = 0;i< BCB.Length; i++)
        {
            if (i < PlayerP.Ammount())
            {
                BCB[i].ActivateCharacterBar(PlayerP.PartyCharacters[i]);
            } else
            {
                BCB[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < BPM.Length; i++)
        {
            if (!BPM[i].Enemy)
            {
                if (PlayerP.Ammount() == BPM[i].Ammount())
                {
                    BPM[i].SetupBattleAvatars(PlayerP);
                }
            }
            else
            {
                if (EnemyP.Ammount() == BPM[i].Ammount())
                {
                    BPM[i].SetupBattleAvatars(EnemyP);
                }
            }

            }
        }
    }
