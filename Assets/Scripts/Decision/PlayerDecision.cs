using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDecision : MonoBehaviour
{
    public CameraDecision cam;

    public Transform directionRing;

    public Rigidbody rb;

    private float speed = 1f;
    private float speedHalved;
    public float speedOrigin = 1f;

    public AudioSource sound_effect;

    public Animator cutscene_anim;
    public Animator player_anim;

    public Slider slider;

    private void Start()
    {
        speedHalved = speedOrigin * 0.75f;
    }

    private void Update()
    {
        if (DecisionManager.dm.gameOver)
        {
            if (slider.value > 0)
            {
                slider.value -= 1f;
            }

            return;
        }

        float horizontal = Input.GetAxis("Horizontal"); // set a float to control horizontal input
        float vertical = Input.GetAxis("Vertical"); // set a float to control vertical input
        PlayerMove(horizontal, vertical); // Call the move player function sending horizontal and vertical movements

        if (transform.position.z < 5)
            transform.position = new Vector3(transform.position.x, transform.position.y, 5.15f);
        else if (transform.position.z > 25)
            transform.position = new Vector3(transform.position.x, transform.position.y, 24.85f);
        else if (transform.position.x > 25)
            transform.position = new Vector3(24.85f, transform.position.y, transform.position.z);
        else if (transform.position.x < 15)
            transform.position = new Vector3(15.15f, transform.position.y, transform.position.z);
    }

    private void PlayerMove(float h, float v)
    {
        if (h != 0f || v != 0f) // If horizontal or vertical are pressed then continue
        {
            if (h != 0f && v != 0f) // If horizontal AND vertical are pressed then continue
            {
                speed = speedHalved; // Modify the speed to adjust for moving on an angle
            }
            else // If only horizontal OR vertical are pressed individually then continue
            {
                speed = speedOrigin; // Keep speed to it's original value
            }

            Vector3 targetDirection = new Vector3(h, 0f, v); // Set a direction using Vector3 based on horizontal and vertical input
            rb.MovePosition(rb.position + targetDirection * speed * Time.deltaTime); // Move the players position based on current location while adding the new targetDirection times speed
            RotatePlayer(targetDirection); // Call the rotate player function sending the targetDirection variable
        }
    }

    private void RotatePlayer(Vector3 dir)
    {
        rb.MoveRotation(Quaternion.LookRotation(dir)); // Rotate the player to look at the new targetDirection
    }

    private void FixedUpdate()
    {
        if (DecisionManager.dm.gameOver)
        {
            return;
        }
    }

    public void GameOver()
    {
        cam.GameOver();
        sound_effect.Play();
        player_anim.Play("death");
        DecisionManager.dm.gameOver = true;
    }

    public void MoveToGameplay()
    {
        StartCoroutine(DecisionManager.dm.WaitCutscene());
    }
}
