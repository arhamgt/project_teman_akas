using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControler : MonoBehaviour
{
	public int finishScore;
	public int score;
	public int level;
	public float scale;
	public float speed;
	public float radius;
	public bool sizeUp;

	public Transform visuals;
	public Transform directionRing;
	public Transform playerCanvas;
	public GameObject scoreIndicator;

	public SphereCollider detector;
	public BoxCollider[] colliders;
	public List<Rigidbody> victims;
	
	public float maxDragDistance = 40f;
	private float curDragDistance;
	private Vector3 moveDirection;
	private Vector3 dragStartPos;

	public Animator anim;

	private void Awake()
	{
		GameManager.gm.pc = this;
	}

	private void Start()
	{
		level = 0;
		RefreshScale();
	}

	private void Update()
    {
		if (!GameManager.gm.started || GameManager.gm.gameOver)
		{
			return;
		}

		if (finishScore <= score)
        {
			anim.Play("cutscene_fadeout");
			RefreshScale();
			StartCoroutine(GameManager.gm.SceneTransition(1));
			//GameManager.gm.cam.ZoomOut(3f);
			//GameManager.gm.GameOver();
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

		var nearbyObjects = Physics.OverlapSphere(transform.position, radius);
		foreach(var nearbyObject in nearbyObjects)
		{
			if (nearbyObject.gameObject == gameObject || nearbyObject.gameObject.layer != 9)
			{
				continue;
			}

			Rigidbody nearbyObjectRb = nearbyObject.GetComponentInParent<Rigidbody>();
			if (!nearbyObjectRb || victims.Contains(nearbyObjectRb))
			{
				continue;
			}
			else
			{
				victims.Add(nearbyObjectRb);
				nearbyObject.gameObject.layer = 10;
				//nearbyObjectRb.isKinematic = false;
			}
		}
	}

	private void FixedUpdate()
	{
		if (!GameManager.gm.started || GameManager.gm.gameOver)
		{
			return;
		}
	}

    private void OnTriggerExit(Collider other)
	{
		if (other.transform.position.y < 0)
		{
			other.gameObject.SetActive(false);

			Rigidbody victimRb = other.GetComponent<Rigidbody>();
			if (victimRb)
			{
				victims.Remove(victimRb);
			}
		}
	}

	public void AddScore(int amount)
	{
		score += amount;
		sizeUp = false;
		CheckSize();

		GameObject indicator = Instantiate(scoreIndicator, playerCanvas);
		TextMeshProUGUI scoreText = indicator.GetComponent<TextMeshProUGUI>();
		scoreText.text = "+" + amount.ToString();
		StartCoroutine(DisableAfter(indicator, 2f));

		GameManager.gm.ui.scoreText.text = score.ToString();
	}

	private IEnumerator DisableAfter(GameObject obj, float delay)
	{
		if (!obj)
		{
			yield break;
		}

		yield return new WaitForSeconds(delay);

		if (!obj)
		{
			yield break;
		}
		obj.SetActive(false);
	}

	public void CheckSize()
	{
		if (!sizeUp && score % 10 == 0)
		{
			sizeUp = true;
			RefreshScale();			
		}
	}

	public void RefreshScale()
	{
		level++;
		scale++;
		radius++;
		visuals.localScale = new Vector3(scale, scale, scale);
		visuals.localPosition = new Vector3(0, -scale / 2f - 0.49f, 0);
		//detector.center = new Vector3(0, -1f - scale / 2f, 0);
		detector.center = new Vector3(0, scale / 5f, 0);
		detector.radius = scale / 3f;
		
		foreach (var coll in colliders)
		{
			var direction = coll.center.normalized * 50.025f;
			coll.center = direction * (1 + scale / 100f);
		}

		GameManager.gm.cam.ZoomOut();
	}

	public int GetLevel()
    {
		return level;
    }

	private void OnDrawGizmos()
	{
		Debug.DrawLine(transform.position - new Vector3(radius, 0, 0), transform.position + new Vector3(radius, 0, 0), Color.black);
		Debug.DrawLine(transform.position - new Vector3(0, 0, radius), transform.position + new Vector3(0, 0, radius), Color.black);
	}
}