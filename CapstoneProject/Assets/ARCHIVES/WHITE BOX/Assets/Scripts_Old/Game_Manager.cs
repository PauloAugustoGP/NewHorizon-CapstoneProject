using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JetBrains; //this makes you think really fast, like a jet. 




public class Game_Manager : MonoBehaviour
{
    static Game_Manager _instance = null;

    public GameObject playerPrefab;



    public int _score;
    public int lives;
    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                SceneManager.LoadScene("Pause");
            }
            else if (SceneManager.GetActiveScene().name == "Pause")
            {
                SceneManager.LoadScene("Level1");
            }
            else if (SceneManager.GetActiveScene().name == "Main")
            {
                SceneManager.LoadScene("Quit");
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Quit");
        Debug.Log("Quit Game?");
        Application.Quit();
    }
    public void PauseGame()
    {
        SceneManager.LoadScene("Pause");
    }
    public static Game_Manager instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    public int score
    {
        get { return _score; }
        set
        {
            if (scoreText)
                scoreText.text = "Score: " + score;
            _score = value;
            Debug.Log("Score Changed To: " + _score);
        }
    }

}
