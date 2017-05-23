using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCharacterVisualizer : MonoBehaviour {
    Character GeneratedCharacter = new Character();
    Character GeneratedCharacterOriginal;
    bool FreshCharacter;

    CharacterManager CM;
    CharacterRandomizer CR;
    GameObject GM;
    [SerializeField]
    Text Nome;
    [SerializeField]
    Text Classe;
    [SerializeField]
    Text Raridade;
    [SerializeField]
    Text Descricao;
    [SerializeField]
    Image Avatar;
    [SerializeField]
    SpriteRenderer Sprite;

    [SerializeField]
    TestGiro TG;


    private void Start()
    {
        SetInitialReferences();
    }

    public void GenerateRandomCharacter(int Collection)
    {
        if(MoneyManager.MM.SpendCredits(50))
        {
            GeneratedCharacter = new Character();
            GeneratedCharacterOriginal = CR.BasicRandomCollectionCharacter(Collection);
            GeneratedCharacter.CloneAnotherCharacter(GeneratedCharacterOriginal);
            GeneratedCharacter.GenerateRarity();
            UpdateUI();
            FreshCharacter = true;
        }
    }

    public void Update()
    {
    }

    public void AddGeneratedCharacterToInventory()
    {
        if (FreshCharacter)
        {
            CM.AddCharacterToInventory(GeneratedCharacter);
            FreshCharacter = false;
        } else
        {
            LogText.LT.LogWarning("This character was already added to the inventory");
        }
    }

    void UpdateUI()
    {
        PanelWindowManager.PWM.JumpTo(6);
        Nome.text = GeneratedCharacter.Nome();
        Classe.text = GeneratedCharacter.Classe;
        Descricao.text = GeneratedCharacter.Descricao;

        Sprite.sprite = GeneratedCharacter.WallpaperFull;
        Avatar.sprite = GeneratedCharacter.Wallpaper;
        Avatar.color = Color.white;

        if (GeneratedCharacter.Rarity == 0)
        {
            TG.RarityID = 0;
            Raridade.color = Color.gray;
            Raridade.text = "Raridade:Comum";
        }

        if (GeneratedCharacter.Rarity == 1)
        {
            TG.RarityID = 1;
            Raridade.color = Color.blue;
            Raridade.text = "Raridade:Rara";
        }

        if (GeneratedCharacter.Rarity == 2)
        {
            TG.RarityID = 2;

            Raridade.color = Color.magenta;
            Raridade.text = "Raridade:Epica";
        }

        if (GeneratedCharacter.Rarity == 3)
        {
            TG.RarityID = 3;
            Raridade.color = Color.yellow;
            Raridade.text = "Raridade:Lendaria!";
        }
    }

    void SetInitialReferences()
    {
        GM = GameObject.FindGameObjectWithTag("GameController");
        CM = CharacterManager.CM;
        CR = GM.GetComponent<CharacterRandomizer>();
    }


}
