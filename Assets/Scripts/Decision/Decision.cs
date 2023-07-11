using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public PlayerDecision pd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pd.GameOver();
        }
    }
}
