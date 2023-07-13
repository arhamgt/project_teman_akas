using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuff : MonoBehaviour
{
    public GameObject buff;

    void Update()
    {
        if (Time.fixedTime % 31f == 0)
        {
            Vector3 randomPos = new Vector3(
                transform.position.x + Random.Range(-1.5f, 1.5f),
                0,
                transform.position.z + Random.Range(.5f, 1.5f)
            );
            Instantiate(buff, randomPos, Quaternion.identity);
        }
    }
}
