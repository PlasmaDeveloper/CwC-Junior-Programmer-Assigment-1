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

    // Start is called before the first frame update
    void Start()
    {
        SetupGetComponents();
        LoadName();
        FillWithSavedName();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        scoreText.text = "Best Score : " + userName + " : 0";
        SaveName();
        mainManager.StartGame();
    }

    [System.Serializable]
    class SaveData
    { 
        public string userName;
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
}
