using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGiro : MonoBehaviour {
    [SerializeField]
    float AnimationSpeed;
    bool StartAnimation;
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            StartAnimation = true;
        }
        if (StartAnimation)
        {
            if (transform.eulerAngles.y < 180)
            {
                Vector3 euler2 = transform.eulerAngles;
                euler2.y += Time.deltaTime * AnimationSpeed;
                transform.eulerAngles = euler2;
            }
        }
    }
}
