using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;


public class CharacterManager : MonoBehaviour {
    // THIS CODE IS A GENERAL MANAGER FOR EVERYTHING THAT INVOLVES CHARACTERS IT SHOULD STORE THE PARTY,INVENTORY AND LIBRARY WITH ALL CHARACTERS
    // A library with all characters in the game
    public Character[] CharacterLibrary;
    // A old version of the party sytem
    public Character[] Party = new Character[4];
    // The player inventory

    public List<Character> CharacterInventory = new List<Character>();

    public Party PlayerParty = new Party();

    public Party R1;
   [SerializeField]
    bool testesave;
    [SerializeField]
    bool testeload;

    public static CharacterManager CM;

    private void Awake()
    {
        if(CM == null)
        {
            DontDestroyOnLoad(gameObject);
            CM = this;
        } else if (CM != this)
        {
            Destroy(gameObject);
        }
    }

    void Start () {
        
        ResetCharacters(CharacterLibrary);

        for(int i = 0;i < CharacterLibrary.Length; i++)
        {
            CharacterLibrary[i].OriginalSlot = i;
        }
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
                SavingManager.SM.Save();
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
                SavingManager.SM.Save();
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

    public void SaveCharacters()
    {
        if (File.Exists(Application.persistentDataPath + "/characters.dat"))
        {
            File.Delete(Application.persistentDataPath + "/characters.dat");
        }
        SerializableCharacters Characters = new SerializableCharacters();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/characters.dat", FileMode.Create);
        for (int i = 0; i < CharacterInventory.Count; i++)
        {
            SerializableCharacter Test = new SerializableCharacter();
            Test.SerializeCharacter(CharacterInventory[i]);
            Characters.CharacterInventory.Add(Test);
        }
        for (int i = 0; i < PlayerParty.PartyCharacters.Count; i++)
        {
            SerializableCharacter Test = new SerializableCharacter();
            Test.SerializeCharacter(PlayerParty.PartyCharacters[i]);
            Characters.Party.Add(Test);
        }
        bf.Serialize(file, Characters);
        file.Close();
    }
    public void LoadCharacters()
    {
        CharacterInventory.Clear();
        PlayerParty.PartyCharacters.Clear();
        int slot = 0;
        if (File.Exists(Application.persistentDataPath + "/characters.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/characters.dat", FileMode.Open);
            SerializableCharacters CHAS = (SerializableCharacters)bf.Deserialize(file);
            for(int i = 0;i < CHAS.Party.Count; i++)
            {
                PlayerParty.AddToParty(CHAS.Party[i].UnSerializeCharacter());
            }
            for (int i = 0; i < CHAS.CharacterInventory.Count; i++)
            {
                CharacterInventory.Add(CHAS.CharacterInventory[i].UnSerializeCharacter());
            }
        }
        //    while (File.Exists(Application.persistentDataPath + "/character" + slot + ".dat"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream file = File.Open(Application.persistentDataPath + "/character" + slot + ".dat",FileMode.Open);
        //    SerializableCharacter CHA = (SerializableCharacter)bf.Deserialize(file);
        //    CharacterInventory.Add(CHA.UnSerializeCharacter());
        //    file.Close();
        //    slot++;
        //}
        //while (File.Exists(Application.persistentDataPath + "/partycharacter" + slot + ".dat"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream file = File.Open(Application.persistentDataPath + "/partycharacter" + slot + ".dat", FileMode.Open);
        //    SerializableCharacter CHA = (SerializableCharacter)bf.Deserialize(file);
        //    PlayerParty.AddToParty(CHA.UnSerializeCharacter());
        //    file.Close();
        //    slot++;
        //}
    }

    void Update () {
    //    TurnCharger();
        if(Input.GetKeyDown("space"))
        {
            R1.RandomParty();

            StaticReferences.BM.SetupBattle(R1);
        }
        if (testeload)
        {
            LoadCharacters();
            testeload = false;
        }
        if (testesave)
        {
            SaveCharacters();
            testesave = false;
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
    //public ColorBlock Colors;

    
    [Header("Rarity Flavours")]
    public string CommonTitle;
    public string RareTitle;
    public string LegendaryTitle;
    public int TitleRarityID; 
    public Sprite ImageCommon;
    public Sprite ImageRare;
    public Sprite ImageLegendary;
    public Sprite WallpaperCommon;
    public Sprite WallpaperRare;
    public Sprite WallpaperLegendary;
    public Sprite Image;
    public Sprite Wallpaper;

    public int Collection;

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

    public int OriginalSlot;

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
            TitleRarityID = 0;
            Titulo = CommonTitle;
            Image = ImageCommon;
        }else if (r1 < 90)
        {
            TitleRarityID = 1;
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
            TitleRarityID = 2;
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
            TitleRarityID = 0;
            Titulo = CommonTitle;
            Image = ImageCommon;
            Wallpaper = WallpaperCommon;
        }
            else if (r1 < RarityLevel2)
            {
            TitleRarityID = 1;
            Image = ImageRare;
            Wallpaper = WallpaperRare;
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
            TitleRarityID = 2;
            Image = ImageLegendary;
            Wallpaper = WallpaperLegendary;
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

        if (AIControlled)
        {
            PlayerProfile.PP.TotalDamageTaken += Damage;
        } else { PlayerProfile.PP.TotalDamageDone += Damage; }
    }

    public void Healing(int Heal)
    {
        HP += Heal;
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }
        if (!AIControlled)
        {
            PlayerProfile.PP.TotalDamageHealed += Heal;
        }
   }

    public void Revive(int Heal)
    {
        HP += Heal;

        if (!AIControlled)
        {
            PlayerProfile.PP.TotalDamageHealed += Heal;
        }

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

        //Colors = CHA.Colors;

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
        WallpaperLegendary = CHA.WallpaperLegendary;
        WallpaperCommon = CHA.WallpaperCommon;
        WallpaperRare = CHA.WallpaperRare;
        MaxHP = CHA.MaxHP;
        MaxSP = CHA.MaxSP;
        BaseSpeed = CHA.BaseSpeed;
        Speed = CHA.Speed;

        Image = CHA.Image;
        Descricao = CHA.Descricao;
        ResetStatsToBase();
        OriginalSlot = CHA.OriginalSlot;

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
public class SerializableCharacter
{

    public string BaseNome;
    public string Titulo;
    public string Descricao;
    public string Sexo;
    public string Raca;
    public int RaceID;
    public string Classe;
    public int Level = 1;

    public int NextLevelCost = 5;
    public int OriginalSlot;
    public int TitleRarityID;
    public Stats Stats;

    public bool Vivo = true;
    public int HP;
    public int MaxHP;
    public int SP;
    public int MaxSP;
    public int BaseSpeed;
    public int Speed;
    public int TurnCharge;
    public bool Ready;

    public Attack[] Attacks = new Attack[4];

    public int Rarity;
    public int ExtraRarity;

    public int Collection;

    public void SerializeCharacter(Character CHA)
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
        Level = CHA.Level;

        OriginalSlot = CHA.OriginalSlot;

        //Colors = CHA.Colors;
        Collection = CHA.Collection;

        TitleRarityID = CHA.TitleRarityID;
        Attacks[0] = CHA.Attacks[0];
        Attacks[1] = CHA.Attacks[1];
        Attacks[2] = CHA.Attacks[2];
        Attacks[3] = CHA.Attacks[3];

        Rarity = CHA.Rarity;
        MaxHP = CHA.MaxHP;
        MaxSP = CHA.MaxSP;
        BaseSpeed = CHA.BaseSpeed;
        Speed = CHA.Speed;

        Descricao = CHA.Descricao;


    }

    public Character UnSerializeCharacter()
    {
        Character UNCHA = new Character();
        UNCHA.BaseNome = BaseNome;
        UNCHA.Stats = new Stats();
        UNCHA.Stats.BaseForca = Stats.BaseForca;
        UNCHA.Stats.BaseCarisma = Stats.BaseCarisma;
        UNCHA.Stats.BaseDextreza = Stats.BaseDextreza;
        UNCHA.Stats.BaseInteligencia = Stats.BaseInteligencia;
        UNCHA.Stats.BaseSabedoria =Stats.BaseSabedoria;
        UNCHA.Stats.BaseSorte =Stats.BaseSorte;
        UNCHA.Stats.BaseConstituicao =Stats.BaseConstituicao;
        UNCHA.Raca =Raca;
        UNCHA.Classe =Classe;
        UNCHA.Level = Level;
        UNCHA.OriginalSlot = OriginalSlot;
        //Colors =Colors;

        UNCHA.Collection = Collection;

        UNCHA.Attacks[0] =Attacks[0];
        UNCHA.Attacks[1] =Attacks[1];
        UNCHA.Attacks[2] =Attacks[2];
        UNCHA.Attacks[3] =Attacks[3];

        UNCHA.Rarity =Rarity;
        UNCHA.TitleRarityID = TitleRarityID;
        UNCHA.CommonTitle = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].CommonTitle;
        UNCHA.RareTitle = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].RareTitle;
        UNCHA.LegendaryTitle = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].LegendaryTitle;
        UNCHA.ImageLegendary = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].ImageLegendary;
        UNCHA.ImageCommon = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].ImageCommon;
        UNCHA.ImageRare = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].ImageRare;
        UNCHA.WallpaperLegendary = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].WallpaperLegendary;
        UNCHA.WallpaperCommon = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].WallpaperCommon;
        UNCHA.WallpaperRare = CharacterManager.CM.CharacterLibrary[UNCHA.OriginalSlot].WallpaperRare;
        UNCHA.MaxHP = MaxHP;
        UNCHA.MaxSP = MaxSP;
        UNCHA.BaseSpeed = BaseSpeed;
        UNCHA.Speed = Speed;
        if(TitleRarityID == 0)
        {
            UNCHA.Image = UNCHA.ImageCommon;
            UNCHA.Titulo = UNCHA.CommonTitle;
        }
        if (TitleRarityID == 1)
        {
            UNCHA.Image = UNCHA.ImageRare;
            UNCHA.Titulo = UNCHA.RareTitle;
        }
        if (TitleRarityID == 2)
        {
            UNCHA.Image = UNCHA.ImageLegendary;
            UNCHA.Titulo = UNCHA.LegendaryTitle;
        }

        UNCHA.Descricao = Descricao;
        UNCHA.ResetStatsToBase();

        return UNCHA;
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
[System.Serializable]
public class SerializableCharacters
{
    public List<SerializableCharacter> CharacterInventory = new List<SerializableCharacter>();
    public List<SerializableCharacter> Party = new List<SerializableCharacter>();

}