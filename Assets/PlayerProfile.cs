using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour {

    public string Name;
    public string PlayerNumber;

    public long TotalDamageDone;
    public long TotalDamageTaken;
    public long TotalDamageHealed;

    public float PlayerLuck;
    public float PlayerLuckRatio;

    public static PlayerProfile PP;

    private void Awake()
    {
        if (PP == null)
        {
            DontDestroyOnLoad(gameObject);
            PP = this;
        }
        else if (PP != this)
        {
            Destroy(gameObject);
        }
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
