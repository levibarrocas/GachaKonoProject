using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGiro : MonoBehaviour {
    [SerializeField]
    float AnimationSpeed;
    [SerializeField]
    float BackAnimationSpeed;
    [SerializeField]
    bool StartAnimation;
    [SerializeField]
    bool BackAnimation;
    [SerializeField]
    float ZAnimationSpeed;
    [SerializeField]
    ParticleSystem[] PS;
    public int RarityID;

    Vector3 OriginalPosition;
    Vector3 OriginalRotation;

    private void Start()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.eulerAngles;
    }

    public void ResetPosition()
    {
        transform.position = OriginalPosition;
        transform.eulerAngles = OriginalRotation;
        BackAnimation = false;
        StartAnimation = false;
        for (int i = 0; i < PS.Length; i++)
        {
            PS[i].Clear();
            PS[i].gameObject.SetActive(false);
        }
        PS[RarityID].gameObject.SetActive(true);
    }

    public void ActivateCard()
    {
        ResetPosition();
        Vector3 euler2 = transform.eulerAngles;
        euler2.y = 0;
        transform.eulerAngles = euler2;
        BackAnimation = false;
        StartAnimation = true;
        PS[RarityID].Play();
    }

    public void TurnBackCard()
    {
        ResetPosition();
    }

	void Update () {
        if (StartAnimation)
        {
            if (transform.eulerAngles.y < 180)
            {
                Vector3 euler = transform.eulerAngles;
                euler.y += Time.deltaTime * AnimationSpeed;
                transform.eulerAngles = euler;
                Vector3 POS = transform.position;
                POS.z += Time.deltaTime + ZAnimationSpeed;
                transform.position = POS;
            }
            if (transform.eulerAngles.y >= 180)
            {
                StartAnimation = false;
                Vector3 euler = transform.eulerAngles;
                euler.y = 180;
                transform.eulerAngles = euler;
                Vector3 POS = transform.position;
                transform.position = POS;


            }
        }
        if (BackAnimation)
        {
            //ANI.Play("teste1");
            //BackAnimation = false;
            Vector3 euler = transform.eulerAngles;
            euler.y -= Time.deltaTime * BackAnimationSpeed;
            transform.eulerAngles = euler;
            if (transform.eulerAngles.y < 360 && transform.eulerAngles.y > 180)
            {
                Vector3 euler2 = transform.eulerAngles;
                euler2.y = 0;
                transform.eulerAngles = euler2;
                BackAnimation = false;
            }
        }
    }
}
