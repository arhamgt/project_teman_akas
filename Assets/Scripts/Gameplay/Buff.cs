using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public float buffSpeed;
    public float buffDuration;

    public float speedEaten;
    public bool eaten = false;

    public AudioSource sfx;
    public Collider coll;

    private void OnTriggerStay(Collider other)
    {
        PlayerControler Player = other.GetComponent<PlayerControler>();
        if (other.CompareTag("Player"))
        {
            eaten = true;
            transform.position = Vector3.MoveTowards(
                transform.position,
                other.transform.position,
                speedEaten * 3 * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler Player = other.GetComponent<PlayerControler>();
        if (other.CompareTag("Player"))
        {
            sfx.Play();
            Player.SpeedBuff(buffSpeed, buffDuration);
        }
    }

    void Start()
    {
        eaten = false;
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        if (eaten)
        {
            transform.localScale -= Vector3.one * speedEaten * Time.deltaTime;
            if (transform.localScale.x < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
