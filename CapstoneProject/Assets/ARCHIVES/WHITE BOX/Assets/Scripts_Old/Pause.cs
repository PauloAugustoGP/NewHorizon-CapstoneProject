using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public Button resumeBtn;
    public Button optBtn;
    public Button quitBtn;

    // Use this for initialization
    void Start()
    {
        if (resumeBtn)
        {

            resumeBtn.onClick.AddListener(delegate
            {                
                GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 1.0f;
            });
        }

        /* if (optBtn)
             optBtn.onClick.AddListener(delegate { Scene_Manager.instance.GoTo_LEVEL("options"); });
          */
        if (quitBtn)
        {
            quitBtn.onClick.AddListener (Scene_Manager.instance.GoTo_MENU); 
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GetComponentInChildren<Canvas>().enabled == false)
            {
                GetComponentInChildren<Canvas>().enabled = true;
                Time.timeScale = 0f;
            }
            else if (GetComponentInChildren<Canvas>().enabled == true)
            {
                GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 1.0f;
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
