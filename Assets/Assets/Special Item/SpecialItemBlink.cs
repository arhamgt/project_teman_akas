using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemBlink : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float timer = 0f;
    public float wholeTime;
    public float step;
    public float offset;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float time = step * offset;

        if (timer > wholeTime)
        {
            timer = 0;
        }

        if (timer > wholeTime - time)
        {
            meshRenderer.enabled = false;
        }
        else if (timer > time)
        {
            meshRenderer.enabled = true;
        }
    }
}
