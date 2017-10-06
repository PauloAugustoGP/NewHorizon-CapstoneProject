using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour {

	static Scene_Manager _instance = null;

	// Use this for initialization
	void Start () 
	{
		if (instance)
			DestroyImmediate (gameObject); // destroys the new one that was just created 
		else 
		{
			instance = this;
			DontDestroyOnLoad (this);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void GoTo_MENU()
	{
		if (SceneManager.GetActiveScene ().name != "MainMenu") 
		{
			SceneManager.LoadScene ("MainMenu");
			SoundManger.instance.MusicCaller("Theme");
		} 
		else 
		{
			Debug.LogError ("Error: Can't change to same scene.");
			return;
		}
	}

	public void GoTo_LEVEL(string lvlName)
	{
		if (lvlName == "MainMenu") 
		{
			Debug.LogError ("Error: Main is not a level. Try GoTo_MENU() function");
			return;
		}
		if (SceneManager.GetActiveScene ().name != lvlName) 
		{
			SceneManager.LoadScene (lvlName);
			SoundManger.instance.MusicCaller("LevelMusic");
		} 
		else 
		{
			Debug.LogError ("Error: Can't change to same scene.");
			return;
		}
	}

	public void Reload_LEVEL()
	{
		if (SceneManager.GetActiveScene ().name == "MainMenu") 
		{
			Debug.LogError ("Error: No reload access to scene:" + SceneManager.GetActiveScene().name);
			return;
		}
		SoundManger.instance.MusicCaller("LevelMusic");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}





	public static Scene_Manager instance
	{
		get{ return _instance; }
		set{ _instance = value; }
	}
}