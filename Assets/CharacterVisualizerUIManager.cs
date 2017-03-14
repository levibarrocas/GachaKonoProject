using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVisualizerUIManager : MonoBehaviour {
    CharacterManager CM;

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


	// Use this for initialization
	void Start () {
        CM = GameObject.Find("GameManager").GetComponent<CharacterManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SlotButtonPressed(int SlotNumber)
    {
        NomeDoPersonagem.text = CM.Characters[SlotNumber].Nome;
        DescricaoDoPersonagem.text = CM.Characters[SlotNumber].Descricao;
        RacaDoPersonagem.text = CM.Characters[SlotNumber].Raca;

        Carisma.text = "Car:" + CM.Characters[SlotNumber].Stats.Carisma.ToString();
        Forca.text = "For:" + CM.Characters[SlotNumber].Stats.Forca.ToString();
        Inteligencia.text = "Int:" + CM.Characters[SlotNumber].Stats.Inteligencia.ToString();
        Sabedoria.text = "Sab:" + CM.Characters[SlotNumber].Stats.Sabedoria.ToString();
        Dex.text = "Dex:" + CM.Characters[SlotNumber].Stats.Dextreza.ToString();
        Const.text = "Con:" + CM.Characters[SlotNumber].Stats.Constituicao.ToString();
        Sorte.text = "Sorte:" + CM.Characters[SlotNumber].Stats.Sorte.ToString();

        ImagemDoPersonagem.sprite = CM.Characters[SlotNumber].Image;

        if(CM.Characters[SlotNumber].Sexo == "Masculino")
        {
            SexSym.sprite = MaleSym;
        } else if(CM.Characters[SlotNumber].Sexo == "Feminino")
        {
            SexSym.sprite = FemaleSym;
        }
    }
}
