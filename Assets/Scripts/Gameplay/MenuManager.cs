using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
	public GameObject mainMenu;
	public TextMeshProUGUI bestText;

	public GameObject gameplay;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI timerText;

	public GameObject gameOver;

	private void Awake()
	{
		GameManager.gm.ui = this;
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
    }

    public void StartGame()
	{
		mainMenu.SetActive(false);
		gameplay.SetActive(true);
	}

	public void Continue()
	{
		GameManager.gm.gameOver = false;
		GameManager.gm.RestartScene();
	}
}
