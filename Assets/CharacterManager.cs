using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    [SerializeField]
    public Character[] Characters;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
[System.Serializable]
public class Character : System.Object
{
    [Header("Base Info")]
    [SerializeField]
    public string Nome;
    [SerializeField]
    public string Descricao;
    [SerializeField]
    public string Sexo;
    [SerializeField]
    public string Raca;
    public int RaceID;
    [SerializeField]
    public string Classe;

    public Sprite Image;

    [Header("Stats")]
    [SerializeField]
    public Stats Stats;

    [Header("Vitals")]
    public int HP;
    public int MaxHP;
    public int SP;
    public int MaxSP;
    public int Speed;
    public int TurnCharge;




}
[System.Serializable]
public class Stats : System.Object
{
    public int Forca;
    public int Constituicao;
    public int Dextreza;
    public int Carisma;
    public int Inteligencia;
    public int Sabedoria;
    public int Sorte;
}

[System.Serializable]
public class Attack : System.Object
{
    [Header("Basic Info")]
    public string Nome;
    public string Descrição;
    public string AttackType;
    public int AttackMode;
    public string StatPrimario;
    public string StatSegundario;
    public string BaseVariable;

}