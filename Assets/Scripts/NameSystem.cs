using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class NameSystem : MonoBehaviour
{
    //objects and components
    [SerializeField] private MainManager mainManager;
    [SerializeField] private Text scoreText;
    [SerializeField] private TMP_InputField inputUsername;

    //values
    private string userName;
    private int pointsThisRound;
    private int pointsOverall;
    private bool gameEndCheck; //so that function runs only once on game over, set false after gamestart and true after OnGameOver()

    // Start is called before the first frame update
    void Start()
    {
        SetupGetComponents();
        LoadName();
        FillWithSavedName();
        gameEndCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEndCheck)
        {
            OnGameOver();
        }
    }

    private void OnGameOver()
    {
        if (mainManager.GetGameOver())
        {
            Debug.Log("Game Over");
            pointsThisRound = mainManager.GetPoints();
            SavePoints();
            gameEndCheck = true;
        }
    }

    //objects and components
    void SetupGetComponents()
    {

    }

    public void FillWithSavedName()
    {
        inputUsername.text = userName;
    }

    public void SetName(string input)
    {
        userName = input;
        Debug.Log(userName);
    }

    public void AcceptName()
    {
        LoadPoints();
        scoreText.text = "Best Score : " + userName + " : " + pointsOverall;
        SaveName();
        mainManager.StartGame();
    }

    public void SetPoints(int points)
    {
        pointsOverall = points;
    }

    [System.Serializable]
    class SaveData
    { 
        public string userName;
        public int pointsOverall;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.userName = userName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            userName = data.userName;
            SetName(data.userName);
        }
    }

    public void SavePoints()
    {
        SaveData data = new SaveData();
        if (pointsThisRound > pointsOverall)
        {
            data.pointsOverall = pointsThisRound;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefilePoints.json", json);
        }
        //else dont change points anymore
        
    }

    public void LoadPoints()
    {
        string path = Application.persistentDataPath + "/savefilePoints.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            pointsOverall = data.pointsOverall;
            SetPoints(data.pointsOverall);
        }
        else 
        {
            pointsOverall = 0; //have to set, to prevent errors in AcceptName() on gamestart, with no points saved
        }
    }
}
