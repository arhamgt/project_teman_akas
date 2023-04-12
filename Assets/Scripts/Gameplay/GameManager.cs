using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public PlayerControler pc;
	public CameraController cam;
	public MenuManager ui;

	public bool started;
	public bool gameOver;

	public float gravity = 10f;
	public float time = 30;
	public float totalTime = 30;
	
	private void Awake()
	{
		if (!gm)
		{
			gm = this;
			DontDestroyOnLoad(this);
		}
	}

	public void Play()
	{
		int hasil = PlayerPrefs.GetInt("Done");
		if (hasil == 1)
			time = PlayerPrefs.GetFloat("time");
        else if (hasil == 2)
			GameOver();
		else
			time = totalTime;
		
		started = true;
	}

	public void GameOver()
	{
		PlayerPrefs.SetInt("Done", 0);
		ui.gameOver.SetActive(true);
		if (time > 0)
        {
			ui.finalScoreText.color = Color.green;
			ui.finalScoreText.text = "Kamu berhasil selesai\ndengan sisa waktu " + time.ToString("0") + " detik!";
		}
        else
        {
			ui.finalScoreText.color = Color.red;
			ui.finalScoreText.text = "Yahh! kamu selesai dengan\ntambahan waktu " + time.ToString("0") + " detik!";
		}
		ui.finalBestText.gameObject.SetActive(false);

		var curBest = PlayerPrefs.GetFloat("bestTime");
		if (PlayerPrefs.GetFloat("time") > curBest)
		{
			PlayerPrefs.SetFloat("bestTime", time);
			ui.finalBestText.gameObject.SetActive(true);
		}		
	}

	private void Update()
	{
		if (!started || gameOver)
		{
			return;
		}

		time -= Time.deltaTime;
		
		if (time < 0)
		{
			ui.timerText.color = Color.red;
			//GameOver();
			//ui.timerText.text = "00:00";
			//return;
		}

		//var minutes = Mathf.FloorToInt(time / 60f);
		//var seconds = Mathf.FloorToInt(time - minutes * 60f);
		//ui.timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
		ui.timerText.text = time.ToString("0");
	}

	public IEnumerator SceneTransition(int sceneIndex)
    {
		PlayerPrefs.SetFloat("time", time);
		gameOver = true;
		cam.ZoomOut();
		yield return new WaitForSeconds(5f);
		gameOver = false;
		started = false;
		SceneManager.LoadScene(sceneIndex);
    }

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}