using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDecision : MonoBehaviour
{
	public Camera kamera;
	private Vector3 defaultPos;
	private Vector3 step;

	private void Awake()
	{
		kamera = GetComponent<Camera>();
		//DecisionManager.dm.cam = this;
	}

	private void Start()
	{
		defaultPos = transform.localPosition;
		step = defaultPos / DecisionManager.dm.pd.scale;
	}

    public void ZoomOut ()
	{
		StartCoroutine(AnimateZoomOut());
	}

	private IEnumerator AnimateZoomOut()
	{
		Vector3 startPos = transform.localPosition;
		Vector3 targetPos = step * DecisionManager.dm.pd.scale;

		var t = 0f;
		while (t <= 1f)
		{
			transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
			t += Time.deltaTime;
			yield return null;
		}
	}
}