using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_Manager : MonoBehaviour
{
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject ControlsPanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject TestPanel;
    [SerializeField] GameObject LoadingPanel;
    [SerializeField] GameObject AudioPanel;

    [SerializeField] Text ButtonDisplay;
    [SerializeField] Button[] MenuButtons;

    [SerializeField] Slider MasterSlider;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;

    [SerializeField] Toggle MasterMute;
    [SerializeField] Toggle MusicMute;
    [SerializeField] Toggle SFXMute;

    public void StartGame()
    {
        MainPanel.gameObject.SetActive(false);
        LoadingPanel.gameObject.SetActive(true);
    }

     // For menu's panels
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
        AudioPanel.gameObject.SetActive(false);
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

    public void Audio()
    {
        OptionsPanel.gameObject.SetActive(false);
        AudioPanel.gameObject.SetActive(true);
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



    // This is for the test levels and for testing purposes only

    public void test1()
    {
        Scene_Manager.instance.GoTo_LEVEL("SmallTestScene");
        Debug.Log("test 1");
    }
    
    public void test2 ()
    {
        Scene_Manager.instance.GoTo_LEVEL("TestScene2");
        Debug.Log("test 2");
    }

    public void test3()
    {
        Scene_Manager.instance.GoTo_LEVEL("ObjectBuilding");
        Debug.Log("test 3");
    }
    
}
