using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour {

    public Party PlayerParty;
    public Party EnemyParty;

    bool BattleActive;
    public bool TurnActive;
    public Character ActiveCharacter;

    [SerializeField]
    GameObject AttackPanel;

    [SerializeField]
    Text Title;
    [SerializeField]
    Image Avatar;

    public void SetupBattle(Party EP)
    {
        PlayerParty = StaticReferences.CharacterManager.PlayerParty;
        EnemyParty = EP;
        StaticReferences.BCM.StartBattle(PlayerParty, EP);
        BattleActive = true;
    }

    private void Update()
    {
        if (BattleActive)
        {
            if (!TurnActive)
            {
                for (int i = 0; i < PlayerParty.Ammount(); i++)
                {
                    if (PlayerParty.Slot(i).TurnCharge < 10000) { PlayerParty.Slot(i).TurnCharge += PlayerParty.Slot(i).Speed; }
                    else
                    {
                        ActiveCharacter = PlayerParty.Slot(i);
                        AttackPanel.SetActive(true);
                        Title.text = ActiveCharacter.Nome(1) + " is ready to attack!";
                        Avatar.sprite = ActiveCharacter.Image;
                        
                        TurnActive = true;
                        
                    }

                }
            }
        }
    }
}
