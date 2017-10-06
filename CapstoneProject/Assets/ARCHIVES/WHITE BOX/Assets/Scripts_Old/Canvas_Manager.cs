using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Manager : MonoBehaviour
{
    [SerializeField] List<string> levelNames;

    public Button startBtn;
    public Button optBtn;
    public Button credBtn;
    public Button quitBtn;
    public Button menuBtn;


    // Use this for initialization
    void Start()
    {
        //Menu Scene
        /*if (startBtn)
            startBtn.onClick.AddListener(delegate { Scene_Manager.instance.GoTo_LEVEL(levelNames[0]); });

        if (optBtn)
             optBtn.onClick.AddListener(delegate { Scene_Manager.instance.GoTo_LEVEL(levelNames[1]); });

        if (credBtn)
            credBtn.onClick.AddListener(delegate { Scene_Manager.instance.GoTo_LEVEL(levelNames[2]); });*/
        
        /*
        if (quitBtn)
        {
            quitBtn.onClick.AddListener(delegate
            {
                Application.Quit();
                Debug.Log("quit game");

            });            
        }*/


        //Winning Scene
        if (menuBtn)
        {
            menuBtn.onClick.AddListener( Scene_Manager.instance.GoTo_MENU);  
        }




        //starBuilder.GetComponent<Button>().
    }

    public void StartGame()
    {
        Scene_Manager.instance.GoTo_LEVEL(levelNames[0]);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
