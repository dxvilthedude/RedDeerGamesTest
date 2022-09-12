using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidInputHandler : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	private void LateUpdate()
	{
		//if (Application.platform == RuntimePlatform.Android)
			if (Input.GetKey(KeyCode.Escape))
				PauseMenu();
	}

	private void PauseMenu()
	{
		var scene = SceneManager.GetActiveScene();
		switch (scene.buildIndex)
		{
			case 0:
				Application.Quit();
				break;
			case 1:;
				PauseGame();
				break;
		}
	}
    private void PauseGame()
	{
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
	}
	public void ResumeGame()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}
	public void BackToMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}
