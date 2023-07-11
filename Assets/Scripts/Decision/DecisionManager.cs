using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DecisionManager : MonoBehaviour
{
	public static DecisionManager dm;
	public PlayerDecision pd;
    public CameraDecision cam;

    public bool gameOver;

	public Animator anim;

	private void Awake()
	{
		if (!dm)
		{
			dm = this;
		}
	}

    public IEnumerator WaitCutscene()
    {
		gameOver = true;
		anim.Play("cutscene_fadeout");
		
        yield return new WaitForSeconds(3f);
		gameOver = false;
		SceneManager.LoadScene(1);
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (gameOver)
		{
			return;
		}
	}
}
