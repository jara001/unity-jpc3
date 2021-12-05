using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    int bestScore = 0;
    string bestScoreUser = "Noone";

    public string playerName;
    [SerializeField] InputField playerText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
            UpdatePlayerName();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        playerName = playerText.text;
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class GameData
    {
        public int bestScore;
        public string bestScoreUser;

        public string playerName;
    }

    void SaveData()
    {
        GameData data = new GameData();
        data.bestScore = bestScore;
        data.bestScoreUser = bestScoreUser;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            bestScore = data.bestScore;
            bestScoreUser = data.bestScoreUser;
            playerName = data.playerName;

            UpdateBestScoreText();
        }
    }

    void UpdatePlayerName()
    {
        playerText.text = playerName;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public string GetBestScoreUser()
    {
        return bestScoreUser;
    }

    public void UpdateBestScore(string name, int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreUser = name;
            SaveData();
        }
    }

    void UpdateBestScoreText()
    {
        bestScoreText.text = "Best Score: " + bestScoreUser + " - " + bestScore;
    }
}
