using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    [SerializeField]
    public Character[] CharacterLibrary;
    [SerializeField]
    public Character[] Party = new Character[4];
    [SerializeField]
    public Character[] PlayerCharacterInventory = new Character[32];

    LogText LT;

    bool BattleActive = true;

    // Use this for initialization
    void Start () {
        LT = gameObject.GetComponent<LogText>();
        ResetCharacters(CharacterLibrary);

        for(int i = 0;i < PlayerCharacterInventory.Length; i++)
        {
            PlayerCharacterInventory[i] = new Character();
        }
	}
	
	// Update is called once per frame
	void Update () {
        TurnCharger();
    }

    void ResetCharacters(Character[] Characters)
    {
        for(int i = 0;i < Characters.Length; i++)
        {

            Characters[i].ResetStatsToBase();
        }
    }

     public void AddToPartyFromLibrary(int CharacterID,int TargetSlot)
    {
        Party[TargetSlot] = CharacterLibrary[CharacterID];
    }

    void TurnCharger()
    {// CODE TO CHARGE CHARACTER TURN
        for (int i = 0; i < CharacterLibrary.Length; i++)
        {// THIS ONE IS CHARGING CHARACTERS IN THE CHARACTER LIBRARY,ONLY FOR TESTING!!!
            if (CharacterLibrary[i].TurnCharge < 1000)
            {
                CharacterLibrary[i].TurnCharge += CharacterLibrary[i].Speed;
                CharacterLibrary[i].Ready = false;
            }
            if (CharacterLibrary[i].TurnCharge >= 1000)
            {
                CharacterLibrary[i].TurnCharge = 1000;
                CharacterLibrary[i].Ready = true;
            }
        }
        for (int i = 0; i < Party.Length; i++)
        {// THIS ONE *SHOULD BE* CLOSER TO THE FINAL VERSION,ONLY CHARGES CHARACTERS IN THE PARTY
            if (Party[i].TurnCharge < 1000)
            {
                Party[i].TurnCharge += Party[i].Speed;
                Party[i].Ready = false;
            }
            if (Party[i].TurnCharge >= 1000)
            {
                Party[i].TurnCharge = 1000;
                Party[i].Ready = true;
            }
        }
    }
}
[System.Serializable]
public class Character
{
    [Header("Base Info")]
    [SerializeField]
    public string Nome;
    [SerializeField]
    public string Descricao;
    [SerializeField]
    public string Sexo;
    [SerializeField]
    public string Raca;
    public int RaceID;
    [SerializeField]
    public string Classe;
    // Determines if this character is active/ usable or not. Characters with this bool as true count as usable and will be seen in the inventory screen
    public bool Ativo;

    public Sprite Image;

    [Header("Stats")]
    [SerializeField]
    public Stats Stats;

    [Header("Vitals")]
    public bool Vivo = true;
    public int HP;
    public int MaxHP;
    public int SP;
    public int MaxSP;
    public int BaseSpeed;
    public int Speed;
    public int TurnCharge;
    public bool Ready;

    [Header("Attacks")]
    [SerializeField]
    public Attack[] Attacks = new Attack[4];

    public void TakeDamage(int Damage)
    {
        HP -= Damage;
        if(HP <= 0)
        {
            Vivo = false;
            HP = 0;
        }
    }

    public void Healing(int Heal)
    {
        HP += Heal;
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }
    }

    public void Revive(int Heal)
    {
        HP += Heal;
        Vivo = true;
    }

    public Character()
    {
        Ativo = false;
    }

    public void ResetStatsToBase()
    {
        HP = MaxHP;
        SP = MaxSP;
        TurnCharge = 0;
        Speed = BaseSpeed;
        Stats.DynForca = Stats.BaseForca;
        Stats.DynCarisma = Stats.BaseCarisma;
        Stats.DynDextreza = Stats.BaseDextreza;
        Stats.DynInteligencia = Stats.BaseInteligencia;
        Stats.DynSabedoria = Stats.BaseSabedoria;
        Stats.DynSorte = Stats.BaseSorte;
        Stats.DynConstituicao = Stats.BaseConstituicao;
    }

}
[System.Serializable]
public class Stats
{
    [Header("Base Stats")]
    public int BaseForca;
    public int BaseConstituicao;
    public int BaseDextreza;
    public int BaseCarisma;
    public int BaseInteligencia;
    public int BaseSabedoria;
    public int BaseSorte;

    [Header("Dynamic Stats")]
    public int DynForca;
    public int DynConstituicao;
    public int DynDextreza;
    public int DynCarisma;
    public int DynInteligencia;
    public int DynSabedoria;
    public int DynSorte;
}

[System.Serializable]
public class Attack
{
    [Header("Basic Info")]
    public string Nome;
    public string Descricao;
    public string AttackType;
    public int AttackMode;
    public string StatPrimario;
    public string StatSegundario;
    public int BaseVariable;

    [Header("Targeting Info")]
    public bool FriendlyTarget;
    public int NumberOfTargets;

    [Header("Cost")]
    public int SPCost;

    private int Stat1;
    private int Stat2;

    void SetAttackVariables(Character attacker)
    {
        if (StatPrimario == "Força")
        {

        }

    }



    public void doAttack(Character attacker, Character[] target)
    {
        if (attacker.Ready)
        {
            if (attacker.SP >= SPCost)
            {
                attacker.TurnCharge = 0;
                attacker.SP -= SPCost;
                if (AttackMode == 0)
                {
                    for (int i = 0; i < target.Length; i++) { target[i].TakeDamage(BaseVariable); LogText.LT.addToLogText(attacker.Nome + " attacked " + target[i].Nome + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n"); }

                }
                if (AttackMode == 1)
                {
                    for (int i = 0; i < target.Length; i++)
                    {
                        LogText.LT.addToLogText(attacker.Nome + " attacked " + target[i].Nome + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n");
                        target[i].Healing(BaseVariable);
                    }
                }
            }
            else
            {
                LogText.LT.addToLogText(attacker.Nome + "does not have enought SP \n");
            }
            }
        else if (!attacker.Ready)
        {
            LogText.LT.addToLogText(attacker.Nome + "is not ready yet \n");
        }
    }

    public void doAttack(Character attacker,Character target)
    {
        if (attacker.Ready)
        {
            if(attacker.SP >= SPCost)
            {
                if (AttackMode == 0)
                {
                    target.TakeDamage(BaseVariable);
                    LogText.LT.addToLogText(attacker.Nome + " attacked " + target.Nome + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n");
                    attacker.TurnCharge = 0;
                    attacker.SP -= SPCost;
                }
                if (AttackMode == 1)
                {

                    target.Healing(BaseVariable);
                    LogText.LT.addToLogText(attacker.Nome + " healed " + target.Nome + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n");
                    attacker.TurnCharge = 0;
                    attacker.SP -= SPCost;
                }
            } else
            {
                LogText.LT.addToLogText(attacker.Nome + "does not have enought SP \n");
            }

        }
       else if (!attacker.Ready)
        {
            LogText.LT.addToLogText(attacker.Nome + "is not ready yet \n");
        }


    }

}