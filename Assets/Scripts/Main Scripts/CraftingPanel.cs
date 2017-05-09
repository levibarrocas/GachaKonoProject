using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingPanel : MonoBehaviour
{

    public int SelectedSlot;
    CharacterManager CM;
    int CraftingCost;
    [SerializeField]
    Text CostText;
    [SerializeField]
    int ExtraPoints;
    [SerializeField]
    int[] CraftingCosts = new int[16];

    private void Start()
    {
        CM = StaticReferences.CharacterManager;
        SelectSlot(0);
        FillOutCosts();
    }

    private void Update()
    {
        if (Input.GetKey("n")) { DebugCrafting(); }
    }

    void FillOutCosts()
    {
        for(int i = 0;i < CraftingCosts.Length;i++)
        {
            if(i == 0)
            {
                CraftingCosts[i] = 40;
            } else if(i == 1)
            {
                CraftingCosts[i] = 100;
            } else if(i > 1)
            {
                CraftingCosts[i] = CraftingCosts[i - 1] * 4;
            }
            
           
        }
    }

    public void SelectSlot(int SlotNumber)
    {

        SelectedSlot = SlotNumber;
        CraftingCost = CraftingCosts[CM.CharacterLibrary[SelectedSlot].Rarity + ExtraPoints];
        CostText.text = "Cost:" + CraftingCost.ToString();
    }


    public void VisualizeCharacter()
    {
        StaticReferences.CV.ShowCharacter(CM.CharacterLibrary[SelectedSlot]);

    }

    public void AddAExtraPoint()
    {
        if (ExtraPoints < 10)
        {
            ExtraPoints++;
            SelectSlot(SelectedSlot);
        } else
        {
            StaticReferences.LogText.LogWarning("ExtraPoints can't go above 10!");
        }
    }
    public void RemoveAExtraPoint()
    {if(ExtraPoints > 0)
        {
            ExtraPoints--;
            SelectSlot(SelectedSlot);
        }
        else
        {
            StaticReferences.LogText.LogWarning("Extra Points can't go below 0!");
        }
        
    }

    public void DebugCrafting()
    {
        Character CHA = new Character();
        int R = Random.Range(0, CM.CharacterLibrary.Length);
        CHA.CloneAnotherCharacter(CM.CharacterLibrary[R]);

        CHA.GenerateRarity(ExtraPoints);
        CM.AddCharacterToInventory(CHA);
    }

    public void CraftACharacter()
    {

        if (MoneyManager.MM.SpendDust(CraftingCost))
        {
            Character CHA = CM.GenerateCharacter(SelectedSlot, ExtraPoints);
            CM.AddCharacterToInventory(CHA);
            StaticReferences.LogText.LogWarning("You spent " + CraftingCost + " to craft " + CHA.Nome() + " with a rarity level of " + CHA.ExtraRarity);
        }


    }

}
