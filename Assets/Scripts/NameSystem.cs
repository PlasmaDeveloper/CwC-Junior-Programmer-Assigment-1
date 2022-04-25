using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameSystem : MonoBehaviour
{
    //objects and components
    [SerializeField] private MainManager mainManager;
    [SerializeField] private Text scoreText;

    //values
    private string userName;

    // Start is called before the first frame update
    void Start()
    {
        SetupGetComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //objects and components
    void SetupGetComponents()
    {

    }

    public void SetName(string input)
    {
        userName = input;
        Debug.Log(userName);
    }

    public void AcceptName()
    {
        scoreText.text = "Best Score : " + userName + " : 0";
        mainManager.StartGame();
    }
}
