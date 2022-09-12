using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle autoTargetingToggle;
    [SerializeField] private HighscoreTable highscore;
    [SerializeField] private GameObject mainMenu;
    public static bool autoTargeting;
    private void Start()
    {
        highscore = FindObjectOfType<HighscoreTable>();
        highscore.SetButtonsListeners(mainMenu);
    }
    public void StartGame()
    {
        autoTargeting = autoTargetingToggle.isOn;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
