using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleCharacterBar : MonoBehaviour
{

    [SerializeField]
    Image CharacterAvatar;
    [SerializeField]
    Slider HPBar;
    [SerializeField]
    Slider SPBar;
    [SerializeField]
    Slider TurnBar;
    [SerializeField]
    Character CharacterToShow;
    [SerializeField]
    Text CharacterName;

    public void ActivateCharacterBar(Character CHA)
    {
        gameObject.SetActive(true);
        CharacterToShow = CHA;
        TurnBar.maxValue = 10000;
        SPBar.maxValue = CharacterToShow.MaxSP;
        HPBar.maxValue = CharacterToShow.MaxHP;
        CharacterAvatar.sprite = CharacterToShow.Image;
        CharacterName.text = CharacterToShow.Nome(1);

    }

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            TurnBar.value = CharacterToShow.TurnCharge;
            SPBar.value = CharacterToShow.SP;
            HPBar.value = CharacterToShow.HP;
            if (CharacterToShow.HP < 0)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
