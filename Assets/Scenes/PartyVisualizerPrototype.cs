using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PartyVisualizerPrototype : MonoBehaviour
{

    CharacterManager CM;

    public int PartySlotSelected;

    [SerializeField]
    Text NomeDoPersonagem;
    [SerializeField]
    Text DescricaoDoPersonagem;
    [SerializeField]
    Text RacaDoPersonagem;

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

    [SerializeField]
    Image ImagemDoPersonagem;

    [SerializeField]
    Image SexSym;

    [SerializeField]
    Sprite MaleSym;
    [SerializeField]
    Sprite FemaleSym;


    void Start()
    {
        CM = GameObject.Find("GameManager").GetComponent<CharacterManager>();
    }

    void Update()
    {
        NomeDoPersonagem.text = CM.PlayerParty.PartyCharacters[PartySlotSelected].Nome();
        DescricaoDoPersonagem.text = CM.PlayerParty.PartyCharacters[PartySlotSelected].Descricao;
        RacaDoPersonagem.text = CM.PlayerParty.PartyCharacters[PartySlotSelected].Raca;

        Carisma.text = "Car:" + CM.PlayerParty.PartyCharacters[PartySlotSelected].Stats.BaseCarisma.ToString();
        Forca.text = "For:" + CM.PlayerParty.PartyCharacters[PartySlotSelected].Stats.BaseForca.ToString();
        Inteligencia.text = "Int:" + CM.PlayerParty.PartyCharacters[PartySlotSelected].Stats.BaseInteligencia.ToString();
        Sabedoria.text = "Sab:" + CM.PlayerParty.PartyCharacters[PartySlotSelected].Stats.BaseSabedoria.ToString();
        Dex.text = "Dex:" + CM.PlayerParty.PartyCharacters[PartySlotSelected].Stats.BaseDextreza.ToString();
        Const.text = "Con:" + CM.PlayerParty.PartyCharacters[PartySlotSelected].Stats.BaseConstituicao.ToString();
        Sorte.text = "Sorte:" + CM.PlayerParty.PartyCharacters[PartySlotSelected].Stats.BaseSorte.ToString();

        ImagemDoPersonagem.sprite = CM.PlayerParty.PartyCharacters[PartySlotSelected].Image;

        if (CM.PlayerParty.PartyCharacters[PartySlotSelected].Sexo == "Masculino")
        {
            SexSym.sprite = MaleSym;
        }
        else if (CM.PlayerParty.PartyCharacters[PartySlotSelected].Sexo == "Feminino")
        {
            SexSym.sprite = FemaleSym;
        }
    }

    public void SwitchPartySlot(bool direction)
    {
        if (direction)
        {
			if (PartySlotSelected < CM.PlayerParty.Ammount())
            {
                PartySlotSelected++;
            }
			if (PartySlotSelected == CM.PlayerParty.Ammount())
            {
                PartySlotSelected = 0;
            }
        }
        if (!direction)
        {
            if (PartySlotSelected > 0)
            {
                PartySlotSelected--;
            }
            if (PartySlotSelected == 0)
            {
				PartySlotSelected = CM.PlayerParty.Ammount();
            }
        }
    }
}