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

	public bool gameOver;

	public float gravity = 10f;
    public float time = 30;
    public float totalTime = 30;

	public int totalBuff;
	public List<GameObject> Buffs = new List<GameObject>();

    private void Awake()
    {
        if (!gm)
        {
            gm = this;
        }
    }

    private void Start()
    {
        for (int i = 1; i <= totalBuff; i++)
        {
			int rand = Random.Range(0, Buffs.Count);
			Buffs[rand].SetActive(true);
			Buffs.RemoveAt(rand);
		}
	}

    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (gameOver)
		{
			return;
		}

        time -= Time.deltaTime;

        if (time < 0)
        {
            ui.timerText.color = Color.red;
        }
		else if (time < 30)
        {
			if (Mathf.RoundToInt(time) % 2 == 0)
				ui.timerText.color = Color.red;
			else
				ui.timerText.color = Color.white;
		}

        ui.timerText.text = time.ToString("0");
    }

	public IEnumerator SceneTransition(int sceneIndex)
    {
		gameOver = true;
		yield return new WaitForSeconds(5f);
		gameOver = false;
		SceneManager.LoadScene(sceneIndex);
    }

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}