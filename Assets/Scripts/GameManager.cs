using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    private bool gameOn;
    public bool GameOn => gameOn;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text GameOverScoreText;
    [SerializeField] private GameObject GameOverPopup;
    [SerializeField] private TMP_Text countdownText;
    

    [Header("Managers")]
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private HighscoreTable highscoreTable;

    [Header("Player")]
    private string playerName;
    [SerializeField] private TMP_Text playerNameText;
    private int score = 0;

    IEnumerator StartGameCountdown()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }
        countdownText.text = "GO !";
        gameOn = true;
        enemyManager.GameStart();
        yield return new WaitForSeconds(1);
        
        countdownText.gameObject.SetActive(false);       
    }
    private void Start()
    {
        StartCoroutine(StartGameCountdown());
        highscoreTable = FindObjectOfType<HighscoreTable>();
    }
    public void PauseGame()
    {
        gameOn = false;
    }
    public void ResumeGame()
    {
        gameOn = true;
    }
    public void AddPoints(int points)
    {
        score += points;
        ScoreUpdateUI();
    }
    private void ScoreUpdateUI()
    {
        scoreText.text = score.ToString();
    }
    private void GameOverScoreUpdateUI()
    {
        GameOverScoreText.text = score.ToString();
    }
    public void PlayerDeath()
    {
        PauseGame();
        GameOverScoreUpdateUI();
        GameOverPopup.SetActive(true);
    }
    public void SaveHighscore()
    {
        if (playerNameText.text != "")
        {
            playerName = playerNameText.text;
            highscoreTable.AddHighscoreEntry(score, playerName);
        }
    }
}
