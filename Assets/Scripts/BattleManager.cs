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
    GameObject TargetPanelGO;
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
            if(WinLoseConditions() == 0)
            {
                PanelWindowManager.PWM.JumpTo(0);
                LogText.LT.addToLogText("Player has lost a battle");
                
                BattleActive = false;
            }
            if (WinLoseConditions() == 1)
            {
                int Reward = 0;
                PanelWindowManager.PWM.JumpTo(0);
                LogText.LT.addToLogText("Player has won a battle");
                for (int i = 0;i < EnemyParty.PartyCharacters.Count; i++)
                {
                    Reward += EnemyParty.PartyCharacters[i].Level * 5;
                }
                MoneyManager.MM.Power.Gain(Reward);
                BattleActive = false;
            }
        }
    }

    public void AttackPanelButton(int Slot)
    {
        AttackPanel.SetActive(false);
       
        TargetPanelGO.SetActive(true);

        ActiveAttack = ActiveCharacter.Attacks[Slot];
        TargetPanel.TP.SetUPTargetSystem(ActiveAttack.FriendlyTarget);
    }

    public void TargetEnemyPanelButton(int Slot)
    {
        TargetPanelGO.SetActive(false);
        ActiveAttack.doAttack(ActiveCharacter, EnemyParty.PartyCharacters[Slot]);
        ActiveCharacter.TurnCharge = 0;
        ActiveCharacter.Ready = false;
        TurnActive = false;
    }
    public void TargetAllyPanelButton(int Slot)
    {
        TargetPanelGO.SetActive(false);
        ActiveAttack.doAttack(ActiveCharacter, PlayerParty.PartyCharacters[Slot]);
        ActiveCharacter.TurnCharge = 0;
        ActiveCharacter.Ready = false;
        TurnActive = false;
    }

    public int WinLoseConditions()
    {
        bool PlayerAlive = false;
        bool EnemyAlive = false;
        for(int i = 0;i < PlayerParty.Ammount(); i++)
        {
            if (PlayerParty.Slot(i).HP > 0)
            {
                PlayerAlive = true;
            }
        }
        for (int i = 0; i < EnemyParty.Ammount(); i++)
        {
            if (EnemyParty.Slot(i).HP > 0)
            {
                EnemyAlive = true;
            }
        }
        if(!PlayerAlive)
        {
            return 0;
        } else if (!EnemyAlive)
        {
            return 1;
        } else
        {
            return 2;
        }

    }
}
