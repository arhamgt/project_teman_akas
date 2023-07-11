using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDecision : MonoBehaviour
{
	public Camera kamera;

	private GameObject player;
	public Vector3 offsetPos;

    bool isGameOver;

    private void Start()
    {
        isGameOver = false;
        player = GameObject.FindWithTag("Player"); // Find the GameObject named Player
    }

    void OnEnable()
    {
        gameObject.transform.parent = null; // This makes the camera a parent object rather than a child
    }

    private void Update()
    {
        if (isGameOver)
        {
            offsetPos = Vector3.MoveTowards(offsetPos, new Vector3(0f, 3f, -10f), 5 * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(45f, 0, 0); // Set the camera's rotation
        transform.position = player.transform.position + offsetPos; // Set cameras final position
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}