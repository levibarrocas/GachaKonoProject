using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour {

    public static SavingManager SM;

    private void Awake()
    {
        if (SM == null)
        {
            DontDestroyOnLoad(gameObject);
            SM = this;
        }
        else if (SM != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSave();
    }

    void OnApplicationFocus(bool pauseStatus)
    {
        if (pauseStatus)
        {
        }
        else
        {
            Save();
        }
    }

    public void Save()
    {
        CharacterManager.CM.SaveCharacters();
        MoneyManager.MM.SaveMoney();
    }

    
    public void LoadSave()
    {
        CharacterManager.CM.LoadCharacters();
        MoneyManager.MM.LoadMoney();
    }

}
