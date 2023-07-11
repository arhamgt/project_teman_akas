using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Camera kamera;

	private GameObject player;
	public Vector3 offsetPos;
	public Vector3 cameraRotation;

	private void Start()
    {
		player = GameObject.FindWithTag("Player"); // Find the GameObject named Player
	}

	void OnEnable()
	{
		gameObject.transform.parent = null; // This makes the camera a parent object rather than a child
	}

	void LateUpdate()
	{
		transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z); // Set the camera's rotation
		transform.position = player.transform.position + offsetPos; // Set cameras final position
	}
}