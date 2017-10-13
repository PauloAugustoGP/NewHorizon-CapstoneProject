using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Manager : MonoBehaviour
{
    [SerializeField]
    List<string> levelNames;

    public Canvas[] Canvi;

    Canvas main;
    Canvas control;
    Canvas options;
    Canvas credits;


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

        Canvi = GetComponentsInChildren<Canvas>();

        foreach (Canvas can in Canvi)
        {
            if (can.name == "Menu Canvas")
                main = can;
            else if (can.name == "Options Canvas")
                options = can;
            else if (can.name == "Controls Canvas")
                control = can;
            else if (can.name == "Credits Canvas")
                credits = can;
        }

        for (int i = 0; i < Canvi.Length; i++)
        {
            Canvi[i] = null;
        }

        if (menuBtn)
        {
            menuBtn.onClick.AddListener(Scene_Manager.instance.GoTo_MENU);
        }

        BackToMenu();




        //starBuilder.GetComponent<Button>().
    }

    public void StartGame()
    {
        Scene_Manager.instance.GoTo_LEVEL("Level");
    }

    public void Options()
    {
        main.enabled = false;
        control.enabled = false;
        options.enabled = true;
        credits.enabled = false;
    }

    public void BackToMenu()
    {
        main.enabled = true;
        control.enabled = false;
        options.enabled = false;
        credits.enabled = false;

    }

    public void BackToOptions()
    {
        main.enabled = false;
        control.enabled = false;
        options.enabled = true;
        credits.enabled = false;
    }

    public void controls()
    {
        main.enabled = false;
        control.enabled = true;
        options.enabled = false;
        credits.enabled = false;
    }

    public void Credits()
    {
        main.enabled = false;
        control.enabled = false;
        options.enabled = false;
        credits.enabled = true;
    }

    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
    
    /*[SerializeField] List<string> levelNames;

    
    public Button startBtn;
    public Button optBtn;
    public Button credBtn;
    public Button quitBtn;
    public Button menuBtn;


    // Use this for initialization
    void Start()
    {
        */
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
    /* if (menuBtn)
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
 }*/
}
