using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public class MoneyManager : MonoBehaviour {

    [SerializeField]
    public Currency Credits;
    [SerializeField]
    public Currency Dust;
    [SerializeField]
    public Currency Power;

    public static MoneyManager MM;


    private void Awake()
    {
        if (MM == null)
        {
            DontDestroyOnLoad(gameObject);
            MM = this;
        }
        else if (MM != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveMoney()
    {
        if(File.Exists(Application.persistentDataPath + "/currency.dat"))
        {
            File.Delete(Application.persistentDataPath + "/currency.dat");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/currency.dat", FileMode.Create);
        SerializableMoney Test = new SerializableMoney();
        Test.Credits = Credits;
        Test.Dust = Dust;
        Test.Power = Power;
        bf.Serialize(file, Test);
        file.Close();
    }

    public void LoadMoney()
    {
        if (File.Exists(Application.persistentDataPath + "/currency.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/currency.dat", FileMode.Open);
            SerializableMoney MON = (SerializableMoney)bf.Deserialize(file);
            Dust = MON.Dust;
            Power = MON.Power;
            Credits = MON.Credits;
            file.Close();
        }
    }

    private void Start()
    {
        MM = this;

    }

    public bool SpendPower(int AmmountSpent)
    {
        return(Power.Spend(AmmountSpent));
    }

    public bool SpendDust(int AmmountSpent)
    {
        return (Dust.Spend(AmmountSpent));

    }

    public void GainDust(int AmmountGained)
    {
        Power.Gain(AmmountGained);

    }
    public bool SpendCredits(int AmmountSpent)
    {
        return (Credits.Spend(AmmountSpent));

    }

    public void GainCredits(int AmmountGained)
    {
        Power.Gain(AmmountGained);

    }
    public void GainPower(int AmmountGained)
    {
        Power.Gain(AmmountGained);

    }

}
[System.Serializable]
public class Currency
{
    public string Name;
    public string PluralName;
    public long Value;
    float ExchangeRate;



    public bool Spend(int AmmountSpent)
    {
        if (Value >= AmmountSpent)
        {
            Value -= AmmountSpent;
            SavingManager.SM.Save();
            StaticReferences.LogText.addToLogText(AmmountSpent.ToString() + " was spent from the player's" + PluralName);
            return true;
        }
        else
        {
            StaticReferences.LogText.LogWarning("The player didnt have enough currency for a transaction!");

            return false;
        }
    }

    public void Gain(int AmmountGained)
    {
        Value += AmmountGained;
        SavingManager.SM.Save();
        LogText.LT.addToLogText(AmmountGained.ToString() + " was added to the player's" + PluralName);

    }

    public string StrValue()
    {
        return Value.ToString();
    }
}
[System.Serializable]
public class SerializableMoney{
    public Currency Credits;
    public Currency Dust;
    public Currency Power;
}