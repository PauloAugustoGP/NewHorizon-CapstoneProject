using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_Manager : MonoBehaviour
{
    //[SerializeField] List<string> levelNames;
    //[SerializeField] GameObject[] Panels;

    //public Canvas[] Canvi;
    //public int panelSwitch = 0;
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject ControlsPanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject TestPanel;
    [SerializeField] GameObject LoadingPanel;

    [SerializeField] Text ButtonDisplay;
    [SerializeField] Button[] MenuButtons;

    /*Canvas main;
    Canvas control;
    Canvas options;
    Canvas credits;*/


    /*public Button startBtn;
    public Button optBtn;
    public Button credBtn;
    public Button quitBtn;
    public Button menuBtn;*/



    // Use this for initialization
    void Start()
    {
        
        /*foreach (Button buttons in MenuButtons)
        {
            this.ButtonDisplay.text = SceneManager.GetSceneByBuildIndex(0).name;
            //this.ButtonDisplay.text = SceneUtility.GetScenePathByBuildIndex(2).ToString();
            //this.ButtonDisplay.text = SceneManager.GetSceneAt(1).name;

            //Debug.Log(SceneManager.GetSceneAt(2).name);
            Debug.Log(SceneUtility.GetScenePathByBuildIndex(2).ToString());
            //Debug.Log(SceneUtility.GetScenePathByBuildIndex(2).ToString());
            
        }*/
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

        // Canvi = GetComponentsInChildren<Canvas>();

        // panelScreen(panelSwitch);

        

    }

    //private void panelScreen(int panelSwitch)
    //{
    /* foreach (GameObject panel in Panels)
     {
         panel.gameObject.SetActive(false);
         Debug.Log(panel.name);

         /*if (panel.name == "Menu Canvas")
             main = panel;
         else if (panel.name == "Options Canvas")
             options = panel;
         else if (panel.name == "Controls Canvas")
             control = can;
         else if (panel.name == "Credits Canvas")
             credits = can;*/
    //}
    //OptionsPanel.gameObject.SetActive(false);
    //CreditsPanel.gameObject.SetActive(false);
    //ControlsPanel.gameObject.SetActive(false);
    /*for (int i = 0; i < Panels.Length; i++)
    {
        Panels[i] = null;
    }*/
    //MainPanel.gameObject.SetActive(true);



    /*switch (panelSwitch)
    {
        case 0:
            MainPanel.gameObject.SetActive(true);
            break;
        case 1:
            OptionsPanel.gameObject.SetActive(true);
            break;
        case 2:
            CreditsPanel.gameObject.SetActive(true);
            break;
        case 3:
            ControlsPanel.gameObject.SetActive(true);
            break;
            //mainMenuPanel.gameObject.SetActive(true);
    }*/



    /* if (menuBtn)
     {
         menuBtn.onClick.AddListener(Scene_Manager.instance.GoTo_MENU);
     }*/

    //BackToMenu();

        //}
        
        //starBuilder.GetComponent<Button>().
    

    public void StartGame()
    {
        //Scene_Manager.instance.GoTo_LEVEL("Loading Screen");
        LoadingPanel.gameObject.SetActive(true);
    }

     // for if menu's are panels
    public void Options()
    {
        MainPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        MainPanel.gameObject.SetActive(true);
        OptionsPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(false);
        TestPanel.gameObject.SetActive(false);
    }

    public void BackToOptions()
    {
        ControlsPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(true);
    }

    public void controls()
    {
        ControlsPanel.gameObject.SetActive(true);
        OptionsPanel.gameObject.SetActive(false);
    }

    public void Credits()
    {
        MainPanel.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(true);
    }

    public void Test()
    {
        MainPanel.gameObject.SetActive(false);
        TestPanel.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }



    // Hereafter is for the test levels and for testing purposes only

    public void andre()
    {
        Scene_Manager.instance.GoTo_LEVEL("SmallTestScene");
        
        Debug.Log("Andre");
    }

    public void brian()
    {
        Scene_Manager.instance.GoTo_LEVEL("Brian Scene");
        Debug.Log("Brian");
    }

    public void dan()
    {
        Scene_Manager.instance.GoTo_LEVEL("Dan's Test Zone");
        Debug.Log("Dan");
    }

    public void clement()
    {
        //Scene_Manager.instance.GoTo_LEVEL("Test");
        Debug.Log("Clement");
    }

    public void garrett()
    {
        Scene_Manager.instance.GoTo_LEVEL("Garrett_Test_Scene");
        Debug.Log("Garrett");
    }

    public void Michael()
    {
        Scene_Manager.instance.GoTo_LEVEL("Camera_Test");
        Debug.Log("Michael");
    }

    public void paulo()
    {
        Scene_Manager.instance.GoTo_LEVEL("AITest");
        Debug.Log("Paulo");
    }

    public void ryck()
    {
        Scene_Manager.instance.GoTo_LEVEL("CharMov_TEST");
        Debug.Log("Ryck");
    }

    public void tobi()
    {
        Scene_Manager.instance.GoTo_LEVEL("Tobi_Test_Scene");
        Debug.Log("Tobi");
    }

    public void tyler()
    {
        Scene_Manager.instance.GoTo_LEVEL("TestScene2");
        Debug.Log("tyler");
    }



    /* //menu controls is menus are canvas
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
    }*/

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
