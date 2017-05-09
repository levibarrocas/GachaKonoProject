using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSlotButton : MonoBehaviour {
    [SerializeField]
    Text NomeAtaque;
    [SerializeField]
    Text TipoAtaque;
    [SerializeField]
    Text Custo;

    public int Slot;

    Attack AT;

    void SetData()
    {
        AT = StaticReferences.BM.ActiveCharacter.Attacks[Slot];

        NomeAtaque.text = AT.Nome;
        TipoAtaque.text = AT.AttackType;
        Custo.text = "SP COST: " + AT.SPCost;
    }

    private void OnEnable()
    {
        SetData();
    }

    void TaskOnClick()
    {

    }
}
