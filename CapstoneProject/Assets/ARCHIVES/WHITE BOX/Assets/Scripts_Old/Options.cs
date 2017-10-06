using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public Button mainBtn;
    public Button contBtn;    
    

    // Use this for initialization
    void Start()
    {

        if (mainBtn)
            mainBtn.onClick.AddListener(Scene_Manager.instance.GoTo_MENU);
               
        if (contBtn)
            contBtn.onClick.AddListener(delegate { Scene_Manager.instance.GoTo_LEVEL("Controls"); });


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene_Manager.instance.GoTo_MENU();
        }
    }
}
