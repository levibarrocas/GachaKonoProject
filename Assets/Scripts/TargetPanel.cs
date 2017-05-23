using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPanel : MonoBehaviour {

    int AmmountOfTargets;

    [SerializeField]
    GameObject[] EnemyTargets;
    [SerializeField]
    GameObject[] FriendlyTargets;

    public static TargetPanel TP;

    private void Awake()
    {
        if (TP == null)
        {
            TP = this;
        }
        else if (TP != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetUPTargetSystem(bool friendly)
    {
        if (friendly)
        {
            for(int i = 0;i< FriendlyTargets.Length; i++)
            {
                FriendlyTargets[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < EnemyTargets.Length; i++)
            {
                EnemyTargets[i].gameObject.SetActive(true);
            }
        } else
        {
            for (int i = 0; i < FriendlyTargets.Length; i++)
            {
                FriendlyTargets[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < EnemyTargets.Length; i++)
            {
                EnemyTargets[i].gameObject.SetActive(true);
            }
        }

    }

}
