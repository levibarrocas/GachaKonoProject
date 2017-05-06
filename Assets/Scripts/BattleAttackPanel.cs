using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAttackPanel : MonoBehaviour {
    [SerializeField]
    AttackSlotButton[] ASB;
    [SerializeField]
    GameObject AttackScreen;
    [SerializeField]
    GameObject TargetScreen;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < ASB.Length; i++)
        {
            ASB[i].Slot = i;
        }
	}
	
    public void SwitchToTargetScreen(int AttackSlot)
    {
        AttackScreen.SetActive(false);
   
    }

	// Update is called once per frame
	void Update () {
		
	}
}
