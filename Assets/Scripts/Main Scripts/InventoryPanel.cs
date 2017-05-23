using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPanel : MonoBehaviour {
    [SerializeField]
    GameObject DeleteButton;
    [SerializeField]
    GameObject VisualizeButton;
    [SerializeField]
    GameObject AddToPartyButton;
    [SerializeField]
    GameObject EmptySign;
    [SerializeField]
    GameObject InventoryView;

    public int SelectedSlot;
    int Reward = 5;
    [SerializeField]
    Text DeleteText;
    public int[] CraftingCosts = new int[15];

    private void Start()
    {
        FillOutCosts();
    }

    void FillOutCosts()
    {
        for (int i = 0; i < CraftingCosts.Length; i++)
        {
            if (i == 0)
            {
                CraftingCosts[i] = 5;
            }
            else if (i == 1)
            {
                CraftingCosts[i] = 20;
            }
            else if (i == 2)
            {
                CraftingCosts[i] = 100;
            }
            else if (i > 2)
            {
                CraftingCosts[i] = CraftingCosts[i - 1] * 4;
            }


        }
    }

    private void Update()
    {
        if(StaticReferences.CharacterManager.CharacterInventory.Count > 0)
        {
            DeleteButton.SetActive(true);
            VisualizeButton.SetActive(true);
            AddToPartyButton.SetActive(true);
            EmptySign.SetActive(false);
            InventoryView.SetActive(true);
        } else
        {
            InventoryView.SetActive(false);
            EmptySign.SetActive(true);
            DeleteButton.SetActive(false);
            VisualizeButton.SetActive(false);
            AddToPartyButton.SetActive(false);
        }
    }

    public void SelectSlot(int SlotNumber)
    {
        FillOutCosts();
        SelectedSlot = SlotNumber;
        Reward = CraftingCosts[CharacterManager.CM.CharacterInventory[SelectedSlot].ExtraRarity];
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 0)
        //{
        //    Reward = 5;
        //}
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 1)
        //{
        //    Reward = 20;
        //}
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 2)
        //{
        //    Reward = 100;
        //}
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 3)
        //{
        //    Reward = 400;
        //}
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 4)
        //{
        //    Reward = 1600;
        //}
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 5)
        //{
        //    Reward = 6400;
        //}
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 6)
        //{
        //    Reward = 25600;
        //}
        //if (StaticReferences.CharacterManager.CharacterInventory[SelectedSlot].ExtraRarity == 7)
        //{
        //    Reward = 102400;
        //}
        DeleteText.text = "Dust this character for " + Reward.ToString();
    }

    public void DeleteSlot()
    {
        MoneyManager.MM.GainDust(Reward);
        StaticReferences.CharacterManager.CharacterInventory.RemoveAt(SelectedSlot);
        if (SelectedSlot > CharacterManager.CM.PlayerParty.PartyCharacters.Count)
        {
            SelectSlot(CharacterManager.CM.PlayerParty.PartyCharacters.Count - 1);
        }
        else
        {
            SelectSlot(SelectedSlot);
        }
    }
    public void AddToParty()
    {
        StaticReferences.CharacterManager.PlayerParty.AddToParty(StaticReferences.CharacterManager.CharacterInventory[SelectedSlot]);
        StaticReferences.CharacterManager.CharacterInventory.RemoveAt(SelectedSlot);
        if (SelectedSlot > CharacterManager.CM.PlayerParty.PartyCharacters.Count)
        {
            SelectSlot(CharacterManager.CM.PlayerParty.PartyCharacters.Count - 1);
        } else
        {
            SelectSlot(SelectedSlot);
        }
    }

    public void VisualizeCharacter()
    {
        StaticReferences.CV.ShowCharacter(StaticReferences.CharacterManager.CharacterInventory[SelectedSlot]);

    }

}
