using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

    public int Credits = 1000;

    public int Dust = 0;
    public int Power = 0;

    public static MoneyManager MM;

    private void Start()
    {
        MM = this;
    }
    public bool SpendPower(int AmmountSpent)
    {
        if (Power >= AmmountSpent)
        {
            Power -= AmmountSpent;

            StaticReferences.LogText.addToLogText(AmmountSpent.ToString() + " was spent from the player's Power");
            return true;
        }
        else
        {
            StaticReferences.LogText.LogWarning("The player didnt have enough Power to use!");
            return false;
        }
    }

    public bool SpendDust(int AmmountSpent)
    {
        if (Dust >= AmmountSpent)
        {
            Dust -= AmmountSpent;

            StaticReferences.LogText.addToLogText(AmmountSpent.ToString() + " was spent from the player's dust");
            return true;
        }
        else
        {
            StaticReferences.LogText.LogWarning("The player didnt have enough Dust for a craft!");
            return false;
        }

    }

    public void GainDust(int AmmountGained)
    {
        Dust += AmmountGained;
        LogText.LT.addToLogText(AmmountGained.ToString() + " was added to the player's dust");

    }
    public bool SpendCredits(int AmmountSpent)
    {
        if(Credits >= AmmountSpent)
        {
            Credits -= AmmountSpent;

            StaticReferences.LogText.addToLogText(AmmountSpent.ToString() + " was spent from the player's credits");
            return true;
        } else
        {
            StaticReferences.LogText.LogWarning("The player didnt have enough money for a transaction!");
            return false;
        }

    }

    public void GainCredits(int AmmountGained)
    {
        Credits += AmmountGained;
        LogText.LT.addToLogText(AmmountGained.ToString() + " was added to the player's credits");

    }
    public void GainPower(int AmmountGained)
    {
        Power += AmmountGained;
        LogText.LT.addToLogText(AmmountGained.ToString() + " was added to the player's Power");

    }

}
