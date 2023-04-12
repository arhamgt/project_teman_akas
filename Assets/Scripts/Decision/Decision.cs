using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public bool isLeft;
    public DecisionManager dm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isLeft)
                dm.BackToGameplay();
            else
                dm.GameFinished();
        }
    }
}
