using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DummyTesterPanel : MonoBehaviour {
    [SerializeField]
    Character Dummy;

    CharacterManager CM;
    CharacterVisualizerUIManager CUIM;

    [SerializeField]
    Slider HPBar;
    [SerializeField]
    Slider SPBar;


	// Use this for initialization
	void Start () {
        CUIM = GetComponentInParent<CharacterVisualizerUIManager>();
        CM = GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterManager>();


        SPBar.maxValue = Dummy.MaxSP;
        HPBar.maxValue = Dummy.MaxHP;
	}
	
	// Update is called once per frame
	void Update () {
        HPBar.value = Dummy.HP;
        SPBar.value = Dummy.SP;
	}

    public void AttackDummy(int AttackSlot)
    {
        CM.CharacterLibrary[CUIM.CharacterSlotSelected].Attacks[AttackSlot].doAttack(CM.CharacterLibrary[CUIM.CharacterSlotSelected], Dummy);
    }
}
