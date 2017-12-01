﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCanvas : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void BackToMenu()
    {
        Sound_Manager.instance.MusicCaller("MenuMusic");
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayLevel()
    {
        Sound_Manager.instance.MusicCaller("LevelMusic");
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
