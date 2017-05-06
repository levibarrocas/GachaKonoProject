using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticReferences : MonoBehaviour {
    public static CharacterManager CharacterManager;
    public static CharacterRandomizer CharacterRandomizer;
    public static LogText LogText;
    public static MoneyManager MoneyManager;
    public static CharacterVisualizer CV;
    public static AttackVisualizer AV;
    public static BattleScreenManager BCM;
    public static BattleManager BM;
	void Awake () {
        SetReferences();
	}

    void SetReferences()
    {
        CharacterRandomizer = GetComponent<CharacterRandomizer>();
        LogText = GetComponent<LogText>();
        MoneyManager = GetComponent<MoneyManager>();
        CharacterManager = GetComponent<CharacterManager>();
        CV = GameObject.Find("Character Visualizer Panel").GetComponent<CharacterVisualizer>();
        GameObject.Find("Character Visualizer ClickBlocking Panel").SetActive(false);
        AV = GameObject.Find("Attack Info Visualizer").GetComponent<AttackVisualizer>();
        GameObject.Find("Attack Info ClickBlocking").SetActive(false);
        BCM = GameObject.Find("Battle Screen").GetComponent<BattleScreenManager>();
        GameObject.Find("Battle Screen").SetActive(false);
        if (GameObject.Find("Starting Canvas").activeSelf) { GameObject.Find("Main Canvas").SetActive(false); }
        BM = GetComponent<BattleManager>();
        
        
    }
}
