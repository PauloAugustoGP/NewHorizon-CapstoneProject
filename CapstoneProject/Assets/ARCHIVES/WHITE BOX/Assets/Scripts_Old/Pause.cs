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

    // Use this for initialization
    void Start()
    {
        isPaused = false;
        if (resumeBtn)
        {

            resumeBtn.onClick.AddListener(delegate
            {                
                GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 1.0f;
            });
        }

        if (restartBtn)
        {
            restartBtn.onClick.AddListener(delegate { Scene_Manager.instance.Reload_LEVEL(); });
            GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
            isPaused = false;
        }
          
        if (quitBtn)
        {
            quitBtn.onClick.AddListener (Scene_Manager.instance.GoTo_MENU); 
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(KeyCode.Escape))
        {
            if (GetComponentInChildren<Canvas>().enabled == false)
            {
                GetComponentInChildren<Canvas>().enabled = true;
                Time.timeScale = 0f;
                isPaused = true;
            }
            else if (GetComponentInChildren<Canvas>().enabled == true)
            {
                GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 1.0f;
                isPaused = false;
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
}
