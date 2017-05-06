using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackVisualizer : MonoBehaviour {
    [SerializeField]
    Text Nome;
    [SerializeField]
    Text Stat1;
    [SerializeField]
    Text Stat2;
    [SerializeField]
    Text Descricao;

    [SerializeField]
    GameObject ClickBlockerPanel;

    public void VisualizeAttack(Attack ATT)
    {
        ClickBlockerPanel.SetActive(true);
        Nome.text = ATT.Nome;
        Stat1.text = "Stat1:" + ATT.StatPrimario;
        Stat2.text = "Stat2:" + ATT.StatSegundario;
        Descricao.text = ATT.Descricao;
    }

}
