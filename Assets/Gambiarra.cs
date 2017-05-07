using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambiarra : MonoBehaviour {

    Vector3 TRA;

	// Use this for initialization
	void Start () {
        TRA = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        
      
        gameObject.GetComponent<Transform>().position = TRA;
    }
}
