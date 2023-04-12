using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int levelToEat;
    public float speed;
    public bool eaten = false;

    private void OnTriggerStay(Collider other)
    {
        PlayerControler Player =  other.GetComponent<PlayerControler>();
        if (other.CompareTag("Player") && Player.GetLevel() >= levelToEat)
        {
            eaten = true;
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler Player = other.GetComponent<PlayerControler>();
        if (other.CompareTag("Player") && Player.GetLevel() >= levelToEat)
        {
            Player.AddScore(1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        eaten = false;
    }

    // Update is called once per frame
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
