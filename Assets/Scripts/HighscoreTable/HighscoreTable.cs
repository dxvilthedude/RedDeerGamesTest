using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour {

    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    [SerializeField] private GameObject leaderboardMenu;
    [SerializeField] private Button scoreboardButton;
    [SerializeField] private Button backToMenuButton;
    private GameObject mainMenu;
    private static HighscoreTable highscoreInstance;
    public void SetButtonsListeners(GameObject menu)
    {
        scoreboardButton = GameObject.Find("ScoreButton").GetComponent<Button>();
        mainMenu = menu;
        if (scoreboardButton != null)
            scoreboardButton.onClick.AddListener(OpenScoreboard);
            scoreboardButton.onClick.AddListener(ReloadList);

        backToMenuButton.onClick.AddListener(OpenMainMenu);
    }
    private void Awake() {

        DontDestroyOnLoad(this);
        if (highscoreInstance == null)
            highscoreInstance = this;
        else
            Destroy(gameObject);

        leaderboardMenu.SetActive(false);
        ReloadList();
    }
    // FOR CLEARING TABLE
   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            PlayerPrefs.DeleteAll();
    }*/
    private void ReloadList()
    {
        Debug.Log("reloadList");
        foreach (Transform child in entryContainer)
        {
            if (child != entryTemplate)
                child.GetComponent<EntryTemplate>().DestroyEntry();
        }
        highscoreEntryTransformList = new List<Transform>();
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores != null)
        {
            // Sort entry list by Score
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {
                        // Swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

            highscoreEntryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            }
        }
    }
    public void OpenScoreboard()
    {
        leaderboardMenu.SetActive(true);
    }
    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 10f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "TH"; break;

        case 1: rankString = "1ST"; break;
        case 2: rankString = "2ND"; break;
        case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("rank").GetComponent<TMP_Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("points").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("name").GetComponent<TMP_Text>().text = name;     

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name) {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public string name;
    }

}
