using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_Manager : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private GameObject CreditsPanel;
    [SerializeField] private GameObject TestPanel;
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject GarrettLoadingPanel;
    [SerializeField] private GameObject AudioPanel;

    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    [SerializeField] private Toggle MasterMute;
    [SerializeField] private Toggle MusicMute;
    [SerializeField] private Toggle SFXMute;

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
        ControlsPanel.gameObject.SetActive(false);
    }

    public void BackToOptions()
    {
        AudioPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(true);
    }

    public void controls()
    {
        ControlsPanel.gameObject.SetActive(true);
        MainPanel.gameObject.SetActive(false);
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
        Debug.Log("Andre");
    }
    
    public void test2 ()
    {
        Scene_Manager.instance.GoTo_LEVEL("Level_2_temp");
        Debug.Log("Tyler");
    }

    public void test3()
    {
        TestPanel.gameObject.SetActive(false);
        GarrettLoadingPanel.gameObject.SetActive(true);
    }

    public void PowerRoom()
    {
        Scene_Manager.instance.GoTo_LEVEL("PowerRoom_Level");
    }
    
    public void HangarLevel()
    {
        Scene_Manager.instance.GoTo_LEVEL("Hangar_v1");
    }

    public void KeyPadTest()
    {
        Scene_Manager.instance.GoTo_LEVEL("Keypad_Level");
    }
}
