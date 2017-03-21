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
        NomeDoPersonagem.text = CM.Party[PartySlotSelected].Nome;
        DescricaoDoPersonagem.text = CM.Party[PartySlotSelected].Descricao;
        RacaDoPersonagem.text = CM.Party[PartySlotSelected].Raca;

        Carisma.text = "Car:" + CM.Party[PartySlotSelected].Stats.BaseCarisma.ToString();
        Forca.text = "For:" + CM.Party[PartySlotSelected].Stats.BaseForca.ToString();
        Inteligencia.text = "Int:" + CM.Party[PartySlotSelected].Stats.BaseInteligencia.ToString();
        Sabedoria.text = "Sab:" + CM.Party[PartySlotSelected].Stats.BaseSabedoria.ToString();
        Dex.text = "Dex:" + CM.Party[PartySlotSelected].Stats.BaseDextreza.ToString();
        Const.text = "Con:" + CM.Party[PartySlotSelected].Stats.BaseConstituicao.ToString();
        Sorte.text = "Sorte:" + CM.Party[PartySlotSelected].Stats.BaseSorte.ToString();

        ImagemDoPersonagem.sprite = CM.Party[PartySlotSelected].Image;

        if (CM.Party[PartySlotSelected].Sexo == "Masculino")
        {
            SexSym.sprite = MaleSym;
        }
        else if (CM.Party[PartySlotSelected].Sexo == "Feminino")
        {
            SexSym.sprite = FemaleSym;
        }
    }

    public void SwitchPartySlot(bool direction)
    {
        if (direction)
        {
            if (PartySlotSelected < 3)
            {
                PartySlotSelected++;
            }
            if (PartySlotSelected == 3)
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
                PartySlotSelected = 3;
            }
        }
    }
}