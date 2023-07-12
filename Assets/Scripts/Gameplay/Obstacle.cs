using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int levelToEat;
    public float speed;
    public bool eaten = false;

    public Collider coll;

    private void OnTriggerStay(Collider other)
    {
        PlayerControler Player = other.GetComponent<PlayerControler>();
        if (other.CompareTag("Player") && Player.GetLevel() >= levelToEat)
        {
            eaten = true;
            transform.position = Vector3.MoveTowards(
                transform.position,
                other.transform.position,
                speed * 3 * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayerControler Player = other.gameObject.GetComponent<PlayerControler>();
        if (other.gameObject.CompareTag("Player") && Player.GetLevel() >= levelToEat)
        {
            coll.isTrigger = true;
            Player.AddScore(1);
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
            transform.localScale -= Vector3.one * speed * Time.deltaTime;
            if (transform.localScale.x < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
