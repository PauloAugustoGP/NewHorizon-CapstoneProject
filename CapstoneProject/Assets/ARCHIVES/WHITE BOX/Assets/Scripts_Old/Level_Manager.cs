using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour {

    static Level_Manager _instance = null;
     
    public Transform PlayerSpawn;

	// Use this for initialization
	void Start () {
        if (instance)
            DestroyImmediate(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /*void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "finish")
        {
            Scene_Manager.instance.GoTo_LEVEL("Winning");
        }
    }*/

    public void PlayerDeath()
    {        
        GameObject.FindGameObjectWithTag("Player").transform.position = PlayerSpawn.position;
        GameObject.FindGameObjectWithTag("Player").transform.rotation = PlayerSpawn.rotation;
    }

    public static Level_Manager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}
