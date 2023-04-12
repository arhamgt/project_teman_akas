using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecisionManager : MonoBehaviour
{
	public static DecisionManager dm;
	public PlayerDecision pd;
    public CameraDecision cam;

    public bool started;
    public bool gameOver;

	public Animator anim;

	private void Awake()
	{
		if (!dm)
		{
			dm = this;
			DontDestroyOnLoad(this);
		}
	}

	void Start()
    {
        Play();
    }

    public void BackToGameplay()
    {
        PlayerPrefs.SetInt("Done", 1);
        StartCoroutine(WaitCutscene());
    }

    public void GameFinished()
    {
        PlayerPrefs.SetInt("Done", 2);
        StartCoroutine(WaitCutscene());
    }

    IEnumerator WaitCutscene()
    {
		gameOver = true;
		anim.Play("cutscene_fadeout");
        yield return new WaitForSeconds(3f);
		gameOver = false;
		SceneManager.LoadScene(0);
    }

	public void Play()
	{
		started = true;
	}

	private void Update()
	{
		if (!started || gameOver)
		{
			return;
		}
	}
}
