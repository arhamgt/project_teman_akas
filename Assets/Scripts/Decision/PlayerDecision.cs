using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecision : MonoBehaviour
{
	public float speed;
	public float scale;

	public Transform directionRing;
	
	public float maxDragDistance = 40f;
	private float curDragDistance;
	private Vector3 moveDirection;
	private Vector3 dragStartPos;

	public Animator anim;

	private void Awake()
	{
		//DecisionManager.dm.pd = this;
	}

	private void Update()
    {
		if (!DecisionManager.dm.started || DecisionManager.dm.gameOver)
		{
			return;
		}

		Vector3 mousePos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			dragStartPos = mousePos;
		}
		else if (Input.GetMouseButton(0))
		{
			curDragDistance = (mousePos - dragStartPos).magnitude;

			if (curDragDistance > maxDragDistance)
			{
				dragStartPos = mousePos - moveDirection * maxDragDistance;
				curDragDistance = (mousePos - dragStartPos).magnitude;
			}

			moveDirection = (mousePos - dragStartPos).normalized;
			var direction = new Vector3(moveDirection.x, 0, moveDirection.y);
			transform.position += direction * speed * curDragDistance * Time.deltaTime;
			directionRing.rotation = Quaternion.LookRotation(direction) /* Quaternion.Euler(new Vector3(90, 0, 0))*/;
		}
	}

    private void FixedUpdate()
    {
		if (!DecisionManager.dm.started || DecisionManager.dm.gameOver)
		{
			return;
		}
	}
}