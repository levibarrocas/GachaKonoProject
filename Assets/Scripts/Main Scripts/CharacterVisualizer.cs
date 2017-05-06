using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVisualizer : MonoBehaviour {
    // This script should be used to create a popup panel that visualizes a character's info on the screen
    [SerializeField]
    Text Carisma;
    [SerializeField]
    Text Sorte;
    [SerializeField]
    Text Sabedoria;
    [SerializeField]
    Text Forca;
    [SerializeField]
    Text Inteligencia;
    [SerializeField]
    Text Constituicao;
    [SerializeField]
    Text Dextreza;
    [SerializeField]
    Text Nome;
    [SerializeField]
    Text Level;
    [SerializeField]
    Image Avatar;
    [SerializeField]
    Image SexSymbol;

    [SerializeField]
    Text HP;
    [SerializeField]
    Text SP;
    [SerializeField]
    Text Speed;

    [SerializeField]
    GameObject ClickBlockerPanel;

    Character CharacterStorage;

    public void ShowCharacter(Character CHA)
    {
        ClickBlockerPanel.SetActive(true);
        CharacterStorage = CHA;
        //Carisma.text = "Carisma:" + CHA.Stats.DynCarisma.ToString();
        //Sorte.text = "Sorte:" + CHA.Stats.DynSorte.ToString();
        //Sabedoria.text = "Sabedoria:" + CHA.Stats.DynSabedoria.ToString();
        //Forca.text = "Força:" + CHA.Stats.DynForca.ToString();
        //Inteligencia.text = "Inteligencia:" + CHA.Stats.DynInteligencia.ToString();
        //Constituicao.text = "Constituição:" + CHA.Stats.DynConstituicao.ToString();
        //Dextreza.text = "Dextreza:" + CHA.Stats.DynDextreza.ToString();

        Carisma.text = CHA.Stats.DynCarisma.ToString();
        Sorte.text = CHA.Stats.DynSorte.ToString();
        Sabedoria.text = CHA.Stats.DynSabedoria.ToString();
        Forca.text = CHA.Stats.DynForca.ToString();
        Inteligencia.text = CHA.Stats.DynInteligencia.ToString();
        Constituicao.text = CHA.Stats.DynConstituicao.ToString();
        Dextreza.text = CHA.Stats.DynDextreza.ToString();
        Level.text = "Power Level: " + CHA.Level.ToString();

        HP.text = CHA.HP.ToString();
        SP.text = CHA.SP.ToString();
        Speed.text = CHA.Speed.ToString();

        Nome.text = CHA.Nome(3);
        Avatar.sprite = CHA.Image;

    }

    public void DisplayAttackInfo(int Slot)
    {
        StaticReferences.AV.VisualizeAttack(CharacterStorage.Attacks[Slot]);
    }


}
