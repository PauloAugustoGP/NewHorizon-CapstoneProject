using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    //Drag Pause Prefab from Dan's folder into the heirarchy
    //create an Event System in the heirarchy
    //make sure the scene being used is in the build settings
    //quit and restart dont seem to work this way becuase its not through the menu's, will look into it

    public Button resumeBtn;
    public Button restartBtn;
    public Button quitBtn;

    public bool isPaused;

	public CameraController _cameraController;

    // Use this for initialization
    void Start()
	{

        isPaused = false;
        /*if (resumeBtn)
        {

            resumeBtn.onClick.AddListener(delegate
            {                
                GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 1.0f;
            });
        }*/

        if (restartBtn)
        {
            restartBtn.onClick.AddListener(delegate { Scene_Manager.instance.Reload_LEVEL(); });
            GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
            isPaused = false;
        }
          
        /*if (quitBtn)
        {
            quitBtn.onClick.AddListener (Scene_Manager.instance.GoTo_MENU); 
        }*/ 
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (!isPaused)
            {
				PauseGame(true);
            }
			else if (isPaused)
            {
				PauseGame(false);
            }

            
        }

                     


        /*
        if (UI.GetComponentInChildren<Canvas>().enabled)
        {
            UI.GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            UI.GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0f;
        }*/
    }

	public void PauseGame(bool pPause)
	{
		if(pPause)
		{
			GetComponentInChildren<Canvas>().enabled = true;
			Time.timeScale = 0f;
			isPaused = true;
			_cameraController.inGame = false;
		}
		else
		{
			GetComponentInChildren<Canvas>().enabled = false;
			Time.timeScale = 1.0f;
			isPaused = false;
			_cameraController.inGame = true;
		}
	}
}
