using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVisualizerUIManager : MonoBehaviour {
    CharacterManager CM;

    public int CharacterSlotSelected;

    [Header("Base Info Panel 1")]
    [SerializeField]
    Text NomeDoPersonagem;
    [SerializeField]
    Text DescricaoDoPersonagem;
    [SerializeField]
    Text RacaDoPersonagem;

    [Header("Stats Panel 1")]
    [SerializeField]
    Text Carisma;
    [SerializeField]
    Text Forca;
    [SerializeField]
    Text Const;
    [SerializeField]
    Text Dex;
    [SerializeField]
    Text Inteligencia;
    [SerializeField]
    Text Sabedoria;
    [SerializeField]
    Text Sorte;


    [Header("Attack Slot Buttons Panel 2")]
    [SerializeField]
    Text AttackName1;
    [SerializeField]
    Text AttackName2;
    [SerializeField]
    Text AttackName3;
    [SerializeField]
    Text AttackName4;

    [Header("Attack Slot Buttons Panel 3")]
    [SerializeField]
    Text AttackDummyName1;
    [SerializeField]
    Text AttackDummyName2;
    [SerializeField]
    Text AttackDummyName3;
    [SerializeField]
    Text AttackDummyName4;

    [Header("Attack Descriptions and info Panel 2")]
    [SerializeField]
    Text AttackDescription;

    [Header("Images for Panel 1")]
    [SerializeField]
    Image ImagemDoPersonagem;

    [SerializeField]
    Image SexSym;

    [SerializeField]
    Sprite MaleSym;
    [SerializeField]
    Sprite FemaleSym;


	// Use this for initialization
	void Start () {
        CM = GameObject.Find("GameManager").GetComponent<CharacterManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AttackSlotButtonPressed(int SlotNumber)
    {
        AttackDescription.text = CM.CharacterLibrary[CharacterSlotSelected].Attacks[SlotNumber].Descricao;
    }

    public void SlotButtonPressed(int SlotNumber)
    {
        CharacterSlotSelected = SlotNumber;
        NomeDoPersonagem.text = CM.CharacterLibrary[SlotNumber].Nome;
        DescricaoDoPersonagem.text = CM.CharacterLibrary[SlotNumber].Descricao;
        RacaDoPersonagem.text = CM.CharacterLibrary[SlotNumber].Raca;

        Carisma.text = "Car:" + CM.CharacterLibrary[SlotNumber].Stats.BaseCarisma.ToString();
        Forca.text = "For:" + CM.CharacterLibrary[SlotNumber].Stats.BaseForca.ToString();
        Inteligencia.text = "Int:" + CM.CharacterLibrary[SlotNumber].Stats.BaseInteligencia.ToString();
        Sabedoria.text = "Sab:" + CM.CharacterLibrary[SlotNumber].Stats.BaseSabedoria.ToString();
        Dex.text = "Dex:" + CM.CharacterLibrary[SlotNumber].Stats.BaseDextreza.ToString();
        Const.text = "Con:" + CM.CharacterLibrary[SlotNumber].Stats.BaseConstituicao.ToString();
        Sorte.text = "Sorte:" + CM.CharacterLibrary[SlotNumber].Stats.BaseSorte.ToString();

        AttackName1.text = CM.CharacterLibrary[SlotNumber].Attacks[0].Nome;
        AttackName2.text = CM.CharacterLibrary[SlotNumber].Attacks[1].Nome;
        AttackName3.text = CM.CharacterLibrary[SlotNumber].Attacks[2].Nome;
        AttackName4.text = CM.CharacterLibrary[SlotNumber].Attacks[3].Nome;

        AttackDummyName1.text = CM.CharacterLibrary[SlotNumber].Attacks[0].Nome;
        AttackDummyName2.text = CM.CharacterLibrary[SlotNumber].Attacks[1].Nome;
        AttackDummyName3.text = CM.CharacterLibrary[SlotNumber].Attacks[2].Nome;
        AttackDummyName4.text = CM.CharacterLibrary[SlotNumber].Attacks[3].Nome;

        ImagemDoPersonagem.sprite = CM.CharacterLibrary[SlotNumber].Image;

        if(CM.CharacterLibrary[SlotNumber].Sexo == "Masculino")
        {
            SexSym.sprite = MaleSym;
        } else if(CM.CharacterLibrary[SlotNumber].Sexo == "Feminino")
        {
            SexSym.sprite = FemaleSym;
        }
    }
}
