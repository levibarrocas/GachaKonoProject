﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRandomizer : MonoBehaviour {



    [SerializeField]
    List<Character> Commons = new List<Character>();
    [SerializeField]
    List<Character> Rares = new List<Character>();
    [SerializeField]
    List<Character> Epics = new List<Character>();
    [SerializeField]
    List<Character> Legendaries = new List<Character>();

    CharacterManager CM;

    public ColorBlock[] ColorBlocks;

    // This script should generate a random character with a 4 tiered rarity,using 4 different arrays each for a different rarity

    // It will generate a random number and according to it draw a character from the appropriate array

    // The 4 rarity levels are 
    // Common 50% of chance. 1-50 on the random generation
    // Rare with a 30% of chance. 51-80 on the random generation
    // Epic with a 15% of chance. 81-95 on the random generation
    // Legendary with a 5% of chance. 96-100 on the random generation 
    public static CharacterRandomizer CR;

    private void Awake()
    {
        if (CR == null)
        {
            CR = this;
        }
        else if (CM != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        CM = CharacterManager.CM;
        FillOutRarities();
    }




    private void Update()
    {
    }

    void FillOutRarities()
    {
        Character[] CharacterLibrary = CM.CharacterLibrary;

        for (int i = 0; i < CharacterLibrary.Length; i++)
        {
            if (!CharacterLibrary[i].AIControlled)
            {
                if (CharacterLibrary[i].Rarity == 0)
                {
                    Commons.Add(CharacterLibrary[i]);
                    //CharacterLibrary[i].Colors = ColorBlocks[0];
                }
                if (CharacterLibrary[i].Rarity == 1)
                {
                    Rares.Add(CharacterLibrary[i]);
                    //CharacterLibrary[i].Colors = ColorBlocks[1];
                }
                if (CharacterLibrary[i].Rarity == 2)
                {
                    Epics.Add(CharacterLibrary[i]);
                    //CharacterLibrary[i].Colors = ColorBlocks[2];
                }
                if (CharacterLibrary[i].Rarity == 3)
                {
                    Legendaries.Add(CharacterLibrary[i]);
                    //CharacterLibrary[i].Colors = ColorBlocks[3];
                }
            }
        }
    }

    public Character BasicRandomCharacter()
    {
        float r = Random.Range(1, 100);

        if (r < 50)
        {
           int n =Random.Range(0, Commons.Count);
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Commons[n]);
            return CHA;
        }
        else if (r <80)
        {
            int n = Random.Range(0, Rares.Count);
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Rares[n]);
            return CHA;
        }
        else if (r < 95)
        {
            int n = Random.Range(0, Epics.Count);
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Epics[n]);
            return CHA;
        }
        else if (r <= 100)
        {
            int n = Random.Range(0, Legendaries.Count);
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Legendaries[n]);
            return CHA;
        } else
        {
            return Commons[0];
        }
    }

    public Character BasicRandomCollectionCharacter(int Collection)
    {
        float r = Random.Range(1, 100);

        if (r < 50)
        {
            int n = Random.Range(0, Commons.Count);
            while (Commons[n].Collection != Collection)
            {
                n = Random.Range(0, Commons.Count);
            }
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Commons[n]);
            return CHA;
        }
        else if (r < 80)
        {
            int n = Random.Range(0, Rares.Count);
            while (Rares[n].Collection != Collection)
            {
                n = Random.Range(0, Rares.Count);
            }
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Rares[n]);
            return CHA;
        }
        else if (r < 95)
        {
            int n = Random.Range(0, Epics.Count);
            while (Epics[n].Collection != Collection)
            {
                n = Random.Range(0, Epics.Count);
            }
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Epics[n]);
            return CHA;

        }
        else if (r <= 100)
        {
            int n = Random.Range(0, Legendaries.Count);
            while (Legendaries[n].Collection != Collection)
            {
                n = Random.Range(0, Legendaries.Count);
            }
            Character CHA = new Character();
            CHA.CloneAnotherCharacter(Legendaries[n]);
            return CHA;
        }
        else
        {
            return Commons[0];
        }
    }

    public Character SpecialCharacterGeneration(float Bonus)
    {
        float r = Random.Range(1, 100);

        r = (r + Bonus);

        if (r < 50)
        {
            int n = Random.Range(0, Commons.Count);
            return Commons[n];
        }
        else if (r < 80)
        {
            int n = Random.Range(0, Rares.Count);
            return Rares[n];
        }
        else if (r < 95)
        {
            int n = Random.Range(0, Epics.Count);
            return Epics[n];
        }
        else if (r <= 100)
        {
            int n = Random.Range(0, Legendaries.Count);
            return Legendaries[n];
        }
        else
        {
            return Commons[0];
        }
    }

}
