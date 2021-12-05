using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public int bestScore;
    public string bestScoreUser;

    public string playerName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
        }
    }
}
