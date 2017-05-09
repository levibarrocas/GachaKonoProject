using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterManager : MonoBehaviour {
    // THIS CODE IS A GENERAL MANAGER FOR EVERYTHING THAT INVOLVES CHARACTERS IT SHOULD STORE THE PARTY,INVENTORY AND LIBRARY WITH ALL CHARACTERS
    // A library with all characters in the game
    public Character[] CharacterLibrary;
    // A old version of the party sytem
    public Character[] Party = new Character[4];
    // The player inventory
    public List<Character> CharacterInventory = new List<Character>();
    // The new party system
    public Party PlayerParty = new Party();

    public Party R1;


    // Use this for initialization
    void Start () {
        
        ResetCharacters(CharacterLibrary);
	}
	
    public bool AddCharacterToInventory(Character CHA)
    {
        if (CharacterInventory.Count < 70)
        {

            bool RepeatedTest = false;
            for (int i = 0; i < CharacterInventory.Count; i++)
            {
                if (CharacterInventory[i] == CHA)
                {
                    RepeatedTest = true;
                }
            }
            if (!RepeatedTest)
            {
                CharacterInventory.Add(CHA);
                LogText.LT.addToLogText("Added " + CHA.Nome() + " to the character inventory");
                return true;

            }
            else
            {
                if (CHA.Rarity == 0)
                {
                    MoneyManager.MM.GainCredits(5);
                }
                if (CHA.Rarity == 1)
                {
                    MoneyManager.MM.GainCredits(20);
                }
                if (CHA.Rarity == 2)
                {
                    MoneyManager.MM.GainCredits(100);
                }
                if (CHA.Rarity == 3)
                {
                    MoneyManager.MM.GainCredits(400);
                }

                LogText.LT.LogWarning("Repeated character " + CHA.Nome() + " added rewarded credits according to rarity");
                return true;
            }
        } else
        {
            LogText.LT.LogWarning("Inventory is full!");
            return false;
        }
    }

    public Character GenerateCharacter(int LibrarySlot,int ExtraPoints)
    {
        Character CHA = new Character();
        CHA.CloneAnotherCharacter(CharacterLibrary[LibrarySlot]);

        CHA.GenerateRarity(ExtraPoints);
        return CHA;
    }

    // Update is called once per frame
    void Update () {
    //    TurnCharger();
        if(Input.GetKeyDown("space"))
        {
            R1.RandomParty();

            StaticReferences.BM.SetupBattle(R1);
        }

    }

    public void TestBattle()
    {
        R1.RandomParty();

        StaticReferences.BM.SetupBattle(R1);
    }

    void ResetCharacters(Character[] Characters)
    {
        for(int i = 0;i < Characters.Length; i++)
        {

            Characters[i].ResetStatsToBase();
        }
    }

     public void AddToPartyFromLibrary(int CharacterID)
    {
        PlayerParty.AddToParty(CharacterLibrary[CharacterID]);
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
public class Party
{
    [SerializeField]
    public List<Character> PartyCharacters = new List<Character>();

    public bool AIControlled;

    public Character Slot(int i)
    {
        return PartyCharacters[i];
    }

    public int Ammount()
    {
        return PartyCharacters.Count;
    }

    public void RandomParty()
    {
        PartyCharacters.Clear();
        Character C1 = StaticReferences.CharacterRandomizer.BasicRandomCharacter();
        Character C2 = StaticReferences.CharacterRandomizer.BasicRandomCharacter();
        Character C3 = StaticReferences.CharacterRandomizer.BasicRandomCharacter();
        Character C4 = StaticReferences.CharacterRandomizer.BasicRandomCharacter();
        PartyCharacters.Add(C1);
        PartyCharacters.Add(C2);
        PartyCharacters.Add(C3);
        PartyCharacters.Add(C4);
        
    }

    

    public bool AddToParty(Character CHA)
    {
        if (Ammount() < 4)
        {
            PartyCharacters.Add(CHA);
            return true;
        } else
        {
            StaticReferences.LogText.LogWarning("O personagem " + CHA.Nome() + " nao cabe na party porque ela está cheia");
            return false;
        }
    }

    public void RemoveFromParty(int Slot)
    {
        StaticReferences.CharacterManager.CharacterInventory.Add(PartyCharacters[Slot]);
        PartyCharacters.RemoveAt(Slot);

    }


}

[System.Serializable]
public class Character
{
    [Header("Base Info")]
    [SerializeField]
    public string BaseNome;
    public string Titulo;
    [SerializeField]
	[TextArea]
    public string Descricao;
    [SerializeField]
    public string Sexo;
    [SerializeField]
    public string Raca;
    public int RaceID;
    [SerializeField]
    public string Classe;
    public int Level = 1;

    public int NextLevelCost = 5;
    public ColorBlock Colors;


    [Header("Rarity Flavours")]
    public string CommonTitle;
    public string RareTitle;
    public string LegendaryTitle;
    public Sprite ImageCommon;
    public Sprite ImageRare;
    public Sprite ImageLegendary;

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

    [SerializeField]
    [Header("Attacks")]
    public Attack[] Attacks = new Attack[4];

    [Header("Game Info")]
    public bool AIControlled;
    public int Rarity;
    public int ExtraRarity;
    // Determines if this character is active/ usable or not. Characters with this bool as true count as usable and will be seen in the inventory screen
    public bool Ativo;


    public void GenerateRarity()
    {
        int r1 = Random.Range(0, 100);
        int r2 = Random.Range(0, 100);

        if(r1 < 70)
        {
            Titulo = CommonTitle;
            Image = ImageCommon;
        }else if (r1 < 90)
        {
            Image = ImageRare;
            ExtraRarity++;
            Titulo = RareTitle;
            Stats.BaseCarisma += Random.Range(1,5);
            Stats.BaseConstituicao += Random.Range(1,5);
            Stats.BaseDextreza += Random.Range(1, 5);
            Stats.BaseInteligencia += Random.Range(1, 5);
            Stats.BaseSabedoria += Random.Range(1, 5);
            Stats.BaseSorte += Random.Range(1, 5);
            Stats.BaseForca += Random.Range(1, 5);
        }
        else if (r1 < 101)
        {
            Image = ImageLegendary;
            ExtraRarity += 2;
            Titulo = LegendaryTitle;
            Stats.BaseCarisma += Random.Range(3, 10);
            Stats.BaseConstituicao += Random.Range(3, 10);
            Stats.BaseDextreza += Random.Range(3, 10);
            Stats.BaseInteligencia += Random.Range(3, 10);
            Stats.BaseSabedoria += Random.Range(3, 10);
            Stats.BaseSorte += Random.Range(3, 10);
            Stats.BaseForca += Random.Range(3, 10);
        }

        if (r2 < 60)
        {

        } else if (r2 < 90)
        {
            ExtraRarity++;
            Levelup();
        }
        else if (r2 <101)
        {
            ExtraRarity += 2;
            Levelup();
            Levelup();
        }
        ExtraRarity += Rarity;
        StaticReferences.LogText.addToLogText("Generated a " + Nome() + "com raridade extra de " + ExtraRarity.ToString() );

    }

    public void GenerateRarity(int ExtraPoints)
    {
        int RarityLevel1 = 70;
        int RarityLevel2 = 90;

        if(ExtraPoints == 1)
        {
            RarityLevel1 = 50;
            RarityLevel2 = 85;
        }
        if (ExtraPoints == 2)
        {
            RarityLevel1 = 40;
            RarityLevel2 = 75;
        }
        if (ExtraPoints == 3)
        {
            RarityLevel1 = 30;
            RarityLevel2 = 65;
        }
        if (ExtraPoints == 4)
        {
            RarityLevel1 = 20;
            RarityLevel2 = 55;
        }
        if (ExtraPoints == 5)
        {
            RarityLevel1 = 10;
            RarityLevel2 = 45;
        }
        if (ExtraPoints == 6)
        {
            RarityLevel1 = 0;
            RarityLevel2 = 35;
        }
        if (ExtraPoints == 7)
        {
            RarityLevel1 = 0;
            RarityLevel2 = 25;
        }
        if (ExtraPoints == 8)
        {
            RarityLevel1 = 0;
            RarityLevel2 = 15;
        }
        if (ExtraPoints == 9)
        {
            RarityLevel1 = 0;
            RarityLevel2 = 5;
        }
        if (ExtraPoints == 10)
        {
            RarityLevel1 = 0;
            RarityLevel2 = 0;
        }
        int r1 = Random.Range(0, 100);
            int r2 = Random.Range(0, 100);

            if (r1 < RarityLevel1)
            {
                Titulo = CommonTitle;
            Image = ImageCommon;
        }
            else if (r1 < RarityLevel2)
            {
            Image = ImageRare;
                ExtraRarity++;
                Titulo = RareTitle;
                Stats.BaseCarisma += Random.Range(1, 5);
                Stats.BaseConstituicao += Random.Range(1, 5);
                Stats.BaseDextreza += Random.Range(1, 5);
                Stats.BaseInteligencia += Random.Range(1, 5);
                Stats.BaseSabedoria += Random.Range(1, 5);
                Stats.BaseSorte += Random.Range(1, 5);
                Stats.BaseForca += Random.Range(1, 5);
            }
            else if (r1 < 101)
            {
            Image = ImageLegendary;
                ExtraRarity += 2;
                Titulo = LegendaryTitle;
                Stats.BaseCarisma += Random.Range(3, 10);
                Stats.BaseConstituicao += Random.Range(3, 10);
                Stats.BaseDextreza += Random.Range(3, 10);
                Stats.BaseInteligencia += Random.Range(3, 10);
                Stats.BaseSabedoria += Random.Range(3, 10);
                Stats.BaseSorte += Random.Range(3, 10);
                Stats.BaseForca += Random.Range(3, 10);
            }

            if (r2 < RarityLevel1)
            {

            }
            else if (r2 < RarityLevel2)
            {
                ExtraRarity++;
                Levelup();
            }
            else if (r2 < 101)
            {
                ExtraRarity += 2;
                Levelup();
                Levelup();
            }
            ExtraRarity += Rarity;
            StaticReferences.LogText.addToLogText("Generated a " + Nome() + "com raridade extra de " + ExtraRarity.ToString());

        
    }

    public string Nome()
    {
        return (Titulo + " "+  BaseNome + " LvL:" + Level.ToString());
    }

    public string Nome(int Mode)
    {
        if(Mode == 0) { return (Titulo + " " + BaseNome + " LvL:" + Level.ToString()); }
        if(Mode == 1) { return (BaseNome); }
        if (Mode == 2) { return (BaseNome + " LvL:" + Level.ToString()); }
        else return (Titulo + " " + BaseNome);
    }

    public void Levelup()
    {
        NextLevelCost *= 2;
        Level++;
        Stats.BaseCarisma += Random.Range(Level, Level + Level);
        Stats.BaseConstituicao += Random.Range(Level, Level + Level);
        Stats.BaseDextreza += Random.Range(Level, Level + Level);
        Stats.BaseInteligencia += Random.Range(Level, Level + Level);
        Stats.BaseSabedoria += Random.Range(Level, Level + Level);
        Stats.BaseSorte += Random.Range(Level, Level + Level);
        Stats.BaseForca += Random.Range(Level, Level + Level);
        MaxHP += Random.Range(Stats.BaseConstituicao, Stats.BaseConstituicao * 2);
        MaxSP += Random.Range(Stats.BaseSabedoria, Stats.BaseSabedoria * 2);
        BaseSpeed += Random.Range(Stats.BaseDextreza / 8, Stats.BaseDextreza / 16);

        ResetStatsToBase();

    }

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
    
    public void CloneAnotherCharacter(Character CHA)
    {
        BaseNome = CHA.BaseNome;
        Stats = new Stats();
        Stats.BaseForca = CHA.Stats.BaseForca;
        Stats.BaseCarisma = CHA.Stats.BaseCarisma;
        Stats.BaseDextreza = CHA.Stats.BaseDextreza;
        Stats.BaseInteligencia = CHA.Stats.BaseInteligencia;
        Stats.BaseSabedoria = CHA.Stats.BaseSabedoria;
        Stats.BaseSorte = CHA.Stats.BaseSorte;
        Stats.BaseConstituicao = CHA.Stats.BaseConstituicao;
        Raca = CHA.Raca;
        Classe = CHA.Classe;
        Level = 1;

        Colors = CHA.Colors;

        Attacks[0] = CHA.Attacks[0];
        Attacks[1] = CHA.Attacks[1];
        Attacks[2] = CHA.Attacks[2];
        Attacks[3] = CHA.Attacks[3];

        Rarity = CHA.Rarity;
        CommonTitle = CHA.CommonTitle;
        RareTitle = CHA.RareTitle;
        LegendaryTitle = CHA.LegendaryTitle;
        ImageLegendary = CHA.ImageLegendary;
        ImageCommon = CHA.ImageCommon;
        ImageRare = CHA.ImageRare;
        MaxHP = CHA.MaxHP;
        MaxSP = CHA.MaxSP;
        BaseSpeed = CHA.BaseSpeed;
        Speed = CHA.Speed;

        Image = CHA.Image;
        Descricao = CHA.Descricao;
        ResetStatsToBase();


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
	[Range(0,15)]
    public int BaseForca;
	[Range(0,15)]
    public int BaseConstituicao;
	[Range(0,15)]
	public int BaseDextreza;
	[Range(0,15)]
	public int BaseCarisma;
	[Range(0,15)]
	public int BaseInteligencia;
	[Range(0,15)]
	public int BaseSabedoria;
	[Range(0,15)]
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
                    for (int i = 0; i < target.Length; i++) { target[i].TakeDamage(BaseVariable); LogText.LT.addToLogText(attacker.Nome() + " attacked " + target[i].Nome() + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n"); }

                }
                if (AttackMode == 1)
                {
                    for (int i = 0; i < target.Length; i++)
                    {
                        LogText.LT.addToLogText(attacker.Nome() + " attacked " + target[i].Nome() + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n");
                        target[i].Healing(BaseVariable);
                    }
                }
            }
            else
            {
                LogText.LT.addToLogText(attacker.Nome() + "does not have enought SP \n");
            }
            }
        else if (!attacker.Ready)
        {
            LogText.LT.addToLogText(attacker.Nome() + "is not ready yet \n");
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
                    LogText.LT.addToLogText(attacker.Nome() + " attacked " + target.Nome() + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n");
                    attacker.TurnCharge = 0;
                    attacker.SP -= SPCost;
                }
                if (AttackMode == 1)
                {

                    target.Healing(BaseVariable);
                    LogText.LT.addToLogText(attacker.Nome() + " healed " + target.Nome() + "for " + BaseVariable + " Damage,using " + SPCost + "SP \n");
                    attacker.TurnCharge = 0;
                    attacker.SP -= SPCost;
                }
            } else
            {
                LogText.LT.addToLogText(attacker.Nome() + "does not have enought SP \n");
            }

        }
       else if (!attacker.Ready)
        {
            LogText.LT.addToLogText(attacker.Nome() + "is not ready yet \n");
        }


    }

}