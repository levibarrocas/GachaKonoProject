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
    GameObject TargetPanel;
    [SerializeField]
    Attack ActiveAttack;

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
                        ActiveCharacter.Ready = true;
                        AttackPanel.SetActive(true);
                        Title.text = ActiveCharacter.Nome(1) + " is ready to attack!";
                        Avatar.sprite = ActiveCharacter.Image;
                        
                        TurnActive = true;
                        
                    }

                }
            } 
        }
    }

    public void AttackPanelButton(int Slot)
    {
        AttackPanel.SetActive(false);
        TargetPanel.SetActive(true);

        ActiveAttack = ActiveCharacter.Attacks[Slot];
    }

    public void TargetEnemyPanelButton(int Slot)
    {
        TargetPanel.SetActive(false);
        ActiveAttack.doAttack(ActiveCharacter, EnemyParty.PartyCharacters[Slot]);
        ActiveCharacter.TurnCharge = 0;
        ActiveCharacter.Ready = false;
        TurnActive = false;
    }
    public void TargetAllyPanelButton(int Slot)
    {
        TargetPanel.SetActive(false);
        ActiveAttack.doAttack(ActiveCharacter, PlayerParty.PartyCharacters[Slot]);
        ActiveCharacter.TurnCharge = 0;
        ActiveCharacter.Ready = false;
        TurnActive = false;
    }
}
