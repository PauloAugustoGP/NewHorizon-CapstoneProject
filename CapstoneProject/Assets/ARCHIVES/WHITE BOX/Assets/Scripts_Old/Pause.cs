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

    [SerializeField] private GameObject[] _objectsToDisable;
    private LinkedList<MonoBehaviour> _componentsToDisable = new LinkedList<MonoBehaviour>();


    // Use this for initialization
    void Start()
    {
        if (_objectsToDisable.Length > 0)
        {
            foreach (GameObject go in _objectsToDisable)
            {
                foreach (MonoBehaviour mo in go.GetComponentsInChildren<MonoBehaviour>())
                {
                    _componentsToDisable.AddLast(mo);
                }
            }
        }

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
            restartBtn.onClick.AddListener(Scene_Manager.instance.Reload_LEVEL);
            GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
            isPaused = false;
        }

        if (quitBtn)
        {
            Debug.Log("quitting");
            quitBtn.onClick.AddListener(Scene_Manager.instance.GoTo_MENU);
            Debug.Log("quitting2");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                //PauseGame(true);
                GetComponentInChildren<Canvas>().enabled = true;
                Time.timeScale = 0f;
                isPaused = true;
            }
            else if (isPaused)
            {
                //PauseGame(false);
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

    public void PauseGame(bool pPause)
    {
        if (pPause)
        {
            SetGame(false);
            _cameraController.inGame = false;
            Cursor.lockState = CursorLockMode.Confined;
            GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            SetGame(true);
            _cameraController.inGame = true;
            GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
            isPaused = false;
        }
    }

    private void SetGame(bool pState)
    {
        if (_componentsToDisable.Count > 0)
        {
            foreach (MonoBehaviour script in _componentsToDisable)
            {
                script.enabled = pState;
            }
        }
    }

    /*public void quit()
    {
        Debug.Log("quit54dafsdfhg34");

    }*/


}
