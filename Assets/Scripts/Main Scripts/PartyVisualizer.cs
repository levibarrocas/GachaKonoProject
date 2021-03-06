﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyVisualizer : MonoBehaviour {
    [SerializeField]
    public PartySlotButton[] Buttons;

    CharacterManager CM;
    [SerializeField]
    int SelectedSlot;
    [SerializeField]
    GameObject VisualizeButton;
    [SerializeField]
    GameObject RemoveParty;
    [SerializeField]
    GameObject EnpowerParty;
    [SerializeField]
    GameObject TestBattle;



    private void Start()
    {
        Buttons = GetComponentsInChildren<PartySlotButton>();
        CM = CharacterManager.CM;
    }

    private void Update()
    {

            if(CM.PlayerParty.Ammount() > 0)
            {
            VisualizeButton.SetActive(true);
            RemoveParty.SetActive(true);
            EnpowerParty.SetActive(true);
            TestBattle.SetActive(true);
            } else
            {
            VisualizeButton.SetActive(false);
            RemoveParty.SetActive(false);
            EnpowerParty.SetActive(false);
            TestBattle.SetActive(false);
            }

        for(int i = 0;i < Buttons.Length; i++)
        {
            if(i < CM.PlayerParty.Ammount())
            {
                Buttons[i].SetButtonInfo(CM.PlayerParty.Slot(i));
            } else
            {
                Buttons[i].DisableButton();

            }

        }
    }

    public void VisualizeSlot()
    {
        StaticReferences.CV.ShowCharacter(CM.PlayerParty.Slot(SelectedSlot));
    }

    public void SelectSlot(int Slot)
    {
        SelectedSlot = Slot;

      
    }

    public void LevelUpSlot()
    {
        if (MoneyManager.MM.SpendPower(CM.PlayerParty.Slot(SelectedSlot).NextLevelCost))
        {
            CM.PlayerParty.Slot(SelectedSlot).Levelup();
        } else
        {
            StaticReferences.LogText.LogWarning("You don't have enough points to Enpower " + CM.PlayerParty.Slot(SelectedSlot).Nome(1));
        }
        
    }

    public void RemoveFromParty()
    {
        StaticReferences.LogText.LogWarning(CM.PlayerParty.Slot(SelectedSlot).Nome(SelectedSlot) + " was sent back to the inventory!");
        CM.PlayerParty.RemoveFromParty(SelectedSlot);
        
    }
}
